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
            GameElement.currentState = GameElement.State.Menu;
            GameElement.Initialize();
            base.Initialize();
        }

       //ladda objekter
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElement.LoadContent(Content, Window);
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

            switch (GameElement.currentState)
            {
                case GameElement.State.Run:
                    GameElement.currentState = GameElement.RunUpdate(Content, Window, gameTime);
                    break;
                case GameElement.State.Highscore:
                    GameElement.currentState = GameElement.HighScoreUpdate();
                    break;
                case GameElement.State.Quit:
                    this.Exit();
                    break;
                default:
                    GameElement.currentState = GameElement.MenuUpdate();
                    break;

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
            
            switch (GameElement.currentState)
            {
                case GameElement.State.Run:
                    GameElement.RunDraw(spriteBatch);
                    break;
                case GameElement.State.Highscore:
                    GameElement.HighScoreDraw(spriteBatch);
                    break;
                case GameElement.State.Quit:
                    this.Exit();
                    break;
                default:
                    GameElement.MenuDraw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}