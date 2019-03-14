using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public static class EnemeyFactoryProducer
    {        
        public static Enemy CreateEnemy(EnemyType type)
        {
            EnemyAbstractFactory factory = ProduceFactory(type);
            if (factory != null)
                return factory.Create();
            return null;
        }

        public static EnemyAbstractFactory ProduceFactory(EnemyType type)
        {
            EnemyAbstractFactory factory = null;
            switch (type) {
                case EnemyType.Easy:
                    factory = new EasyEnemyFactory();
                    break;
                case EnemyType.Medium:
                    factory = new MediumEnemyFactory();
                    break;
                case EnemyType.Hard:
                    factory = new HardEnemyFactory();
                    break;
                case EnemyType.MidBoss:
                    factory = new MidBossFactory();
                    break;
                case EnemyType.FinalBoss:
                    factory = new FinalBossFactory();
                    break;
            }
            return factory;
        }
    }

    public abstract class EnemyAbstractFactory
    {
        public abstract Enemy Create();
    }
}
