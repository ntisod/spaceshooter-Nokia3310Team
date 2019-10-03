using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        Enemy enemy;
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
            player = new Player(Content.Load<Texture2D>("Sprites/ship"), 380, 400, 2.5f, 4.5f);
            printText = new PrintText(Content.Load<SpriteFont>("Fonts/myFont"));
            enemy = new Enemy(Content.Load<Texture2D>(""), 0, 0);
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
            enemy.Update(Window);



            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            enemy.Draw(spriteBatch);
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
            printText.Print("Test", spriteBatch, 0, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
