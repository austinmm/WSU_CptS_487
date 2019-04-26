using ShakeAndBake.Controller;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class MediumEnemyFactory : EnemyAbstractFactory
    {
        public override Enemy Create(EnemyConfig config)
        {
            Medium ret = new Medium();
            Configure(ret, config);
            return ret;
        }
    }
}
