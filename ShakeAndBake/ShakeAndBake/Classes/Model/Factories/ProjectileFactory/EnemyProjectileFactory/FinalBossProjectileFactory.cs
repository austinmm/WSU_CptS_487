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
        public FinalBossProjectileFactory()
        {
            //read in velocity, texture and hitdamage
            this.InitReader("FinalBossProjectiles.json");
            this.jsonObject = this.reader.ReadToEnd();
        }

        public override Projectile Create(Vector2 origin)
        {
            return base.Create(origin);
        }
    }
}
