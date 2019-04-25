using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.GameEntity;
using System.Collections.Generic;

namespace ShakeAndBake.Model.Factories.ProjectileFactory
{
    #region Old ProjectileFactoryProducer code that isn't needed anymore
    //public interface ProjectileFactoryProducer
    //{
    //    Projectile CreateProjectile(string type, Vector2 origin);
    //    ProjectileAbstractFactory ProduceFactory(string type);
    //}

    //public class EnemyProjectileFactoryProducer : ProjectileFactoryProducer
    //{
    //    public Projectile CreateProjectile(string type, Vector2 origin)
    //    {
    //       ProjectileAbstractFactory factory = ProduceFactory(type);
    //        if (factory != null)
    //            return factory.Create(origin);
    //        return null;
    //    }

    //    public ProjectileAbstractFactory ProduceFactory(string type)
    //    {
    //        EnemyType et = Util.stringToEnemyType(type);
    //        ProjectileAbstractFactory factory = null;
    //        switch (et)
    //        {
    //            case EnemyType.Easy:
    //                factory = new EasyProjectileFactory();
    //                break;
    //            case EnemyType.Medium:
    //                factory = new MediumProjectileFactory();
    //                break;
    //            case EnemyType.MidBoss:
    //                factory = new MidBossProjectileFactory();
    //                break;
    //            case EnemyType.Hard:
    //                factory = new HardProjectileFactory();
    //                break;
    //            case EnemyType.FinalBoss:
    //                factory = new FinalBossProjectileFactory();
    //                break;
    //        }
    //        return factory;
    //    }
    //}

    //public class PlayerProjectileFactoryProducer : ProjectileFactoryProducer
    //{
    //    public Projectile CreateProjectile(string type, Vector2 origin)
    //    {
    //        ProjectileAbstractFactory factory = ProduceFactory(type);
    //        if (factory != null)
    //            return factory.Create(origin);
    //        return null;
    //    }

    //    public ProjectileAbstractFactory ProduceFactory(string type)
    //    {
    //        ProjectileAbstractFactory factory = null;
    //        switch (type)
    //        {
    //            case "default":
    //                factory = new DefaultPlayerProjectileFactory();
    //                break;
    //            case "special":
    //                factory = new SpecialPlayerProjectileFactory();
    //                break;
    //        }
    //        return factory;
    //    }
    //}


    //public class ProjectileFactory : ProjectileAbstractFactory
    //{
    //    public override Projectile Create(Vector2 origin)
    //    {
    //        PathAbstractFactory factory = new EnemyBulletPathFactory();
    //        Path path = factory.Create(origin, new Vector2(0, 1), 5);
    //        string texture = "enemy_bullet";
    //        Projectile ret = new EnemyBullet(path, texture);
    //        ret.HitDamage = 1;
    //        return ret;
    //    }
    //}

    //public class PlayerTearDropBullet : ProjectileAbstractFactory
    //{
    //    public override Projectile Create(Vector2 origin)
    //    {
    //        PathAbstractFactory factory = new StraightPathFactory();
    //        Path path = factory.Create(origin, new Vector2(0, -1), 5);
    //        string texture = "player_teardrop_bullet";
    //        Projectile ret = new PlayerBullet(path, texture);
    //        ret.HitDamage = 1;
    //        return ret;
    //    }
    //}
    #endregion

    public abstract class ProjectileAbstractFactory
    {
        protected string jsonObject;
        protected System.IO.StreamReader reader;
        protected PathAbstractFactory factory;

        public abstract Projectile Create(Vector2 origin);
        protected virtual void InitReader(string basename)
        {
            string dirname = "../../../../JSON/ProjectileTypes/";
            this.reader = new System.IO.StreamReader(dirname+basename);
            this.jsonObject = this.reader.ReadToEnd();
        }
    }

    public abstract class EnemyProjectileAbstractFactory : ProjectileAbstractFactory
    {
        protected List<EnemyBullet> projectiles = new List<EnemyBullet>();
        public override Projectile Create(Vector2 origin)
        {
            int num = Util.randInt(0, this.projectiles.Count);
            Projectile projectile = this.projectiles[num].Clone();
            this.factory = PathFactoryProducer.ProduceFactory(projectile.PathType);
            Path path = this.factory.Create(origin, new Vector2(0, 1), (float)projectile.Velocity);
            projectile.Path = path;
            projectile.Position = origin;

            return projectile;
        }

        protected override void InitReader(string basename)
        {
           base.InitReader(basename);
           this.projectiles = JsonConvert.DeserializeObject<List<EnemyBullet>>(this.jsonObject);
        }
    }

    public abstract class PlayerProjectileAbstractFactory : ProjectileAbstractFactory
    {
        protected List<PlayerBullet> projectiles = new List<PlayerBullet>();

        protected override void InitReader(string basename)
        {
            base.InitReader(basename);
            this.projectiles = JsonConvert.DeserializeObject<List<PlayerBullet>>(this.jsonObject);
        }
    }

}
