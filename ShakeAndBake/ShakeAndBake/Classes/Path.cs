using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace GameClasses
{
    public abstract class Path
    {
        public Path()
        {

        }

        public abstract Vector2 NextPoint();
    }

    public class StraightPath : Path
    {
        private Vector2 position, direction;
        private float velocity;

        public StraightPath(Vector2 origin, Vector2 direction, float velocity)
        {
            this.position = origin;
            this.direction = direction;
            this.velocity = velocity;
        }

        public override Vector2 NextPoint()
        {
            // GameBoard should check if objects go out of bounds.
            position += direction * velocity;
            return position;   
        }
    }

    public class ListPath : Path
    {
        private int pointIndex;
        private List<Vector2> points;

        public ListPath(List<Vector2> points)
        {
            this.points = points;
        }

        public List<Vector2> Points
        {
            get { return points; }
            set { this.points = value; }
        }

        public override Vector2 NextPoint()
        {
            Vector2 point = points[pointIndex++];
            if (pointIndex >= points.Count) {
                pointIndex = 0;
            }
            return point;
        }
    }
}
