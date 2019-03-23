using ShakeAndBake.Classes.Controller;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class MidBossFactory : EnemyAbstractFactory
    {
        public override Enemy Create(EnemyConfig config)
        {
            MidBoss ret = new MidBoss();
            Configure(ret, config);
            return ret;
        }
    }
}
