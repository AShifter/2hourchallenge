using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.GameElements
{
    public class Paddle : IPongElement
    {
        ///
        /// Local Variables
        /// 

        Rectangle bounds;
        Color color;
        SpriteBatch spriteBatch;
        ContentManager content;

        ///
        /// Inherited Properties
        ///

        // Bounds (from IPongElement)
        public Rectangle Bounds
        {
            get => bounds;
            set => bounds = value;
        }

        // Color (from IPongElement)
        public Color Color
        {
            get => color;
            set => color = value;
        }

        public SpriteBatch SpriteBatch
        {
            get => spriteBatch;
            set => spriteBatch = value;
        }

        public ContentManager Content
        {
            get => content;
            set => content = value;
        }

        public Point Speed
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        // Enabled (from IUpdateable)
        // Nothing fancy here; I'm not worried about ever changing this, so I'm happy hardcoding it.
        public bool Enabled => true;

        // UpdateOrder (from IUpdateable)
        public int UpdateOrder => 0;

        // DrawOrder (from IUpdateable)
        public int DrawOrder => 0;

        // Enabled (from IUpdateable)
        // Nothing fancy here either; I'm not worried about ever changing this, so I'm happy hardcoding it. If I ever do want to change this, it won't be hard to change.
        public bool Visible => true;

        // EnabledChanged (from IUpdateable)
        public event EventHandler<EventArgs> EnabledChanged;

        // UpdateOrderChanged (from IUpdateable)
        public event EventHandler<EventArgs> UpdateOrderChanged;

        // DrawOrderChanged (from IDrawable)
        public event EventHandler<EventArgs> DrawOrderChanged;

        // VisibleChanged (from IDrawable)
        public event EventHandler<EventArgs> VisibleChanged;

        // Initialize() (from IGameComponent)
        public void Initialize()
        {
            
        }

        // Update() (from IUpdateable)
        public void Update(GameTime gameTime)
        {
            
        }

        // Draw() (from IDrawable)
        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(content.Load<Texture2D>("Images/Pixel"), bounds, color);
            spriteBatch.End();
        }
    }
}
