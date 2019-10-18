using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    // =======================================================================
    // Player, klass för att skapa ett spelarobjekt. Klassen ska hantera spel-
    // arens rymdskepp (sprite) och ta emot tangenttryckningar för att ändra
    // rymdskeppets position.
    // =======================================================================
    class Player : PhysicalObject
    {
        List<Bullet> bullets; // Alla skott
        Texture2D bulletTexture; // skottets bild
        double timeSinceLastBullet = 0; // I milisekunder
        int points = 0; // Spelarens poäng
        // =======================================================================
        // Player(), konstruktor för att skapa spelar-objektet
        // =======================================================================
        public Player(Texture2D texture, float X, float Y, float speedX, float speedY,
              Texture2D bulletTexture)
            : base(texture, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
        }

        // =======================================================================
        // Update(), flyttar på spelaren
        // =======================================================================
        public void Update(GameWindow window, GameTime gameTime)
        {
            // Läs in tangenttryckningar:
            KeyboardState keyboardState = Keyboard.GetState();

            // Flytta rymdskeppet efter tangenttryckningar (om det inte är på
            // väg ut från kanten):
            if (vector.X <= window.ClientBounds.Width - texture.Width
                && vector.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                    vector.X += speed.X;
                if (keyboardState.IsKeyDown(Keys.Left))
                    vector.X -= speed.X;
            }
            if (vector.Y <= window.ClientBounds.Height - texture.Height
                && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                    vector.Y += speed.Y;
                if (keyboardState.IsKeyDown(Keys.Up))
                    vector.Y -= speed.Y;
            }

            // Kontrollera ifall rymdskeppet har åkt  ut från kanten, om det har
            // det, så återställ dess position.
            // Har det åkt ut till vänster:
            if (vector.X < 0)
                vector.X = 0;
            // Har det åkt ut till höger:
            if (vector.X > window.ClientBounds.Width - texture.Width)
                vector.X = window.ClientBounds.Width - texture.Width;
            // Har det åkt ut upptill:
            if (vector.Y < 0)
                vector.Y = 0;
            // Har det åkt ut nedtill:
            if (vector.Y > window.ClientBounds.Height - texture.Height)
                vector.Y = window.ClientBounds.Height - texture.Height;

            // Spelaren vill skjuta!
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                // Kontrollera om spelaren FÅR skjuta:
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 200)
                {
                    // Skapa skottet:
                    Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width / 2,
                                             vector.Y);
                    bullets.Add(temp); // Lägg till skottet it listan
                    // Sätt timeSinceLastBullet till detta ögonblick:
                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            // Flytta på alla skott
            foreach (Bullet b in bullets.ToList())
            {
                // Flytta på skottet:
                b.Update();
                // Kontrollera så att skottet inte är "dött"
                if (!b.IsAlive)
                    bullets.Remove(b); // Ta bort skottet ur listan
            }

        }

        // =======================================================================
        // Draw(), ritar ut bilden på skärmen
        // =======================================================================
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            foreach (Bullet b in bullets)
                b.Draw(spriteBatch);
        }
        // Get- och set-egenskaper för poäng:
        public int Points { get { return points; } set { points = value; } }

        // Get-egenskap för alla skott:
        public List<Bullet> Bullets { get { return bullets; } }
    }

    // =======================================================================
    // Bullet, en klass för att skapa skott
    // =======================================================================
    class Bullet : PhysicalObject
    {
        // =======================================================================
        // Bullet(), konstruktor för att skapa ett skott-objekt
        // =======================================================================
        public Bullet(Texture2D texture, float X, float Y)
            : base(texture, X, Y, 0, 3f)
        {
        }

        // =======================================================================
        // Update(), Uppdaterar skottets position och tar bort det om det åker
        // utanför fönstret.
        // =======================================================================
        public void Update()
        {
            vector.Y -= speed.Y;
            if (vector.Y < 0)
                isAlive = false;
        }
    }

}