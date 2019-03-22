using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public static class ProjectileFactoryProducer
    {
        public static Projectile CreateEnemy(ProjectileType type)
        {
            /***
             * TODO: Implement this
             ***/

           /* ProjectileAbstractFactory factory = ProduceFactory(type);
            if (factory != null)
                return factory.Create();*/
            return null;
        }

        public static ProjectileAbstractFactory ProduceFactory(ProjectileType type)
        {
            /*ProjectileAbstractFactory factory = null;
            switch (type)
            {
                case ProjectileType.EnemyBullet:
                    factory = new BulletProjectileFactory();
                    break;
               
            }
            return factory;*/
            
            /***
             * TODO: Implement this 
             * 
             ***/
            return null;
        }
    }

    public abstract class ProjectileAbstractFactory
    {
        public abstract Projectile Create(Vector2 origin);
    }
}
