using ShakeAndBake.Classes.Controller;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class HardEnemyFactory : EnemyAbstractFactory
    {
        public override Enemy Create(EnemyConfig config)
        {
            Hard ret = new Hard();
            Configure(ret, config);
            return ret;
        }
    }
}
