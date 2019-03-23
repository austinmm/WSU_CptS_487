using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System;

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
        public Enemy() : base()
        {
            this.HitBoxRadius = 50;
            this.fireRate = 1250;
        }

        public Vector2 GetRandomSpawnPosition()
        {
            enemySpawnSeed = Math.Abs(enemySpawnSeed * enemySpawnSeed);
            Random rand = new Random(Math.Abs(enemySpawnSeed));
            Vector2 ret = new Vector2(rand.Next(0, GameConfig.Width - Sprite.Width), -this.Sprite.Height);
            return ret;
        }
        //Updates the 
        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            cb.RemoveFromBucketIfExists(this);

            if (!this.IsDestroyed)
            {

                if (this.BoundsContains(Player.Instance) || Player.Instance.BoundsContains(this))
                {
                    /* Apply melee damage */
                    /***
                     * We need a function to DEAL MELEE DAMAGE because not
                     * all enemies should immediately die upon contact with
                     * main player
                     ***/
                    Player.Instance.TakeDamage(1);
                    this.IsDestroyed = true;
                    return;
                }

                //Move Enemy to new position in its set path
                position = path.NextPoint();

                // enemy went off screen without dying, so destroy it
                if (position.Y > GameConfig.Height)
                {
                    //isDestroyed = true;
                    // move back to start since the enemy didn't die
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
                Vector2 pos = Vector2.Add(position, new Vector2((sprite.Width - ShakeAndBakeGame.GetTexture("enemy_bullet").Width) / 2, sprite.Height));
                //Creates a new projectile to be added to the character's ObservableCollection of projectiles

                ProjectileAbstractFactory factory = ProjectileFactoryProducer.ProduceFactory(this.ProjectileTypes[0]);
                Projectile projectile = factory.Create(new Vector2((float)(this.position.X + this.hitBoxRadius/2), (float)(this.position.Y + this.hitBoxRadius/2)));
                //The projectiles position is set to the current character's position
                projectile.Position = this.position;
                projectile.Velocity += this.Velocity;
                this.projectiles.Add(projectile);
            }
        }
    }
}
