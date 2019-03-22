using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using ShakeAndBake.Classes.Model.Factories.PathFactory;


namespace ShakeAndBake.Model.GameEntity
{
    public class MidBoss : Enemy
    {
        //Boss Enemy Constructor
        public MidBoss() : base()
        {
            this.ProjectileTypes = new System.Collections.Generic.List<ProjectileType>();
            this.ProjectileTypes.Add(ProjectileType.BossWaveProjectile);
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
                fireRate = Util.randInt(500, 1000);
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
