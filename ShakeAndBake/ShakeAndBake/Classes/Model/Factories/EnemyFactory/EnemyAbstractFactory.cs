using Microsoft.Xna.Framework;
using ShakeAndBake.Classes.Controller;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public static class EnemeyFactoryProducer
    {        
        public static Enemy CreateEnemy(EnemyConfig config)
        {
            EnemyAbstractFactory factory = ProduceFactory(config);
            if (factory != null)
                return factory.Create(config);
            return null;
        }

        public static EnemyAbstractFactory ProduceFactory(EnemyConfig config)
        {
            EnemyAbstractFactory factory = null;
            EnemyType type = Util.stringToEnemyType(config.stringType);
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
        public abstract Enemy Create(EnemyConfig config);
        
        protected void Configure(Enemy enemy, EnemyConfig config)
        {
            if (config.startPosition.random == true)
            {
                enemy.Position = enemy.GetRandomSpawnPosition();
            }
            else
            {
                enemy.Position = new Vector2((float)config.startPosition.X, (float)config.startPosition.Y);
            }
            enemy.MaxHealth = config.health; // enemy.Health is set to MaxHealth on spawn
            enemy.FireRate = config.fireRate;
            enemy.Velocity = config.speed;
            enemy.Health = config.health;
            enemy.Path = PathFactoryProducer.CreatePath(
                config.pathType,
                enemy.Position,
                new Vector2((float)config.moveDirection.X,
                (float) config.moveDirection.Y),
                (float) enemy.Velocity);
        }
    }
}
