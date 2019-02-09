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
    public class Medium : Enemy
    {
        //Medium Enemy Constructor
        public Medium() : base()
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

            base.Draw(spriteBatch);
        }
    }
}
