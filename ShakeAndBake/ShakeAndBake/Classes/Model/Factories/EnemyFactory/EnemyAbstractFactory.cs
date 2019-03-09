using System;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Classes.Model
{
    abstract class EnemyAbstractFactory
    {
        public abstract Enemy create();
    }
}
