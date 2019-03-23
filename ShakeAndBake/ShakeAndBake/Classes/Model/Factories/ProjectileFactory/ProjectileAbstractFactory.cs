using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public static class ProjectileFactoryProducer
    {
        public static Projectile CreateEnemy(ProjectileType type, Vector2 origin)
        {
           ProjectileAbstractFactory factory = ProduceFactory(type);
            if (factory != null)
                return factory.Create(origin);
            return null;
        }

        public static ProjectileAbstractFactory ProduceFactory(ProjectileType type)
        {
            ProjectileAbstractFactory factory = null;
            switch (type)
            {
                case ProjectileType.EnemyBullet:
                    factory = new EnemyBulletProjectileFactory();
                    break;
                case ProjectileType.PlayerBullet:
                    factory = new PlayerBulletProjectileFactory();
                    break;
                case ProjectileType.BossWaveProjectile:
                    factory = new BossWaveProjectileFactory();
                    break;
                case ProjectileType.EnemyCircleBullet:
                    factory = new CircleBulletEnemyProjectileFactory();
                    break;
                case ProjectileType.EnemySmallBullet:
                    factory = new EnemySmallBulletProjectileFactory();
                    break;
               
            }
            return factory;
        }
    }

    public abstract class ProjectileAbstractFactory
    {
        public abstract Projectile Create(Vector2 origin);
    }
}
