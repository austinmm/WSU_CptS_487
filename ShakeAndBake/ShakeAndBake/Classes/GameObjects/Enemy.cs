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
    public class Enemy : Character
    {
        //This is a set path that the enemy objects will travel when update is called
        protected List<Vector2> path;
        public List<Vector2> Path
        {
            get { return this.path; }
            set { this.path = value; }
        }
        //Enemy Constructor
        public Enemy() : base() { }
        //Updates the 
        public override void Update()
        {
            base.Update();
            //Fire a new projectile if firerate field will allow
            this.FireProjectile();
            //Move Enemy to new position in its set path
        }
        //Checks if a specific projectile, that just updated its coordinates, has hit our player
        protected override void CheckForHits(Projectile projectile)
        {
            GameBoard.IsHit(GameBoard.User, projectile);
        }
    }
}
