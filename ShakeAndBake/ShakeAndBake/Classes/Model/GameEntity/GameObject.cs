using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakeAndBake.Model.GameEntity
{
    public abstract class GameObject
    {
        protected Texture2D sprite;
        public Texture2D Sprite
        {
            get { return this.sprite; }
            set { this.sprite = value; }
        }

        //Used to determine if object has been destroyed or not
        protected bool isDestroyed;
        public bool IsDestroyed
        {
            get { return this.isDestroyed; }
            set { this.isDestroyed = value; }
        }

        //Holds the current position of the object
        protected Vector2 position;
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        //The value that determines how fast the object moves from postion A to position B
        protected double velocity;
        public double Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }

        //The value that determines the acceleration of the object moving from postion A to position B
        protected double acceleration;
        public double Acceleration
        {
            get { return this.acceleration; }
            set { this.acceleration = value; }
        }

        //This is the radius from the objects "position" field that is used to determine if they have made contact with another gameobject
        protected double hitBoxRadius;
        public double HitBoxRadius
        {
            get { return this.hitBoxRadius; }
            set { this.hitBoxRadius = value; }
        }

        //This is for our composite structural design pattern.
        private List<GameObject> children;
        public List<GameObject> Children
        {
            get { return children; }
        }

        public void AddChild(GameObject obj)
        {
            children.Add(obj);
        }

        public void RemoveChild(GameObject obj)
        {
            children.Remove(obj);
        }

        public GameObject GetChild(int index)
        {
            return children[index];
        }

        //constructor
        public GameObject()
        {
            children = new List<GameObject>();
        }


        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void Update(GameTime gameTime, CollisionBoard cb) { }

        // checks if the game object is in the game window
        public bool isInWindow()
        {
            if (sprite == null) return false;
            int windowWidth = GameConfig.Width;
            int windowHeight = GameConfig.Height;
            if (position.X < 0 || position.Y < 0
                || position.X + sprite.Width > windowWidth
                || position.Y + sprite.Height > windowHeight)
            {
                return false;
            }
            return true;
        }

        public bool IsInWindowWidth()
        {
            if (sprite == null) return false;
            int windowWidth = GameConfig.Width;
            if (position.X < 0 || position.X + sprite.Width > windowWidth)
            {
                return false;
            }
            return true;
        }

        public bool IsInWindowHeight()
        {
            if (sprite == null) return false;
            int windowHeight = GameConfig.Height;
            if (position.Y < 0 || position.Y + sprite.Height > windowHeight)
            {
                return false;
            }
            return true;
        }

        // checks if pos is in the sprite texture bounds
        public bool BoundsContains(Vector2 coords)
        {
            if (coords.X >= position.X && coords.X <= position.X + sprite.Width &&
                coords.Y >= position.Y && coords.Y <= position.Y + sprite.Height)
            {
                return true;
            }
            return false;
        }
    }
}
