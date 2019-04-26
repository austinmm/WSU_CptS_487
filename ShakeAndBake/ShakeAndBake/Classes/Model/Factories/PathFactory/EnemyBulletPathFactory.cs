using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;

namespace ShakeAndBake.Classes.Model.Factories.PathFactory
{
    class EnemyBulletPathFactory : PathAbstractFactory
    {
        public override Path Create(Vector2 origin, Vector2 direction, float speed)
        {
            Path path = new StraightPath(origin, direction, 1);
            return path;
        }
    }
}
