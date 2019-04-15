using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
using System;
using Newtonsoft.Json;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public class DefaultPlayerProjectileFactory : PlayerProjectileAbstractFactory
    {
        public DefaultPlayerProjectileFactory()
        {
            //read in velocity, texture and hitdamage
            this.InitReader("DefaultPlayerProjectiles.json");
            this.jsonObject = this.reader.ReadToEnd();
        }

        public override Projectile Create(Vector2 origin)
        {
            int num = Util.randInt(0, this.projectiles.Count);
            Projectile projectile = this.projectiles[num];
            this.factory = PathFactoryProducer.ProduceFactory(projectile.PathType);
            Path path = this.factory.Create(origin, new Vector2(0, -1), (float)projectile.Velocity);
            projectile.Path = path;
            return projectile;
        }
    }
}
