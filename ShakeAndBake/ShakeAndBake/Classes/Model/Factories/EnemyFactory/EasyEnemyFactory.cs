﻿using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class EasyEnemyFactory : EnemyAbstractFactory
    {
        public override Enemy Create()
        {
            return new Easy();
        }
    }
}
