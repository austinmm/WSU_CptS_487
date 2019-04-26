using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;

namespace ShakeAndBake.Classes.Model.Factories.PathFactory
{
    class StraightPathFactory : PathAbstractFactory
    {
        public override Path Create(Vector2 origin, Vector2 direction, float speed)
        {
            return new StraightPath(origin, direction, speed);
        }
    }
}
