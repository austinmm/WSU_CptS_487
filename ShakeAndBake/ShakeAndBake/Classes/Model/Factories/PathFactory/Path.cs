using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ShakeAndBake.Extras.Paths
{
    public abstract class Path
    {
        public virtual void Reset(Nullable<Vector2> origin = null) { }
        public abstract Vector2 NextPoint();
        public abstract bool HasMoved();
    }

    public class StraightPath : Path
    {
        private Vector2 savedOriginal;
        private Vector2 position, direction;
        private float velocity;

        public StraightPath(Vector2 origin, Vector2 direction, float velocity)
        {
            this.savedOriginal = origin;
            this.position = origin;
            this.direction = direction;
            this.velocity = velocity;
        }

        public override void Reset(Nullable<Vector2> origin = null)
        {
            if (origin == null)
            {
                position = savedOriginal;
            }
            else
            {
                position = savedOriginal = origin.Value;
            }
        }

        public override Vector2 NextPoint()
        {
            // GameBoard should check if objects go out of bounds.
            position += direction * velocity;
            return position;   
        }

        public override bool HasMoved()
        {
            return this.position != this.savedOriginal;
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

        public override void Reset(Nullable<Vector2> origin = null)
        {
            pointIndex = 0;
        }

        public override Vector2 NextPoint()
        {
            Vector2 point = points[pointIndex++];
            if (pointIndex >= points.Count) {
                pointIndex = 0;
            }
            return point;
        }

        public override bool HasMoved()
        {
            return this.pointIndex == 0;
        }
    }
}
