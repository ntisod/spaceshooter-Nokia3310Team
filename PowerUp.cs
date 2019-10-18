using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{   
    // GoldCoin är gold coins, bruh
    class GoldCoin : PhysicalObject
    {
        double timeToDie; // Tid tills GoldCoin dör
        public GoldCoin(Texture2D texture, float X, float Y, GameTime gameTime)
            : base(texture, X, Y, 0, 2f)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;
        }     
        public void Update(GameTime gameTime)
        {
            // tiden = 0 -> GoldCoin = dead
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
                isAlive = false;
        }
    }

}