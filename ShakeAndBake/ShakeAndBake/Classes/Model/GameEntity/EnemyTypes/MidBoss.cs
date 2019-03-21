using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakeAndBake.Model.GameEntity
{
    public class MidBoss : Enemy
    {
        //Boss Enemy Constructor
        public MidBoss() : base()
        {
             sprite = ShakeAndBakeGame.AssetManager.GetTexture("circle");
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
