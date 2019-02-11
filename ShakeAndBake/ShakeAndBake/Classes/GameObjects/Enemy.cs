using Microsoft.Xna.Framework;
using ShakeAndBake;

namespace GameClasses
{
    public enum EnemyType
    {
        Easy, Medium, Hard, MidBoss, FinalBoss
    }
    
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
        public Enemy() : base() {
            HitBoxRadius = 50;
            fireRate = 1250;
        }

        //Updates the 
        public override void Update(GameTime gameTime)
        {
            //Move Enemy to new position in its set path
            position = path.NextPoint();

            // enemy went off screen without dying, so destroy it
            if (position.Y > ShakeAndBakeGame.graphics.GraphicsDevice.Viewport.Height)
            {
                isDestroyed = true;
                return;
            }

            //Fire a new projectile if firerate field will allow
            this.FireProjectile();
            
            base.Update(gameTime);  
        }
        
        public override void FireProjectile()
        {
            if (sprite == null) return;
            if (this.CanFire())
            {
                fireRate = Util.randInt(1000, 2000);
                Vector2 pos = Vector2.Add(position, new Vector2((sprite.Width - ShakeAndBakeGame.enemyBullet.Width) / 2, sprite.Height));
                //Creates a new projectile to be added to the character's ObservableCollection of projectiles
                float vel = (float) (this.velocity * Util.randDouble(1.5, 3)); // proj velocity = enemy velocity * rand(1.5,3)
                Projectile projectile = new EnemyBullet(new StraightPath(pos, new Vector2(0, 1), vel));
                //The projectiles position is set to the current character's position
                projectile.Position = this.position;
                this.projectiles.Add(projectile);
            }
        }
        
        //Checks if a specific projectile, that just updated its coordinates, has hit our player
        protected override void CheckForHits(Projectile projectile)
        {
            GameBoard.IsHit(GameBoard.User, projectile);
        }
    }
}
