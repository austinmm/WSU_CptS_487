using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public class HardProjectileFactory : EnemyProjectileAbstractFactory
    {
        public HardProjectileFactory()
        {
            this.InitReader("HardProjectiles.json");
            this.jsonObject = this.reader.ReadToEnd();
        }

        public override Projectile Create(Vector2 origin)
        {
            return base.Create(origin);
        }
    }
}
