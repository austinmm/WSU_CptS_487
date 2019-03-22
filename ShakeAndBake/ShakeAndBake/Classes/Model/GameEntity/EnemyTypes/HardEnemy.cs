using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System.Collections.Generic;

namespace ShakeAndBake.Model.GameEntity
{
    public class Hard : Enemy
    {
        //Hard Enemy Constructor
        public Hard() : base()
        {
            fireRate = Util.randDouble(400, 600);
            this.ProjectileTypes = new List<ProjectileType>();
            this.ProjectileTypes.Add(ProjectileType.EnemyBullet);
            this.path = EnemyPaths.DefaultPath(this.position, new Vector2(0, 1));
             sprite = ShakeAndBakeGame.GetTexture("circle");
            this.health = 3;
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
