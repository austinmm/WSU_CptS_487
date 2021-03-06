﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Controller.Collision;

namespace ShakeAndBake.Model.GameEntity
{
    public abstract class Projectile : GameObject
    {
        //This is a set path that the projectile objects will travel when update is called
        protected Path path;

        protected bool isBouncy;
        public bool IsBouncy
        {
            get { return this.isBouncy; }
            set { this.isBouncy = value; }
        }

        protected double density;
        public double Density
        {
            get { return this.density; }
            set { this.density = value; }
        }

        public Path Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        protected PathType pathType;
        public PathType PathType
        {
            get { return this.pathType; }
            set { this.pathType = value; }
        }

        protected string texture;
        public string Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public override Texture2D Sprite
        {
            get
            {
                if (this.sprite == null)
                {
                    sprite = ShakeAndBakeGame.GetTexture(this.texture);
                    this.hitBoxRadius = sprite.Width / 2;
                }
                return this.sprite;
            }
            set {
                this.sprite = value;
                this.hitBoxRadius = sprite.Width / 2;
            }
        }


        //This is the amount of damage an enemy or player will take if hit by this projectile
        private int hitDamage;
        public int HitDamage
        {
            get { return this.hitDamage; }
            set { this.hitDamage = value; }
        }

        //Projectile Constructor
        public Projectile(Path path, string texture)
        {
            this.path = path;
            this.texture = texture;
            this.Sprite = ShakeAndBakeGame.GetTexture(this.texture);
            this.IsBouncy = false;
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            if (this.IsDestroyed) { return; }
            //If the projectile has gone off screen then it is destroyed
            if (!this.isInWindow())
            {
                IsDestroyed = true;
                return;
            }

            cb.UdateObjectPositionWithFunction(this, () => { position = path.NextPoint(); return true; });
        }
        public bool HasBeenFired() { return this.path.HasMoved(); }

        public abstract Projectile Clone();
    }
}
