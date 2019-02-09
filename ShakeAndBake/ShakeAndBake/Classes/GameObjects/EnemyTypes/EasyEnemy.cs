using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

using ShakeAndBake;

namespace GameClasses
{
    public class Easy : Enemy
    {
        //Easy Enemy Constructor
        public Easy() : base()
        {
            this.path = EnemyPaths.DefaultPath(this.position, new Vector2(0, 1));
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); 
            spriteBatch.Draw(ShakeAndBakeGame.circle, new Vector2(50, 50), Color.White);
        }
    }
}
