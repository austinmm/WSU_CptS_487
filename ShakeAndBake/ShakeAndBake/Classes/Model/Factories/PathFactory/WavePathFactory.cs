using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;

namespace ShakeAndBake.Classes.Model.Factories.PathFactory
{
    class WavePathFactory : PathAbstractFactory
    {
        public override Path Create(Vector2 origin, Vector2 direction, float speed)
        {
            List<Vector2> points = new List<Vector2>();
            int radius = 2;
            for (int i = 0; i < 3600; ++i)
            {
                float prevY = (i == 0) ? origin.Y : points[i - 1].Y;
                float prevX = (i == 0) ? origin.X : points[i - 1].X;
                Vector2 point = new Vector2(prevX + (float)Math.Sin(i*.1) * radius, speed + prevY );
                points.Add(point);
            }
            return new ListPath(points);
        }
    }
}
