using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakeAndBake.Model.GameEntity
{
    public class Hard : Enemy
    {
        //Hard Enemy Constructor
        public Hard() : base()
        {
            this.path = EnemyPaths.DefaultPath(this.position, new Vector2(0, 1));
            sprite = ShakeAndBakeGame.circle;
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
