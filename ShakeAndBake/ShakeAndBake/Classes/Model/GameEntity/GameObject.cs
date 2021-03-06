﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Controller.Collision;

namespace ShakeAndBake.Model.GameEntity
{
    public abstract class GameObject
    {
        protected Texture2D sprite;
        public virtual Texture2D Sprite
        {
            get { return this.sprite; }
            set
            {
                this.sprite = value;
                this.hitBoxRadius = sprite.Width / 2;
            }
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
        protected double acceleration = 1;
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
            return this.IsInWindowWidth() && this.IsInWindowHeight();
        }

        public bool IsInWindowWidth()
        {
            if (sprite == null) return false;
            float adjustedWidth = position.X + sprite.Width;
            if (position.X  < 0 || (adjustedWidth > GameConfig.Width))
            {
                return false;
            }
            return true;
        }

        public bool IsInWindowHeight()
        {
            if (sprite == null) return false;
            float adjustedHeight = position.Y + sprite.Height;
            if (position.Y < 0 || (adjustedHeight > GameConfig.Height))
            {
                return false;
            }
            return true;
        }
        
        public virtual Vector2 GetCenterCoordinates()
        {
            return new Vector2(this.Position.X + (float)this.Sprite.Width/2, this.Position.Y + this.Sprite.Height/2);
        }

        public bool BoundsContains(GameObject obj)
        {
            Vector2 centerCoordinates = obj.GetCenterCoordinates();

            return BoundsContains(centerCoordinates)
                || BoundsContains(new Vector2(centerCoordinates.X + obj.Sprite.Width / 2, centerCoordinates.Y))
                || BoundsContains(new Vector2(centerCoordinates.X - obj.Sprite.Width / 2, centerCoordinates.Y))
                || BoundsContains(new Vector2(centerCoordinates.X, centerCoordinates.Y + obj.Sprite.Height / 2))
                || BoundsContains(new Vector2(centerCoordinates.X, centerCoordinates.Y - obj.Sprite.Height / 2));
        }
        // checks if pos is in the sprite texture bounds
        public bool BoundsContains(Vector2 coords)
        {
            if (
                (coords.X >= GetCenterCoordinates().X - sprite.Width/2 && coords.X <= GetCenterCoordinates().X + sprite.Width/2)
                &&
                (coords.Y >= GetCenterCoordinates().Y - sprite.Height/2 && coords.Y <= GetCenterCoordinates().Y + sprite.Height/2)
                )
            {
                return true;
            }
            return false;
        }
    }
}
