using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake;

namespace GameClasses
{
    public class Medium : Enemy
    {
        //Medium Enemy Constructor
        public Medium() : base()
        {
            this.path = EnemyPaths.DefaultPath(this.position, new Vector2(0, 1));
            sprite = ShakeAndBakeGame.circle;
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); 
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
