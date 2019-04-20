using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
using System;
using Newtonsoft.Json;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public class FinalBossProjectileFactory : EnemyProjectileAbstractFactory
    {
        static double angle = 0;
        double angularVelocity = .01;
      
        public FinalBossProjectileFactory()
        {
            //read in velocity, texture and hitdamage
            this.InitReader("FinalBossProjectiles.json");
            this.jsonObject = this.reader.ReadToEnd();
        }

        public override Projectile Create(Vector2 origin)
        {

            angle += angularVelocity;
            float yComp = (float)Math.Sin(angle);
            float xComp = (float)Math.Cos(angle);

            origin.X += xComp * 100;
            origin.Y += yComp * 100;

            int random = Util.randInt(0, 100);
            int num = Util.randInt(0, this.projectiles.Count);

            Projectile projectile = this.projectiles[0].Clone();
            this.factory = PathFactoryProducer.ProduceFactory(projectile.PathType);
            Path path = this.factory.Create(origin, new Vector2(xComp, yComp), (float)projectile.Velocity);
            projectile.Path = path;
            angle += 1;
            projectile.IsBouncy = true;
            return projectile;
        }
    }
}
