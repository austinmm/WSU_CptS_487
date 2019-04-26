namespace ShakeAndBake
{
    public enum EnemyType
    {
        Easy, Medium, Hard, MidBoss, FinalBoss
    }

    //public enum EnemyProjectileType
    //{
    //    Bullet, Rocket, FireBall, EnemyBullet, PlayerBullet, BossWaveProjectile, EnemyCircleBullet, EnemySmallBullet
    //}
    
    public enum DifficultyLevel
    {
        Easy, Normal, Hard, Lunatic
    }
    
    public enum MoveKeys
    {
        ARROW, WASD
    }

    public enum MenuState
    {
        START, SETTINGS, EXIT
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
}
