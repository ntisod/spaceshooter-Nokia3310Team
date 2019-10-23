using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceShooter
{
    // Enemy - fiende
    abstract class Enemy : PhysicalObject
    {

        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y, speedX, speedY)
        {
        }
        public abstract void Update(GameWindow window);
    }


    // Mine - mina 
 
    class Mine : Enemy
    {
        public Mine(Texture2D texture, float X, float Y)
            : base(texture, X, Y, 6f, 0.3f)
        {
        }


        public override void Update(GameWindow window)
        {
            // Flytta på fienden:
            vector.X += speed.X;
            // Kontrollera så fienden inte åker utanför fönstret på sidorna
            if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
                speed.X *= -1; // Byt riktning på fienden
            vector.Y += speed.Y;
            // Gör fienden inaktiv om den åker ut där nere
            if (vector.Y > window.ClientBounds.Height)
                isAlive = false;

            //kommentarer från boken :-)
        }
    }


    // Tripod - en typ av fiende

    class Tripod : Enemy
    {
        public Tripod(Texture2D texture, float X, float Y)
            : base(texture, X, Y, 0f, 3f)
        {
        }
        public override void Update(GameWindow window)
        {
            // Flytta på fienden:
            vector.Y += speed.Y;
            // Gör fienden inaktiv om den åker ut där nere
            if (vector.Y > window.ClientBounds.Height)
                isAlive = false;
        }
    }

    //Tim Boss

    class TimBoss : Enemy
    {
        public TimBoss(Texture2D texture, float X, float Y)
            : base(texture, X, Y, 3f, 0f)
        {
        }
        public override void Update(GameWindow window)
        {

            // Flytta på fienden:
            vector.X += speed.X;
            // Kontrollera så fienden inte åker utanför fönstret på sidorna
            if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
                speed.X *= -1; // Byt riktning på fienden

            if (vector.Y > window.ClientBounds.Height - texture.Height || vector.Y < 0)
                speed.Y *= -1; // Byt riktning på fienden

            vector.Y += speed.Y;
            // Gör fienden inaktiv om den åker ut där nere
            if (vector.Y > window.ClientBounds.Height)
                isAlive = false;
        }
    }


}