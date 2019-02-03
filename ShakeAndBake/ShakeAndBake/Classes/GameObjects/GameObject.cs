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
    public abstract class GameObject
    {
        //Used to determine if object has been destroyed or not
        protected bool isDestroyed;
        public bool IsDestroyed
        {
            get { return this.isDestroyed; }
            set { this.isDestroyed = value; }
        }
        //Holds the current position of the object
        protected Vector2 position;
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        //The value that determines how fast the object moves from postion A to position B
        protected double velocity;
        public double Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }
        //The value that determines the acceleration of the object moving from postion A to position B
        protected double acceleration;
        public double Acceleration
        {
            get { return this.acceleration; }
            set { this.acceleration = value; }
        }
        //This is the radius from the objects "position" field that is used to determine if they have made contact with another gameobject
        protected double hitBoxRadius;
        public double HitBoxRadius
        {
            get { return this.hitBoxRadius; }
            set { this.hitBoxRadius = value; }
        }
        //constructor
        public GameObject() { }
        //??? 
        public virtual void Draw() { }
        //Moves the game object to a new position
        public virtual void Update() { }
        static private bool IsInBounds(Vector2 coordinates)
        {
            double Min_Window_X = 0, Max_Window_X = 100;
            double Min_Window_Y = 0, Max_Window_Y = 100;
            if ((coordinates.X > Max_Window_X || coordinates.X < Min_Window_X) ||
                (coordinates.Y > Max_Window_Y || coordinates.Y < Min_Window_Y))
            {
                return false;
            }
            return true;
        }
    }
}
