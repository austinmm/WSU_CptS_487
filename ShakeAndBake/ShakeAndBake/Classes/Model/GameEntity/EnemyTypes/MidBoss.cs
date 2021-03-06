﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using ShakeAndBake.Controller.Collision;

namespace ShakeAndBake.Model.GameEntity
{
    public class MidBoss : Enemy
    {
        //Boss Enemy Constructor
        public MidBoss() : base()
        {
            this.ProjectileFactory = new MidBossProjectileFactory();
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
                DrawHealthBar(spriteBatch, 100);
                spriteBatch.Draw(this.Sprite, position, Color.White);
            }
        }
    }
}
