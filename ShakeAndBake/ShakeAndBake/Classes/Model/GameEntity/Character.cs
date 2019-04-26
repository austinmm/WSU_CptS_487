using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace ShakeAndBake.Model.GameEntity
{
    public abstract class Character : GameObject
    {
        //This contains the amount of time the character has been alive for
        protected Stopwatch timeAlive;
        public Stopwatch TimeAlive
        {
            get { return this.timeAlive; }
            set { this.timeAlive = value; }
        }

        //This contains the time the character last fired a projectile (Milliseconds)
        protected double? lastFiredTime;
        public Nullable<double> LastFiredTime
        {
            get { return this.lastFiredTime; }
            set { this.lastFiredTime = value; }
        }

        //This contains the speed at which the character can fire their projectiles (Milliseconds)
        protected double fireRate;
        public double FireRate
        {
            get { return this.fireRate; }
            set { this.fireRate = value; }
        }

        //This contains the amount of health a character has left
        protected double health;
        public double Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        //This contains the initial health that the character spawned with
        protected double maxHealth;
        public double MaxHealth
        {
            get { return this.maxHealth; }
            set { this.maxHealth = value; }
        }

        //This will be used and set by sub classes for further specification of which factory to invoke 
        protected ProjectileAbstractFactory ProjectileFactory;

        //This contains a ObservableCollection of all the projectiles the character currently has
        protected List<Projectile> projectiles;
        public List<Projectile> Projectiles
        {
            get { return this.projectiles; }
            set { this.projectiles = value; }
        }
        
        public Character() : base()
        {
            //When projectiles are added or removed from the ObservableCollection then "OnProjectileChange" is automatically called
            this.health = 5;
            this.timeAlive = new Stopwatch();
            this.timeAlive.Start();
            this.projectiles = new List<Projectile>();
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            this.UpdateProjectiles(gameTime, cb);
            // update character in derived enemy/player classes
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile projectile in projectiles)
            {
                if (!projectile.IsDestroyed)
                {
                    projectile.Draw(spriteBatch);
                }
            }
        }

        /* Returns the remaining amount after it is delt to the character */
        public double TakeDamage(int amount)
        {
            double overflow = 0;
            this.health -= amount;
            if (this.health <= 0)
            {
                overflow = Math.Abs(this.health);
                this.isDestroyed = true;
                this.health = 0;
            }
            return overflow;
        }

        public void UpdateProjectiles(GameTime gameTime, CollisionBoard cb)
        {
            //Update existing bullets already fired by the character
            List<Projectile> deadProjectiles = new List<Projectile>();
            foreach (Projectile projectile in this.projectiles)
            {
                if (projectile.IsDestroyed)
                {
                    deadProjectiles.Add(projectile);
                    continue;
                }
                projectile.Update(gameTime, cb);
            }
                

            foreach (Projectile projectile in deadProjectiles)
            {
                this.projectiles.Remove(projectile);
            }
        }

        public abstract void FireProjectile();

        protected bool CanFire()
        {
            //character has fired atleast one projectile
            if (this.lastFiredTime.HasValue)
            {
                //if the character has to wait longer until they can fire another projectile
                if (this.timeAlive.ElapsedMilliseconds < this.lastFiredTime + this.fireRate)
                {
                    return false;
                }
            }
            //sets last projectile fired time to current time
            this.lastFiredTime = this.timeAlive.ElapsedMilliseconds;
            return true;
        }
        
        public void DrawHealthBar(SpriteBatch spriteBatch, int barWidth)
        {
            if (maxHealth == 0) return;
            int barHeight = (int) ((double)barWidth * 0.15); // scale based on width
            double perc = (health / maxHealth);
            int divider = (int) (barWidth * perc); // end position of green section

            Texture2D rect = new Texture2D(ShakeAndBakeGame.Instance.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });

            Vector2 center = GetCenterCoordinates();
            int x = (int) center.X - (barWidth / 2);
            int y = (int) position.Y - (barHeight + (barHeight / 2));

            // remaining damage (green section)
            spriteBatch.Draw(rect, new Rectangle(x, y, divider, barHeight), Color.Lime);
            // dealt damage (red section)
            spriteBatch.Draw(rect, new Rectangle(x + divider, y, barWidth - divider, barHeight), Color.Red);
        }
    }
}
