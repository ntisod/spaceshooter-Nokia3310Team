using System;
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
    class MenuItem
    {
        Texture2D texture;
        Vector2 position;
        int currentState;

        public MenuItem(Texture2D texture, Vector2 position, int currentState)
        {
            this.texture = texture;
            this.position = position;
            this.currentState = currentState;
        }

        public Texture2D Texture { get { return texture;  } }
        public Vector2 Position { get { return Position; } }
        public int State { get { return currentState; } }
    }

    class Menu
    {
        List<MenuItem> menu; // Lista på menuItems
        int selected = 0; // Första valet i listan är valt

        // currenHeight används för att rita ut menyItems på olika höjd:
        float currentHeight = 0;
        // lastChange används för att "pausa" tangenttryckningar, så att det
        // inte ska gå för fort att bläddra bland menyvalen:
        double lastChange = 0;
        // lastState används för spara den föregående tangenttryckningen. Vi
        // vill ju inte pausa ifall användaren byter riktning i menyn, därför
        // kan vi spara den föregående tangenttryckningen:

        int defaultMenuState; // det state som representerar själva menyn
        // =======================================================================
        // Menu(), konstruktor som skapar listan med MenuItem:s
        // =======================================================================
        public Menu(int defaultMenuState)
        {
            menu = new List<MenuItem>();
            this.defaultMenuState = defaultMenuState;
        }

        // =======================================================================
        // AddItem(), lägger till ett menyval i listan
        // =======================================================================
        public void AddItem(Texture2D itemTexture, int state)
        {
            // Sätt höjd på item:
            float X = 0;
            float Y = 0 + currentHeight;

            // Ändra currentHeight efter detta items höjd + 20 pixlar för lite
            // exra mellanrum:
            currentHeight += itemTexture.Height + 20;

            // Skapa ett temporärt objekt och lägg det i listan:
            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add(temp);
        }

        // =======================================================================
        // Update(), kollar om användaren tryckt någon tangent. Antingen kan pil-
        // tangenterna användas för att välja en viss MenuItem (utan att gå in i
        // just det valet) eller så kan ENTER användas för att gå in i den valda
        // MenuItem:en.
        // =======================================================================
        public int Update(GameTime gameTime)
        {
            // Läs in tangenttryckningar:
            KeyboardState keyboardState = Keyboard.GetState();

            // Byte mellan olika menyval. Först måste vi dock kontrollera så att
            // användaren inte precis nyligen bytte menyval. Vi vill ju inte att
            // det ska ändras 30 eller 60 gånger per sekund! Därför pausar vi i
            // 130 milisekunder. Om användaren trycker en ANNAN (dvs om han byter
            // "rikning" i menyn) så ska vi dock läsa in tangenten:
            if (lastChange + 130 < gameTime.TotalGameTime.TotalMilliseconds)
            {
                // Gå ett steg ned i menyn
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    selected++;
                    // Om vi har gått utanför de möjliga valen, så vill vi att
                    // det första menyvalet ska väljas:
                    if (selected > menu.Count - 1)
                        selected = 0; // Det första menyvalet
                }
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    selected--;
                    // Om vi har gått utanför de möjliga valen (alltså negativa
                    // siffror), så vill vi att det sista menyvalet ska väljas:
                    if (selected < 0)
                        selected = menu.Count - 1; // Det sista menyvalet
                }

                // Ställ lastChange till exakt detta ögonblick:
                lastChange = gameTime.TotalGameTime.TotalMilliseconds;
            }

            // Välj ett menyval med ENTER:
            if (keyboardState.IsKeyDown(Keys.Enter))
                return menu[selected].State; // Returnera menyvalets state

            // Om inget menyval har valts, så stannar vi kvar i menyn:
            return defaultMenuState;
        }

        // =======================================================================
        // Draw(), ritar ut menyn.
        // =======================================================================
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < menu.Count; i++)
            {
                // Om vi har ett aktivt menyval ritar vi ut det med en speciell
                // färgtoning:
                if (i == selected)
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.RosyBrown);
                else // Annars ingen färgtoning alls:
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.White);
            }

        }
    }
}
