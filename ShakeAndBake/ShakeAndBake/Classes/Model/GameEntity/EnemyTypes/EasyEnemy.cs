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
            fireRate = Util.randDouble(2000, 3000);
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
    }
}
