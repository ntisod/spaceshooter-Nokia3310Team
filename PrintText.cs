using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{   
    // PrintText - skapar texten på skärmen
    class PrintText
    {
        SpriteFont font;
        public PrintText(SpriteFont font)
        {
            this.font = font;
        }
        public void Print(string text, SpriteBatch spriteBatch, int X, int Y)
        {
            spriteBatch.DrawString(font, text, new Vector2(X, Y), Color.White);
        }
    }
}