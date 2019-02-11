using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake;

namespace GameClasses
{
    public class MidBoss : Enemy
    {
        //Boss Enemy Constructor
        public MidBoss() : base()
        {
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
