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
            this.ProjectileFactory = new FinalBossProjectileFactory();
            this.Sprite = ShakeAndBakeGame.GetTexture("enemy_default");

            maxHealth = 100;
            health = maxHealth; // temp; add to config
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
                DrawHealthBar(spriteBatch, 150, 20);
                spriteBatch.Draw(this.Sprite, position, Color.White);
            }
        }
    }
}
