using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class MediumEnemyFactory : EnemyAbstractFactory
    {
        public override Enemy Create()
        {
            return new Medium();
        }
    }
}
