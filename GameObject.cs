using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{  
    class GameObject
    {
        protected Texture2D texture; // spaceship textur
        protected Vector2 vector; // spaceship koordinater
  
        // GameObject() - konstruktor 
    
        public GameObject(Texture2D texture, float X, float Y)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
        }
        // Draw() - "gör grafiken"
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }
        // Egenskaper för GameObject
        public float X { get { return vector.X; } }
        public float Y { get { return vector.Y; } }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }
    }
    // MovingObject - rörelse
    abstract class MovingObject : GameObject
    {
        protected Vector2 speed; // hastighet
        // MovingObject() - konstruktor

        public MovingObject(Texture2D texture, float X, float Y, float speedX,
                            float speedY)
            : base(texture, X, Y)
        {
            this.speed.X = speedX;
            this.speed.Y = speedY;

        }
    }
    // PhysicalObject - fysik mellan objeckterna så dem kan inte gå i varandra
    abstract class PhysicalObject : MovingObject
    {
        protected bool isAlive = true;

        public PhysicalObject(Texture2D texture, float X, float Y, float speedX,
                              float speedY)
            : base(texture, X, Y, speedX, speedY)
        {
        }
        // CheckCollision() - kollar om objekten gå in varandra
        public bool CheckCollision(PhysicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X),
                Convert.ToInt32(Y), Convert.ToInt32(Width),
                Convert.ToInt32(Height));
            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X),
                Convert.ToInt32(other.Y), Convert.ToInt32(other.Width),
                Convert.ToInt32(other.Height));
            return myRect.Intersects(otherRect);
        }
        // leva eller inte leva
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    }
}