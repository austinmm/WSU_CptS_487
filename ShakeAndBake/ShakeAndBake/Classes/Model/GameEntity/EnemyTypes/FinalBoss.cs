using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System;

namespace ShakeAndBake.Model.GameEntity
{
    public class FinalBoss : Enemy
    {
        //Boss Enemy Constructor
        public FinalBoss() : base()
        {
            this.ProjectileTypes = new System.Collections.Generic.List<ProjectileType>();
            this.ProjectileTypes.Add(ProjectileType.EnemyCircleBullet);
            sprite = ShakeAndBakeGame.GetTexture("circle");
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            base.Update(gameTime, cb);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (!IsDestroyed)
            {
                spriteBatch.Draw(sprite, position, Color.White);
            }
        }
    }
}
