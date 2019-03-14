using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class FinalBossFactory : EnemyAbstractFactory
    {
        public override Enemy Create()
        {
            return new FinalBoss();
        }
    }
}
