using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ShakeAndBake.Extras.Paths
{
    public abstract class Path
    {
        public virtual void Reset(Nullable<Vector2> origin = null) { }
        public abstract Vector2 NextPoint();
        public abstract Vector2 GetVelocityVector();
        public void SetVelocityVector(Vector2 vector)
        {
            this.velocityOffset = vector;

            // new velocity vector breaks path, deflected bullet
            this.wasDeflected = true;
        }
        public Vector2 velocityOffset = new Vector2(0, 0);
        public abstract bool HasMoved();
        protected Boolean wasDeflected = false;
        public Boolean WasDeflected
        {
            get { return this.wasDeflected; }
            set { this.wasDeflected = value; }
        }
       
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
            this.velocityOffset = direction;
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
            position += (this.velocityOffset) * velocity;
            return position;   
        }

        public override Vector2 GetVelocityVector()
        {
            return (this.velocityOffset) * velocity;
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
            /* Collision makes path break */
            if (!(this.velocityOffset.X == 0 && this.velocityOffset.Y == 0))
            {
                return this.velocityOffset;
            }

            Vector2 point = points[pointIndex++];
            if (pointIndex >= points.Count) {
                pointIndex = 0;
            }
            return point;
        }

        public override Vector2 GetVelocityVector()
        {
            // change in position is velocity
            Vector2 ret;
            if (pointIndex > 0)
            {

                ret = (points[pointIndex] - points[pointIndex - 1]);
            }
            else
            {
                ret = points[pointIndex] - points[points.Count - 1];
            }
            return ret;
        }

        public override bool HasMoved()
        {
            return this.pointIndex == 0;
        }
    }
}
