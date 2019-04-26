using ShakeAndBake.Extras.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeAndBake.Model.GameEntity
{
    class BombPowerUp: PowerUp
    {
        public BombPowerUp(Path path, string texture) : base(path, texture) { }                    
        protected override void PlaySoundEffect()
        {
            ShakeAndBakeGame.GetSoundEffect("explosion").CreateInstance().Play();
        }

    }
}
