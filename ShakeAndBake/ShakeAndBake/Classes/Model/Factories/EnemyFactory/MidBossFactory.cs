using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class MidBossFactory : EnemyAbstractFactory
    {
        public override Enemy Create()
        {
            return new MidBoss();
        }
    }
}
