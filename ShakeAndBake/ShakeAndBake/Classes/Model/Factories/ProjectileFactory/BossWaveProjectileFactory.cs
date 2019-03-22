using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public class BossWaveProjectileFactory : ProjectileAbstractFactory
    {
        public override Projectile Create(Vector2 origin)
        {
            PathAbstractFactory factory = new RandomWavePathFactory();
            Path path = factory.Create(origin, new Vector2(0, 1), 5);
            Projectile ret = new EnemyBullet(path);

            /***
             * TODO: Have this value set according to a config option 
             ***/
            ret.HitDamage = 10;
            return ret;
        }
    }
}
