using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System;
using System.Collections.Generic;

namespace ShakeAndBake.Model.GameEntity
{
    public class Easy : Enemy
    {
        //Easy Enemy Constructor
        public Easy() : base()
        {
            this.ProjectileFactory = new EasyProjectileFactory();
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
