using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System.Collections.Generic;

namespace ShakeAndBake.Model.GameEntity
{
    public class Easy : Enemy
    {
        //Easy Enemy Constructor
        public Easy() : base()
        {
            this.path = EnemyPaths.DefaultPath(this.position, new Vector2(0, 1));
            this.ProjectileTypes = new List<ProjectileType>();
            this.ProjectileTypes.Add(ProjectileType.EnemyBullet);
            sprite = ShakeAndBakeGame.GetTexture("circle");
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            base.Update(gameTime, cb);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(sprite, position, Color.White);
        }

        public override void FireProjectile()
        {
            if (this.CanFire())
            {
                fireRate = Util.randInt(2000, 3000);
                Vector2 pos = Vector2.Add(position, new Vector2((sprite.Width - ShakeAndBakeGame.GetTexture("enemy_bullet").Width) / 2, sprite.Height));
                //Creates a new projectile to be added to the character's ObservableCollection of projectiles

                ProjectileAbstractFactory factory = ProjectileFactoryProducer.ProduceFactory(this.ProjectileTypes[0]);
                Projectile projectile = factory.Create(this.position);
                //The projectiles position is set to the current character's position
                projectile.Position = this.position;
                projectile.Velocity += this.Velocity;
                this.projectiles.Add(projectile);
            }
        }
    }
}
