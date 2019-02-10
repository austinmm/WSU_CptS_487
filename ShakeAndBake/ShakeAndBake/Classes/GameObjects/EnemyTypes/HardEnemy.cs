﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameClasses
{
    public class Hard : Enemy
    {
        //Hard Enemy Constructor
        public Hard() : base()
        {
            this.path = EnemyPaths.DefaultPath(this.position, new Vector2(0, 1));
        }

        public override void Update(GameTime gameTime)
        {
            // update enemy here

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // draw enemy here
            
            base.Draw(spriteBatch);
        }
    }
}
