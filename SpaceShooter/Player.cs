using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    class Player : MovingObject
    {
        public Player(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }
            public void Update(GameWindow window)
            {
                KeyboardState keyboardState = Keyboard.GetState();

                if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
                {
                    if (keyboardState.IsKeyDown(Keys.Right))
                        vector.X += speed.X;
                    if (keyboardState.IsKeyDown(Keys.Left))
                        vector.X -= speed.X;
                }


                if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
                {
                    if (keyboardState.IsKeyDown(Keys.Down))
                        vector.Y += speed.Y;
                    if (keyboardState.IsKeyDown(Keys.Up))
                        vector.Y -= speed.Y;
                }

                if (vector.X < 0)
                    vector.X = 0;

                if (vector.X > window.ClientBounds.Width - texture.Width)
                    vector.X = window.ClientBounds.Width - texture.Width;

                if (vector.Y < 0)
                    vector.Y = 0;

                if (vector.Y > window.ClientBounds.Height - texture.Height)
                    vector.Y = window.ClientBounds.Height - texture.Height;
            }
    }

}
