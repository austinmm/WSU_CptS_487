using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System;
using System.Diagnostics;

namespace ShakeAndBake.Model.GameEntity
{
    public class Enemy : Character
    {
        //This is a set path that the enemy objects will travel when update is called
        protected Path path;
        protected static int enemySpawnSeed = 5;

        public Path Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        //Enemy Constructor
        public Enemy() : base() { }

        public Vector2 GetRandomSpawnPosition()
        {
            enemySpawnSeed = Math.Abs(enemySpawnSeed * enemySpawnSeed);
            Random rand = new Random(Math.Abs(enemySpawnSeed));
            Vector2 ret = new Vector2(rand.Next(0, GameConfig.Width - Sprite.Width), -this.Sprite.Height);
            return ret;
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            cb.RemoveFromBucketIfExists(this);

            if (!this.IsDestroyed)
            {
                if (this.BoundsContains(Player.Instance) || Player.Instance.BoundsContains(this))
                {
                    Player.Instance.TakeDamage(1);
                    this.TakeDamage(1);
                }
                //Move Enemy to new position in its set path
                position = path.NextPoint();
                // enemy went off screen without dying, so destroy it
                if (position.Y > GameConfig.Height)
                {
                    path.Reset();
                }
                else
                {
                    cb.FillBucket(this);
                    //Fire a new projectile if firerate field will allow
                    this.FireProjectile();
                }
            }
            base.Update(gameTime, cb);
        }

        public override void FireProjectile()
        {
            if (this.CanFire())
            {
                //Creates a new projectile to be added to the character's ObservableCollection of projectiles
                Vector2 origin = this.GetCenterCoordinates();
                Projectile projectile = this.ProjectileFactory.Create(origin);
                Debug.Print("The hash code stored: {0}", projectile.GetHashCode());
                this.projectiles.Add(projectile);
                ShakeAndBakeGame.GetSoundEffect("enemy_shot").CreateInstance().Play();
            }
        }
    }
}
