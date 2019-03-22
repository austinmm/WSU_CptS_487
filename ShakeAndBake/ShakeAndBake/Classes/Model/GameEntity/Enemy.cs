using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
namespace ShakeAndBake.Model.GameEntity
{
    public class Enemy : Character
    {
        //This is a set path that the enemy objects will travel when update is called
        protected Path path;
        public Path Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        //Enemy Constructor
        public Enemy() : base()
        {
            HitBoxRadius = 50;
            fireRate = 1250;
        }

        //Updates the 
        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            cb.RemoveFromBucketIfExists(this);

            //Move Enemy to new position in its set path
            position = path.NextPoint();

            // enemy went off screen without dying, so destroy it
            if (position.Y > GameConfig.Height)
            {
                isDestroyed = true;
                return;
            }

            cb.FillBucket(this);

            //Fire a new projectile if firerate field will allow
            this.FireProjectile();

            base.Update(gameTime, cb);
        }
    }
}
