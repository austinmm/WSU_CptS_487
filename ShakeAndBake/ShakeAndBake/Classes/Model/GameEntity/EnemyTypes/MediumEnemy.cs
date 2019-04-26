using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using ShakeAndBake.Controller.Collision;
using System;
using System.Collections.Generic;

namespace ShakeAndBake.Model.GameEntity
{
    public class Medium : Enemy
    {
        //Medium Enemy Constructor
        public Medium() : base()
        {
            this.ProjectileFactory = new MediumProjectileFactory();
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
                DrawHealthBar(spriteBatch, 75);
                spriteBatch.Draw(this.Sprite, position, Color.White);
            }
        }
    }
}
