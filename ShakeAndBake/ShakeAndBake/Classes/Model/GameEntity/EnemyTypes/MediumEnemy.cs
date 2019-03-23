using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System;
using System.Collections.Generic;

namespace ShakeAndBake.Model.GameEntity
{
    public class Medium : Enemy
    {
        //Medium Enemy Constructor
        public Medium() : base()
        {
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
            if (!IsDestroyed)
            {
                spriteBatch.Draw(sprite, position, Color.White);
            }
        }
    }
}
