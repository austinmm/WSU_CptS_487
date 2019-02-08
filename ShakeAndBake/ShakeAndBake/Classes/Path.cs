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
    public class Path
    {
        private List<Vector2> points;
        private int pointIndex;

        public Path() {

        }

        public List<Vector2> Points {
            get { return points; }
            set { this.points = value; }
        }

        public Vector2 nextPoint() {
            Vector2 point = points[pointIndex];
            pointIndex++;
            if (pointIndex >= points.Count) {
                pointIndex = 0;
            }
            return point;
        }
    }
}
