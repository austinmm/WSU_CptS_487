using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using System.Collections.Generic;
using ShakeAndBake.Extras.Paths;
using System;

namespace ShakeAndBake.Model.GameEntity
{
    public abstract class Projectile : GameObject
    {
        //This is a set path that the projectile objects will travel when update is called
        protected Path path;
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

        //This will be used to determine if and how this projectile will move other projectiles during a collision
        protected int mass = 1;//default
        public int Mass
        {
            get { return this.mass; }
            set { this.mass = value; }
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
            //Before updating position, remove current bucket position on collision board
            cb.RemoveFromBucketIfExists(this);
            //Update the position of the projectile based off of its spritePath
            position = path.NextPoint();
            //Fill bucket on collision board
            cb.FillBucket(this);
        }
        public bool HasBeenFired() { return this.path.HasMoved(); }

        protected virtual void HandleProjectileCollision(PlayerBullet other) { return; }
        protected virtual void HandleProjectileCollision(EnemyBullet other) { return; }

        public abstract Projectile Clone();
    }

    public class PlayerBullet : Projectile
    {
        public PlayerBullet(Path path, string texture) : base(path, texture) { }

        public override Projectile Clone()
        {
            PlayerBullet copy = new PlayerBullet(this.path, this.texture);
            copy.Acceleration = this.acceleration;
            copy.HitDamage = this.HitDamage;
            copy.Mass = this.mass;
            copy.PathType = this.pathType;
            copy.Sprite = this.sprite;
            copy.Texture = this.texture;
            copy.Velocity = this.velocity;
            copy.position = this.position;
            copy.Path = this.path;
            return copy;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, position, Color.White);
        }
        
        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            base.Update(gameTime, cb);

            HashSet<GameObject> collidedEnemies = cb.GetObjectsCollided(this, typeof(Enemy));
            HashSet<GameObject> collidedBullets = cb.GetObjectsCollided(this, typeof(EnemyBullet));
            //collision with enemy
            foreach (Enemy enemyObject in collidedEnemies)
            {
                if (enemyObject.IsDestroyed) { continue; }
                enemyObject.TakeDamage(this.HitDamage);
                this.IsDestroyed = true;
            }
            //collision with enemy bullets
            foreach (EnemyBullet enemyBullet in collidedBullets)
            {
                if (this.isDestroyed) { break; }
                else if (enemyBullet.IsDestroyed) { continue; }
                this.HandleProjectileCollision(enemyBullet);
            }

            if (this.IsDestroyed)
            {
                cb.RemoveFromBucketIfExists(this);
            }
        }

        protected override void HandleProjectileCollision(EnemyBullet other)
        {
            //Destoy smaller massed projectile and reduce mass of larger projectile
            if (this.mass > other.Mass)
            {
                other.IsDestroyed = true;
                this.mass -= other.Mass;
            }
            else if (this.mass < other.Mass)
            {
                this.IsDestroyed = true;
                other.Mass -= this.mass;
            }
            else
            {
                this.IsDestroyed = other.IsDestroyed = true;
            }
        }

    }

    public class EnemyBullet : Projectile
    {
        public EnemyBullet(Path path, string texture) : base(path, texture) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, position, Color.White);
        }

        public override Projectile Clone()
        {
            EnemyBullet copy = new EnemyBullet(this.path, this.texture);
            copy.Acceleration = this.acceleration;
            copy.HitDamage = this.HitDamage;
            copy.Mass = this.mass;
            copy.PathType = this.pathType;
            copy.Sprite = this.sprite;
            copy.Texture = this.texture;
            copy.Velocity = this.velocity;
            copy.position = this.position;
            copy.Path = this.path;
            return copy;
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            base.Update(gameTime, cb);
            HashSet<GameObject> collidedBullets = cb.GetObjectsCollided(this, typeof(EnemyBullet));
            //player takes damage from enemy bullet
            if (Player.Instance.BoundsContains(this) || this.BoundsContains(Player.Instance))
            {
                Player.Instance.TakeDamage(this.HitDamage);
                this.isDestroyed = true;
            }
            //check if enemy projectiles collide with other enemy projectiles
            foreach (EnemyBullet go in collidedBullets)
            {
                if (this.isDestroyed) { break; }
                else if (go.IsDestroyed) { continue; }
                this.HandleProjectileCollision(go);
            }
            if (this.IsDestroyed)
            {
                cb.RemoveFromBucketIfExists(this);
            }
        }

        protected override void HandleProjectileCollision(EnemyBullet other)
        {
            //Push smaller massed projectile out of way
            if (this.mass > other.Mass)
            {
                other.path = new StraightPath(other.Position, this.path.NextPoint(), (float)this.velocity);
            }
        }
    }
}