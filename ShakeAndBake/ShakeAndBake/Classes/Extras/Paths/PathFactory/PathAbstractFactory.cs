﻿using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;

namespace ShakeAndBake.Extras.Paths
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
                case PathType.SweepRight:
                    factory = new SweepRightPathFactory();
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
