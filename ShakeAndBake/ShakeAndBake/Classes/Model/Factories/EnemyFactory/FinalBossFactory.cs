using ShakeAndBake.Classes.Controller;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class FinalBossFactory : EnemyAbstractFactory
    {
        public override Enemy Create(EnemyConfig config)
        {
            FinalBoss ret = new FinalBoss();
            Configure(ret, config);
            return ret;
        }
    }
}
