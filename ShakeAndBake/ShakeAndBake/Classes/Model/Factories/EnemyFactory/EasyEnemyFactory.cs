using ShakeAndBake.Classes.Controller;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Model.Factories.EnemyFactory
{
    public class EasyEnemyFactory : EnemyAbstractFactory
    {
        public override Enemy Create(EnemyConfig config)
        {
            Easy ret = new Easy();
            Configure(ret, config);
            return ret;
        }
    }
}
