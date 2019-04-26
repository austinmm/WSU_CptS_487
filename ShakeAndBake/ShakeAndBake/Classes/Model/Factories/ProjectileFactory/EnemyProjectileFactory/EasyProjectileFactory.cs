using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public class EasyProjectileFactory : EnemyProjectileAbstractFactory
    {
        public EasyProjectileFactory()
        {
            //read in velocity, texture and hitdamage
            this.InitReader("EasyProjectiles.json");
            this.jsonObject = this.reader.ReadToEnd();
        }

        public override Projectile Create(Vector2 origin)
        {
            return base.Create(origin);
        }
    }
}
