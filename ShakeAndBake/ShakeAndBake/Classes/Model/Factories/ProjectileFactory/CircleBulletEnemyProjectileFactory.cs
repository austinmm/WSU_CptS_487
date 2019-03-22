using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    public class CircleBulletEnemyProjectileFactory : ProjectileAbstractFactory
    {
        static double angle = 0;
        public override Projectile Create(Vector2 origin)
        {
            // hypotenuse is 1


            angle += .1;
            // sin (angle) = ycomp / hyp
            // sin (angle) = ycomp
            float yComp = (float)Math.Sin(angle);
            float xComp = (float)Math.Cos(angle);
            
            
            PathAbstractFactory factory = new EnemyBulletPathFactory();
            Path path = factory.Create(origin, new Vector2(xComp, yComp), 5);
            Projectile ret = new EnemyBullet(path);
            

            /***
             * TODO: Have this value set according to a config option 
             ***/
            ret.HitDamage = 1;
            angle += 1;
            return ret;
        }
    }
}
