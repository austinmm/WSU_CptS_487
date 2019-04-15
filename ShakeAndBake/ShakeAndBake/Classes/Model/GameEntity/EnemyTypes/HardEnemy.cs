using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System;
using System.Collections.Generic;

namespace ShakeAndBake.Model.GameEntity
{
    public class Hard : Enemy
    {
        //Hard Enemy Constructor
        public Hard() : base()
        {
            this.ProjectileFactory = new HardProjectileFactory();
            this.Sprite = ShakeAndBakeGame.GetTexture("enemy_default");
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
                spriteBatch.Draw(this.Sprite, position, Color.White);
            }
        }
    }
}
