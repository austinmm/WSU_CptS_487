﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;
namespace ShakeAndBake.Classes.Model.Factories.PathFactory
{
    class EnemyBulletPathFactory : PathAbstractFactory
    {
        public override Path Create(Vector2 origin, Vector2 direction, float speed)
        {
            Path path = new StraightPath(origin, direction, 1);
            return path;
        }
    }
}
