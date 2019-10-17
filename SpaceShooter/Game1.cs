﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpaceShooter
{
    /// <summary>
    /// This is the main type for your game. 
    /// 
    /// Artur Jekimov, Te17 i kursen Programmering 2 2019-20.
    /// 
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        List <Enemy> enemies;
        PrintText printText;
        Texture2D texture;
        Vector2 vector;
        Vector2 speed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
 
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            player = new Player(Content.Load<Texture2D>("images/player/ship"), 380, 400, 2.5f,
                    4.5f);


           
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
        }


        protected override void UnloadContent()
        {
            
        }


       
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(Window);
            foreach (Enemy e in enemies)
            {
                if (e.IsAlive)
                    e.Update(Window);
                else
                    enemies.Remove(e);
            }
                



            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
            //grafik
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
            printText.Print("Test", spriteBatch, 0, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
