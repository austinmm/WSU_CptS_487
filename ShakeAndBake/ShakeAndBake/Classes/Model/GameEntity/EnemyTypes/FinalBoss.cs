using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;

namespace ShakeAndBake.Model.GameEntity
{
    public class FinalBoss : Enemy
    {
        //Boss Enemy Constructor
        public FinalBoss() : base()
        {
            fireRate = Util.randDouble(100, 200);
            this.ProjectileTypes = new System.Collections.Generic.List<ProjectileType>();
            this.ProjectileTypes.Add(ProjectileType.EnemyBullet);
            this.path = EnemyPaths.DefaultPath(this.position, new Vector2(0, 1));
             sprite = ShakeAndBakeGame.GetTexture("circle");
            this.health = 10;
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
    }
}
