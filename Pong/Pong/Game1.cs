using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont mainFont;

        GameElements.Paddle leftPaddle = new GameElements.Paddle();
        GameElements.Paddle rightPaddle = new GameElements.Paddle();
        GameElements.Ball gameBall = new GameElements.Ball();

        int P1Score;
        int P2Score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Left Paddle
            leftPaddle.Bounds = new Rectangle(new Point(10, 10), new Point(20, 150));
            leftPaddle.Color = Color.White;
            leftPaddle.SpriteBatch = spriteBatch;
            leftPaddle.Content = Content;

            Components.Add(leftPaddle);

            // Right Paddle
            rightPaddle.Bounds = new Rectangle(new Point(GraphicsDevice.Viewport.Width - 30, 10), new Point(20, 150));
            rightPaddle.Color = Color.White;
            rightPaddle.SpriteBatch = spriteBatch;
            rightPaddle.Content = Content;

            Components.Add(rightPaddle);

            // Ball
            gameBall.Bounds = new Rectangle(new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), new Point(20, 20));
            gameBall.Color = Color.White;
            gameBall.SpriteBatch = spriteBatch;
            gameBall.Content = Content;
            gameBall.Speed = new Point(-2, 2);

            Components.Add(gameBall);

            mainFont = Content.Load<SpriteFont>("Fonts/Main");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                leftPaddle.Bounds = new Rectangle(new Point(10, 10), new Point(20, 150));
                rightPaddle.Bounds = new Rectangle(new Point(GraphicsDevice.Viewport.Width - 30, 10), new Point(20, 150));
                gameBall.Bounds = new Rectangle(new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), new Point(20, 20));
                gameBall.Speed = new Point(-2, 2);
                P1Score = 0;
                P2Score = 0;
            }

            #region Left Paddle Logic
            ///
            /// Left Paddle
            ///

            // Check if paddle should go Up
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && leftPaddle.Bounds.Y != 10)
            {
                leftPaddle.Bounds = new Rectangle(new Point(leftPaddle.Bounds.X, leftPaddle.Bounds.Y - 5), new Point(leftPaddle.Bounds.Width, leftPaddle.Bounds.Height));
            }

            // Check if paddle should go Down
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && (leftPaddle.Bounds.Y + leftPaddle.Bounds.Height) + 10 != GraphicsDevice.Viewport.Height)
            {
                leftPaddle.Bounds = new Rectangle(new Point(leftPaddle.Bounds.X, leftPaddle.Bounds.Y + 5), new Point(leftPaddle.Bounds.Width, leftPaddle.Bounds.Height));
            }

            // Check if ball is colliding with paddle
            if (leftPaddle.Bounds.Intersects(gameBall.Bounds))
            {
                gameBall.Speed = new Point(-gameBall.Speed.X, -gameBall.Speed.Y);
                gameBall.Speed = new Point(gameBall.Speed.X + 1, gameBall.Speed.Y + 1);
            }
            #endregion

            #region Right Paddle Logic
            ///
            /// Right Paddle
            ///

            // Check if paddle should go Up
            if (Keyboard.GetState().IsKeyDown(Keys.T) && rightPaddle.Bounds.Y != 10)
            {
                rightPaddle.Bounds = new Rectangle(new Point(rightPaddle.Bounds.X, rightPaddle.Bounds.Y - 5), new Point(rightPaddle.Bounds.Width, rightPaddle.Bounds.Height));
            }

            // Check if paddle should go Down
            if (Keyboard.GetState().IsKeyDown(Keys.G) && (rightPaddle.Bounds.Y + rightPaddle.Bounds.Height) + 10 != GraphicsDevice.Viewport.Height)
            {
                rightPaddle.Bounds = new Rectangle(new Point(rightPaddle.Bounds.X, rightPaddle.Bounds.Y + 5), new Point(rightPaddle.Bounds.Width, rightPaddle.Bounds.Height));
            }

            // Check if ball is colliding with paddle
            if (rightPaddle.Bounds.Intersects(gameBall.Bounds))
            {
                gameBall.Speed = new Point(gameBall.Speed.X + 1, gameBall.Speed.Y + 1);
                gameBall.Speed = new Point(-gameBall.Speed.X, -gameBall.Speed.Y);
            }
            #endregion

            #region Ball Logic
            ///
            /// Ball
            ///

            // Move the ball as fast as speed
            gameBall.Bounds = new Rectangle(new Point(gameBall.Bounds.X + gameBall.Speed.X, gameBall.Bounds.Y + gameBall.Speed.Y), new Point(gameBall.Bounds.Width, gameBall.Bounds.Height));

            // Check for collision on edge of screen
            // X
            if (gameBall.Bounds.X <= 0)
            {
                // P2 Wins!
                gameBall.Speed = new Point(-gameBall.Speed.X, gameBall.Speed.Y);
                Console.WriteLine("Player 2 scored!");
                if (!(P1Score >= 11))
                {
                    P2Score++;
                }
            }

            if (gameBall.Bounds.X + gameBall.Bounds.Width >= GraphicsDevice.Viewport.Width - 2)
            {
                // P1 Wins!
                gameBall.Speed = new Point(-gameBall.Speed.X, gameBall.Speed.Y);
                Console.WriteLine("Player 1 scored!");
                if (!(P2Score >= 11))
                {
                    P1Score++;
                }
            }

            // Y
            if (gameBall.Bounds.Y <= 0)
            {
                gameBall.Speed = new Point(gameBall.Speed.X, -gameBall.Speed.Y);
            }

            if (gameBall.Bounds.Y + gameBall.Bounds.Height >= GraphicsDevice.Viewport.Height)
            {
                gameBall.Speed = new Point(gameBall.Speed.X, -gameBall.Speed.Y);
            }
            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(mainFont, P1Score.ToString(), new Vector2(50, 10), Color.White);
            spriteBatch.DrawString(mainFont, P2Score.ToString(), new Vector2((GraphicsDevice.Viewport.Width - 50) - mainFont.MeasureString("00").X, 10), Color.White);
            spriteBatch.DrawString(mainFont, "X Speed: " + Math.Abs(gameBall.Speed.X) + " | Y Speed: " + Math.Abs(gameBall.Speed.Y), new Vector2(GraphicsDevice.Viewport.Width / 2 - (mainFont.MeasureString("X Speed: 00 | Y Speed: 00").X / 2), GraphicsDevice.Viewport.Height - 50), Color.White);

            if (P1Score >= 11)
            {
                spriteBatch.DrawString(mainFont, "Player 1 Wins! Press R to restart.", new Vector2((GraphicsDevice.Viewport.Width / 2) - mainFont.MeasureString("Player 1 Wins! Press R to restart.").X / 2, (GraphicsDevice.Viewport.Height / 2) - mainFont.MeasureString("Player 1 Wins! Press R to restart.").Y / 2), Color.White);
            }

            else if (P2Score >= 11)
            {
                spriteBatch.DrawString(mainFont, "Player 2 Wins! Press R to restart.", new Vector2((GraphicsDevice.Viewport.Width / 2) - mainFont.MeasureString("Player 2 Wins! Press R to restart.").X / 2, (GraphicsDevice.Viewport.Height / 2) - mainFont.MeasureString("Player 2 Wins! Press R to restart.").Y / 2), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
