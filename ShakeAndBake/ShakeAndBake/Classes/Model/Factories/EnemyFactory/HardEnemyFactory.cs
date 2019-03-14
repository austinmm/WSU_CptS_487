using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class HardEnemyFactory : EnemyAbstractFactory
    {
        public override Enemy Create()
        {
            return new Hard();
        }
    }
}
