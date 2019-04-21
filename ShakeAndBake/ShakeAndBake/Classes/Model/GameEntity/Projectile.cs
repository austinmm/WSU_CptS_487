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
        protected Boolean isBouncy;
        public Boolean IsBouncy
        {
            get { return this.isBouncy; }
            set { this.isBouncy = value; }
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
            //Before updating position, remove current bucket position on collision board
            cb.RemoveFromBucketIfExists(this);
            //Update the position of the projectile based off of its spritePath
            position = path.NextPoint();
            //Fill bucket on collision board
            cb.FillBucket(this);
        }
        public bool HasBeenFired() { return this.path.HasMoved(); }

        protected void HandleProjectileCollision(Projectile other)
        {
                /* Deflect once */
                if (this.Path.WasDeflected)
                {
                   
                    return;
                }

                if (!other.isBouncy && !this.isBouncy)
                {
                    this.isDestroyed = true;
                    other.isDestroyed = true;
                }
                float m1 = (float)(this.Sprite.Width * this.Sprite.Height);
                float m2 = (float)(other.Sprite.Width * other.Sprite.Height);

                Vector2 P = m1 * this.Path.GetVelocityVector()
                    + m2 * other.Path.GetVelocityVector();

                Vector2 C = this.Path.GetVelocityVector()
                    - other.Path.GetVelocityVector();

                // P = m1v1' + m2v2'
                // v1 - v2 = v2' - v1'
                // let c = v1 - v2

                // m1v1' + m2v2' = P
                // -v1'  +  v2'  = C

                // m1v1' + m2v2' = P
                // -m1v1'+ m1v2' = m1C

                // (m1 + m2)v2' = P + m1C

                // v2' = (P + m1C)/(m1 + m2)

                Vector2 v2f = (P + m1 * C) / (m1 + m2);

                // c = v2' - v1'

                // v1' = v2' - c

                Vector2 v1f = v2f - C;

               
                // perfecly elastic for now
                float heatLoss = 1f;

                float maxLength = Math.Max(other.Path.GetVelocityVector().Length(), this.Path.GetVelocityVector().Length()) * 1.5f;
                // normalize vectors if too fast!
                if (v2f.Length() > maxLength)
                {
                    v2f.Normalize();
                    v2f = v2f * maxLength;
                }

                if (v1f.Length() > maxLength)
                {
                    v1f.Normalize();
                    v1f = v1f * maxLength;
                }

                other.Path.SetVelocityVector(v2f * heatLoss);
                this.Path.SetVelocityVector(v1f * heatLoss);
        }

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

            // player hits itself from defelcted bullet
            if ((Player.Instance.BoundsContains(this) || this.BoundsContains(Player.Instance)) && this.Path.WasDeflected == true)
            {
                Player.Instance.TakeDamage(this.HitDamage);
                this.isDestroyed = true;
            }

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
            HashSet<GameObject> collidedEnemies = cb.GetObjectsCollided(this, typeof(Enemy));


            //player takes damage from enemy bullet
            if (Player.Instance.BoundsContains(this) || this.BoundsContains(Player.Instance))
            {
                Player.Instance.TakeDamage(this.HitDamage);
                this.isDestroyed = true;
            }

            // see if deflected bullet hits enemy bullet
            foreach (EnemyBullet go in collidedBullets)
            {
                if (this.isDestroyed) { break; }
                else if (go.IsDestroyed || go == this) { continue; }
                if (this.Path.WasDeflected)
                {
                    this.isDestroyed = true;
                    go.isDestroyed = true;
                }

            }
            // see if deflected enemy bullet hits an enemy
            foreach (Enemy go in collidedEnemies)
            {
                if (this.isDestroyed) { break; }
                else if (go.IsDestroyed) { continue; }
                if (this.Path.WasDeflected)
                {
                    go.TakeDamage(this.HitDamage);
                    this.isDestroyed = true;
                }

            }
            if (this.IsDestroyed)
            {
                cb.RemoveFromBucketIfExists(this);
            }
        }
    }
}