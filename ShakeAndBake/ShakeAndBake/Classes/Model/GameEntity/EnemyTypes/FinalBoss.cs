using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using ShakeAndBake.Controller.Collision;

namespace ShakeAndBake.Model.GameEntity
{
    public class FinalBoss : Enemy
    {
        //Boss Enemy Constructor
        public FinalBoss() : base()
        {
            this.ProjectileFactory = new FinalBossProjectileFactory();
            this.Sprite = ShakeAndBakeGame.GetTexture("final");
        }
        
        private bool startedSound = false;

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            base.Update(gameTime, cb);

            if (health <= (maxHealth / 2)) {     
                if (!startedSound) {
                    startedSound = true;
                    ShakeAndBakeGame.PlaySoundEffect("final_dead"); 
                }        
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (!IsDestroyed)
            {
                DrawHealthBar(spriteBatch, 150);
                spriteBatch.Draw(this.Sprite, position, Color.White);
            }
        }
    }
}
