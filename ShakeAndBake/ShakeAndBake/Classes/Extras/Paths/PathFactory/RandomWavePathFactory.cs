using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;

namespace ShakeAndBake.Extras.Paths
{
    public class RandomWavePathFactory : PathAbstractFactory
    {
        public override Path Create(Vector2 origin, Vector2 direction, float speed)
        {
            Random random = new Random();
            List<Vector2> points = new List<Vector2>();
            int radius = 8;
            for (int i = 0; i < 3600; ++i)
            {
                float prevY = (i == 0) ? origin.Y : points[i - 1].Y;
                float prevX = (i == 0) ? origin.X : points[i - 1].X;
                Vector2 point = new Vector2(prevX + (float)Math.Sin(i * .1) * radius * (float)random.NextDouble(), speed + prevY);
                points.Add(point);
            }
            return new ListPath(points);
        }
    }
}
