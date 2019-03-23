using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.GameEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeAndBake.Classes.Model.GameEntity.EnemyProjectiles
{
    class SmallBallProjectile : EnemyBullet
    {
        public SmallBallProjectile(Path path) : base(path)
        {
            sprite = ShakeAndBakeGame.GetTexture("small_black_ball");
            this.hitBoxRadius = sprite.Width / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
