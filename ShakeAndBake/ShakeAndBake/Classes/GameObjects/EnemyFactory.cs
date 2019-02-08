namespace GameClasses
{
    public static class EnemeyFactory
    {
        public static Enemy CreateEnemy(EnemyType type)
        {
            switch (type) {
                case EnemyType.Easy:
                    return new Easy();
                case EnemyType.Medium:
                    return new Medium();
                case EnemyType.Hard:
                    return new Hard();
                case EnemyType.MidBoss:
                    return new MidBoss();
                case EnemyType.FinalBoss:
                    return new FinalBoss();
            }
            return null;
        }
    }
}