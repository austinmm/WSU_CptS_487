using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShakeAndBake.Extras.Paths;

namespace ShakeAndBake.Extras.Paths
{
    public class SweepRightPathFactory : PathAbstractFactory
    {
        public override Path Create(Vector2 origin, Vector2 direction, float speed)
        {
            List<Vector2> points = new List<Vector2>();

            int angleStart = 90;

            int iteration = 0;
            int arcSpeed = 2; // reduce this to go through arc faster
            for (int i = angleStart * arcSpeed; i >= 0; i--, ++iteration)
            {
                float xComp;
                float yComp;
                float prevX = (i == angleStart * arcSpeed) ? origin.X : points[iteration - 1].X;
                float prevY = (i == angleStart * arcSpeed) ? origin.Y : points[iteration - 1].Y;
                double angle = ((double)i/arcSpeed) * (Math.PI/180);
                xComp = (float)(prevX + Math.Cos(angle) * speed);
                yComp = (float)(prevY + Math.Sin(angle) * speed);
                points.Add(new Vector2(xComp, yComp));
            }
            
            while (points[points.Count-1].X < GameConfig.Width)
            {
                points.Add(new Vector2(points[points.Count - 1].X + speed, points[points.Count - 1].Y));
            }
            return new ListPath(points);
        }
    }
}
