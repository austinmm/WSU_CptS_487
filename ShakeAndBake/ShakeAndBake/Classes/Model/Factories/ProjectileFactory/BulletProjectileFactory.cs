using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public class ProjectileFactory : ProjectileAbstractFactory
    {
        public override Projectile Create(Vector2 origin)
        {
            PathAbstractFactory factory = new EnemyBulletPathFactory();
            Path path = factory.Create(origin, new Vector2(0, 1), 5);
            string texture = "enemy_bullet";
            Projectile ret = new EnemyBullet(path, texture);
            ret.HitDamage = 1;
            return ret;
        }
    }

    public class PlayerTearDropBullet : ProjectileAbstractFactory
    {
        public override Projectile Create(Vector2 origin)
        {
            PathAbstractFactory factory = new StraightPathFactory();
            Path path = factory.Create(origin, new Vector2(0, -1), 5);
            string texture = "player_teardrop_bullet";
            Projectile ret = new PlayerBullet(path, texture);
            ret.HitDamage = 1;
            return ret;
        }
    }
}
