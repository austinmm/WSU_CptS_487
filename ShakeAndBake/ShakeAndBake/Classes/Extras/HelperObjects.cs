using System;

namespace ShakeAndBake
{
    public enum EnemyType
    {
        Easy, Medium, Hard, MidBoss, FinalBoss
    }

    public enum ProjectileType
    {
        Bullet, Rocket, FireBall, EnemyBullet, PlayerBullet, BossWaveProjectile, EnemyCircleBullet
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
        WavePath, StraightPath, RandomWavePath
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
    }
}
