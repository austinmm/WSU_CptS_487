using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
using System;

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
                spriteBatch.Draw(this.Sprite, position, Color.White);
            }
        }
    }
}
