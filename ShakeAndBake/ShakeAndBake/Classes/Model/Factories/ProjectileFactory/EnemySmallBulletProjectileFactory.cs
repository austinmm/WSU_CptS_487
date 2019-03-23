using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Classes.Model.GameEntity.EnemyProjectiles;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public class EnemySmallBulletProjectileFactory : ProjectileAbstractFactory
    {
        public override Projectile Create(Vector2 origin)
        {
            PathAbstractFactory factory = new EnemyBulletPathFactory();
            Path path = factory.Create(origin, new Vector2(0, 1), 5);
            Projectile ret = new SmallBallProjectile(path);

            /***
             * TODO: Have this value set according to a config option 
             ***/
            ret.HitDamage = 1;
            return ret;
        }
    }
}
