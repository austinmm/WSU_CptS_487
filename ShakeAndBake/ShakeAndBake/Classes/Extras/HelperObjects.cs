using System;

namespace ShakeAndBake
{
    public enum EnemyType
    {
        Easy, Medium, Hard, MidBoss, FinalBoss
    }

    public enum ProjectileType
    {
        Bullet, Rocket, FireBall, EnemyBullet, PlayerBullet, BossWaveProjectile, EnemyCircleBullet, EnemySmallBullet
    }

    public enum MenuState
    {
        START, EXIT
    }

    public enum EndMenuState
    {
        MAIN, EXIT
    }
    
    public enum PathType
    {
        WavePath, StraightPath, RandomWavePath, SweepRight
    }

    public enum GameState
    {
        PLAYING, GAMEOVER, MENU, EXIT, RESET, PAUSE
    }

    public static class Util
    {
        public static readonly Random RANDOM = new Random();
        
        public static int randInt(int min, int max)
        {
            return RANDOM.Next(min, max);
        }
        
        public static double randDouble(double min, double max)
        {
            return RANDOM.NextDouble() * (max - min) + min;
        }

        public static EnemyType stringToEnemyType(string type)
        {
            EnemyType ret;
            switch (type)
            {
                case "Easy": ret = EnemyType.Easy;
                        break;
                case "Medium":
                    ret = EnemyType.Medium;
                    break;
                case "Hard":
                    ret = EnemyType.Hard;
                    break;
                case "MidBoss":
                    ret = EnemyType.MidBoss;
                    break;
                case "FinalBoss":
                    ret = EnemyType.FinalBoss;
                    break;
                default:
                    ret = EnemyType.Easy;
                    break;
            }
            return ret;
        }
    }
}
