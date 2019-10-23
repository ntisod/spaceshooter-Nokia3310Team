using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{

    // Game1 - den gör i princip allt

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics; // Hanterar grafiken
        SpriteBatch spriteBatch; // Ritar bilder
        Player player; // Spelare
        List<Enemy> enemies; // Lista för fiender
        List<GoldCoin> goldCoins; // Lista för goldcoins
        Texture2D goldCoinSprite; // goldcoins textur


        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       //gör nåt när spelet startas
        protected override void Initialize()
        {
            goldCoins = new List<GoldCoin>();
            base.Initialize();
        }

       //ladda objekter
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Skapa spelaren:
            player = new Player(Content.Load<Texture2D>("images/player/ship"), 380, 400, 25f,
                    45f, Content.Load<Texture2D>("images/player/bullet"));


            // Skapa fiender
            enemies = new List<Enemy>();
            Random random = new Random();
            Texture2D tmpSprite = Content.Load<Texture2D>("images/enemies/mine");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height / 2);
                Mine temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp); // Lägg till i listan
            }

            tmpSprite = Content.Load<Texture2D>("images/enemies/tripod");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height / 2);
                Tripod temp = new Tripod(tmpSprite, rndX, rndY);
                enemies.Add(temp); // Lägg till i listan
            }

            tmpSprite = Content.Load<Texture2D>("images/enemies/TimBoss");
            for (int i = 0; i < 1; i++)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height / 2);
                TimBoss temp = new TimBoss(tmpSprite, rndX, rndY);
                enemies.Add(temp); // Lägg till i listan
            }


            // Ladda in bild för guldmynt:
            goldCoinSprite = Content.Load<Texture2D>("images/powerups/coin");
        }
        // rensar minne,typ
        protected override void UnloadContent()
        {

        }

       //game loop
        protected override void Update(GameTime gameTime)
        {
            // Stänger av spelet om man trycker på Back-knappen på gamepaden.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            player.Update(Window, gameTime); // Uppdatera spelarens position

            // Gå igenom alla fiender
            foreach (Enemy e in enemies.ToList())
            {
                // Kontrollera om fienden kolliderar med ett skott
                foreach (Bullet b in player.Bullets)
                {
                    if (e.CheckCollision(b)) // Kollision uppstod
                    {
                        e.IsAlive = false; // Döda fienden
                        player.Points++; // Ge spelaren poäng
                    }
                }
                if (e.IsAlive) // Kontrollera om fienden lever
                {
                    // Kontrollera kollision med spelaren:
                    if (e.CheckCollision(player))
                        this.Exit();
                    e.Update(Window); // Flytta på dem
                }
                else // Ta bort fienden för den är död
                    enemies.Remove(e);
            }
            // Guldmynten ska uppstå slumpmässigt, en chans på 200:
            Random random = new Random();
            int newCoin = random.Next(1, 200);
            if (newCoin == 1) // ok, nytt guldmynt ska uppstå
            {
                // Var ska guldmyntet uppstå:
                int rndX = random.Next(0, Window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height - goldCoinSprite.Height);
                // Lägg till myntet i listan:
                goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }
            // Gå igenom hela listan med existernade guldmynt
            foreach (GoldCoin gc in goldCoins.ToList())
            {
                if (gc.IsAlive) // Kontrollera om guldmyntet lever
                {
                    // gc.Update() kollar om guldmyntet har blivit för gammalt
                    // för att få leva vidare:
                    gc.Update(gameTime);

                    // Kontrollera om de kolliderat med spelaren:
                    if (gc.CheckCollision(player))
                    {
                        goldCoins.Remove(gc); 
                        player.Points++; 
                    }
                }
                else // Ta bort guldmyntet för det är dött
                    goldCoins.Remove(gc);
            }
            base.Update(gameTime);
        }  
        // Draw(), Här ritas själva spelet ut.
        // gameTime, används för att hålla koll på spelets uppdateringsfrekvens.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // Rensa skärmen
            // Använd spriteBatch för att rita ut saker på skärmen
            spriteBatch.Begin();
            player.Draw(spriteBatch); // Rita ut spelaren
            // Rita ut alla fiender:
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
            // Rita ut alla GoldCoins:
            foreach (GoldCoin gc in goldCoins)
                gc.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}