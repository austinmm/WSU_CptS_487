using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using System.Collections.Generic;

namespace ShakeAndBake.Model.GameEntity
{
    public class Projectile : GameObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //This is a set path that the projectile objects will travel when update is called
        protected Path path;
        public Path Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        //This is the type of the projectile which will determine its image, path, hitDamage
        private ProjectileTypes type;
        public ProjectileTypes Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        //This is the amount of damage an enemy or player will take if hit by this projectile
        private double hitDamage;
        public double HitDamage
        {
            get { return this.hitDamage; }
            set { this.hitDamage = value; }
        }

        //Projectile Constructor
        public Projectile(Path path)
        {
            this.path = path;
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            if (position.Y + this.sprite.Height < 0 ||
                position.Y > GameConfig.Height)
            {
                IsDestroyed = true;
                return;
            }

            //Before updating position, remove current bucket position on collision board
            cb.RemoveFromBucketIfExists(this);

            //Update the position of the projectile based off of its spritePath
            position = path.NextPoint();

            //Tell the event handler that the projectile has moved and thus it needs to check if it has hit an enemy or player
            RaisePropertyChanged("Projectile_Position_Changed");

            //Fill bucket on collision board
            cb.FillBucket(this);

            if (this.GetType().Equals(typeof(EnemyBullet)))
            {
            }
            else if (this.GetType().Equals(typeof(PlayerBullet)))
            {
                HashSet<GameObject> collidedEnemies = cb.GetObjectsCollided(this, typeof(Enemy));
                HashSet<GameObject> collidedBullets = cb.GetObjectsCollided(this, typeof(EnemyBullet));

                foreach (Enemy go in collidedEnemies)
                {
                    if (go.IsDestroyed)
                        continue;

                    /* TODO: Replace this with a function which does damage instead of instant kill */
                    go.IsDestroyed = true;
                    this.IsDestroyed = true;
                }

                if (!this.IsDestroyed)
                {
                    foreach (EnemyBullet go in collidedBullets)
                    {
                        if (go.IsDestroyed)
                            continue;

                        /* TODO: Replace this with a function which does damage instead of instant kill */
                        go.IsDestroyed = true;
                        this.IsDestroyed = true;
                    }
                }
            }

            //cb.GetObjectsCollided(this, GameObject)
        }

        //https://github.com/jbe2277/waf/wiki/Implementing-and-usage-of-INotifyPropertyChanged
        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        //Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(PropertyChangedEventArgs property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, property);
            }
        }
    }

    public class PlayerBullet : Projectile
    {
        public PlayerBullet(Path path) : base(path)
        {
            sprite = ShakeAndBakeGame.playerBullet;
            this.hitBoxRadius = sprite.Width / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }

    public class EnemyBullet : Projectile
    {
        public EnemyBullet(Path path) : base(path)
        {
            sprite = ShakeAndBakeGame.enemyBullet;
            this.hitBoxRadius = sprite.Width / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}