using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.GameElements
{
    interface IPongElement : IGameComponent, IUpdateable, IDrawable
    {
        // Made this so every class doesn't have to implement DrawableGameComponent as well as another interface if I ever need to add more here
        
        // I want to follow XNA's Component Based Design Model.

        Rectangle Bounds { get; set; }
        Color Color { get; set; }
        SpriteBatch SpriteBatch { get; set; }
        ContentManager Content { get; set; }
        Point Speed { get; set; }
    }
}
