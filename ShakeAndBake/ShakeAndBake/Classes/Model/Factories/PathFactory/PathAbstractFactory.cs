using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;

namespace ShakeAndBake.Classes.Model.Factories.PathFactory
{
    public static class PathFactoryProducer
    {
        public static Path CreatePath(PathType type, Vector2 origin, Vector2 direction, float speed)
        {
            PathAbstractFactory factory = ProduceFactory(type);
            if (factory != null)
                return factory.Create(origin, direction, speed);
            return null;
        }

        public static PathAbstractFactory ProduceFactory(PathType type)
        {
            PathAbstractFactory factory = null;
            switch (type)
            {
                case PathType.StraightPath:
                    factory = new StraightPathFactory();
                    break;
                case PathType.WavePath:
                    factory = new WavePathFactory();
                    break;
                case PathType.RandomWavePath:
                    factory = new RandomWavePathFactory();
                    break;

            }
            return factory;
        }
    }
    public abstract class PathAbstractFactory
    {
        public abstract Path Create(Vector2 origin, Vector2 direction, float speed);
    }
}
