﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    static class GameElement
    {
        static Texture2D menuSprite;
        static Vector2 menuPos;
        static Player player;
        static List<Enemy> enemies;
        static List<GoldCoin> goldCoins;
        static Texture2D goldCoinSprite;
        static PrintText printText;

        // Olika Gamestates :
        public enum State { Menu, Run, Highscore, Quit };
        public static State currentState;

        // Initialize()

        public static void Initialize()
        {
            goldCoins = new List<GoldCoin>();
        }

        /*
        //skapar fiender, funkar inte
        public static void GenerateEnemies(ContentManager content, GameWindow window)
        {
            Random random = new Random();
            Texture2D tmpSprite = content.Load<Texture2D>("images/enemies/mine");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Mine temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp); // lägg till listan
            }

            tmpSprite = content.Load<Texture2D>("images/enemies/tripod");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Tripod temp = new Tripod(tmpSprite, rndX, rndY);
                enemies.Add(temp); // Lägg till listan
            }

            tmpSprite = content.Load<Texture2D>("images/enemies/TimBoss");
            for (int i = 0; i < 1; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                TimBoss temp = new TimBoss(tmpSprite, rndX, rndY);
                enemies.Add(temp); // Lägg till i listan
            }
        }
        */

        // LoadContent()

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            menuSprite = content.Load<Texture2D>("images/menu");
            menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
            menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;
            player = new Player(content.Load<Texture2D>("images/player/ship"), 380, 400, 4.5f, 4.5f, content.Load<Texture2D>("images/player/bullet"));

            //Fiender
            enemies = new List<Enemy>();
            Random random = new Random();
            Texture2D tmpSprite = content.Load<Texture2D>("images/enemies/mine");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Mine temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp); // lägg till listan
            }

            tmpSprite = content.Load<Texture2D>("images/enemies/tripod");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Tripod temp = new Tripod(tmpSprite, rndX, rndY);
                enemies.Add(temp); // Lägg till listan
            }

            tmpSprite = content.Load<Texture2D>("images/enemies/TimBoss");
            for (int i = 0; i < 1; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                TimBoss temp = new TimBoss(tmpSprite, rndX, rndY);
                enemies.Add(temp); // Lägg till i listan
            }

            goldCoinSprite = content.Load<Texture2D>("images/powerups/coin");
            printText = new PrintText(content.Load<SpriteFont>("images/myFont"));
        }

        private static void Reset(GameWindow window, ContentManager content)
        {
            player.Reset(380, 400, 4.5f, 4.5f);

            enemies.Clear();
            Random random = new Random();
            Texture2D tmpSprite = content.Load<Texture2D>("images/enemies/mine");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Mine temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp); // lägg till listan
            }

            tmpSprite = content.Load<Texture2D>("images/enemies/tripod");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Tripod temp = new Tripod(tmpSprite, rndX, rndY);
                enemies.Add(temp); // Lägg till listan
            }

            tmpSprite = content.Load<Texture2D>("images/enemies/TimBoss");
            for (int i = 0; i < 1; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                TimBoss temp = new TimBoss(tmpSprite, rndX, rndY);
                enemies.Add(temp); // Lägg till i listan
            }
        }

        // MenuUpdate() - menu val

        public static State MenuUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S)) // Startar spelet
                return State.Run;
            if (keyboardState.IsKeyDown(Keys.H)) // Visa HighScore listan
                return State.Highscore;
            if (keyboardState.IsKeyDown(Keys.A)) // Avsluta spelet
                return State.Quit;

            return State.Menu; // Stanna kvar i Menu
        }

        // MenuDraw() - ritar menu 

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuSprite, menuPos, Color.White);
        }

        // RunUpdate() - update metod för själva spelet

        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {

            if (!player.IsAlive)
            {
                Reset(window, content);
                return State.Menu;
            }

            // updatera spelarens position:
            player.Update(window, gameTime);
            // Går genom alla fiender
            foreach (Enemy e in enemies.ToList())
            {
                // Kontrollera om finden kolliderar med ett skott

                foreach (Bullet b in player.Bullets)
                {
                   if(e.CheckCollision(b))
                    e.IsAlive = false;  //Döda fiender
                    player.Points++; // Ge spelaren poäng
                }


                if (e.IsAlive) // kollar om fiende lever
                {
                    // kontrollerar kollision med spelaren 
                    if (e.CheckCollision(player))
                        player.IsAlive = false;
                    e.Update(window);
                }
                else // ta bort fienden för den är död
                    enemies.Remove(e);
            }

            Random random = new Random();
            int newCoin = random.Next(1, 200);
            if (newCoin == 1) // nytt goldcoin
            {
                // Var ska guldmyntet uppstå:
                int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - goldCoinSprite.Height);
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

            if (!player.IsAlive) // you died, thanks obama
                return State.Menu; // Tillbaka till menu

            return State.Run; // Stanna kvar i Run 
        }

        // RunDraw() - ritar (rendrar) spelet

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch); // Rita ut spelaren
            // Rita ut alla fiender:
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
            // Rita ut alla GoldCoins:
            foreach (GoldCoin gc in goldCoins)
                gc.Draw(spriteBatch);
            printText.Print("points:" + player.Points, spriteBatch, 0, 0);
        }

        // HighScoreUpdate() - metod för highscore lista

        public static State HighScoreUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            //if click(esc); -> go back
            if (keyboardState.IsKeyDown(Keys.Escape))
                return State.Menu;
            return State.Highscore;
        }

        // HighScoreDraw() - metod för att rita highscore lista

        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {
            //kod...
        }
    }

}
