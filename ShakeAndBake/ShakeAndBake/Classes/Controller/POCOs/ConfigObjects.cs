using System;
using System.Collections.Generic;

namespace ShakeAndBake.Controller
{
    // used by json deserializer
    public class EnemyConfig
    {
        public string stringType;
        public PathType pathType;
        public PositionConfig startPosition;
        public double speed;
        public int fireRate;
        public int health;
        public PositionConfig moveDirection;
        public int count;
        public string formation;

        public EnemyConfig()
        {
        }

        public EnemyConfig(EnemyConfig copy)
        {
            this.health = copy.health;
            this.pathType = copy.pathType;
            this.startPosition = new PositionConfig(copy.startPosition);
            this.speed = copy.speed;
            this.fireRate = copy.fireRate;
            this.health = copy.health;
            this.moveDirection = new PositionConfig(copy.moveDirection);
            this.count = copy.count;


            if (copy.formation != null)
            {
                this.formation = string.Copy(copy.formation);
            }

            if (copy.stringType != null)
            {
                this.stringType = string.Copy(copy.stringType);
            }
        }

        public class PositionConfig
        {
            public PositionConfig()
            {

            }
            public PositionConfig(PositionConfig copy)
            {
                X = copy.X;
                Y = copy.Y;
                random = copy.random;
            }
            public double X;
            public double Y;
            public Boolean random;
        }
    }
    
    public class WaveConfigs
    {
        public List<List<EnemyConfig>> waves;
    }
}
