using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;
namespace ShakeAndBake.Classes.Model.Factories.PathFactory
{
    class PlayerBulletPathFactory : PathAbstractFactory
    {
        public override Path Create(Vector2 origin)
        {
            Path path = new StraightPath(origin, new Vector2(0, -1), 5);
            return path;
        }
    }
}
