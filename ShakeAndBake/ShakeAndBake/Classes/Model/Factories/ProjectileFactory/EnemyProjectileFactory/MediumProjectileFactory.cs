using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
using System;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public class MediumProjectileFactory : EnemyProjectileAbstractFactory
    {
        static double angle = 0;

        public MediumProjectileFactory()
        {
            //read in velocity, texture and hitdamage
            this.InitReader("MediumProjectiles.json");
            this.jsonObject = this.reader.ReadToEnd();
        }

        public override Projectile Create(Vector2 origin)
        {
            Projectile projectile = base.Create(origin);
            angle += .1;
            float yComp = (float)Math.Sin(angle);
            float xComp = (float)Math.Cos(angle);
            int num = Util.randInt(0, this.projectiles.Count);
            this.factory = PathFactoryProducer.ProduceFactory(projectile.PathType);
            Path path = this.factory.Create(origin, new Vector2(xComp, yComp), (float)projectile.Velocity);
            projectile.Path = path;
            angle += 1;
            return projectile;
        }
    }
}
