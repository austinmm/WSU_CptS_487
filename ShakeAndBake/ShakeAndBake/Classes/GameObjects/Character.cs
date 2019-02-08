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
    public abstract class Character : GameObject
    {
        //Used to invoke the characters update 
        private event PropertyChangedEventHandler ProjectilePropertyChanged;

        //This contains the amount of time the character has been alive for
        protected Stopwatch timeAlive;
        public Stopwatch TimeAlive
        {
            get { return this.timeAlive; }
            set { this.timeAlive = value; }
        }

        //This contains the time the character last fired a projectile (Milliseconds)
        protected Nullable<double> lastFiredTime;
        public Nullable<double> LastFiredTime
        {
            get { return this.lastFiredTime; }
            set { this.lastFiredTime = value; }
        }

        //This contains the speed at which the character can fire their projectiles (Milliseconds)
        protected double fireRate;
        public double FireRate
        {
            get { return this.fireRate; }
            set { this.fireRate = value; }
        }

        //This contains the amount of health a character has left
        protected double health;
        public double Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        //Tuple???
        //This contains a list of all the projectile types the character is allowed to fire
        protected List<ProjectileTypes> projectileTypes;
        public List<ProjectileTypes> ProjectileTypes
        {
            get { return this.projectileTypes; }
            set { this.projectileTypes = value; }
        }

        //This contains a ObservableCollection of all the projectiles the character currently has
        protected ObservableCollection<Projectile> projectiles;
        public ObservableCollection<Projectile> Projectiles
        {
            get { return this.projectiles; }
            set { this.projectiles = value; }
        }

        public Character() : base()
        {
            //When projectiles are added or removed from the ObservableCollection then "OnProjectileChange" is automatically called
            this.health = 100;
            this.timeAlive = new Stopwatch();
            this.timeAlive.Start();
            this.ProjectileTypes = new List<ProjectileTypes>();
            this.projectiles = new ObservableCollection<Projectile>();
            projectiles.CollectionChanged += OnProjectileChange;
        }
        
        public override void Update()
        {
            if (!this.isDestroyed)
            {
                base.Update();
                this.UpdateProjectiles();
            }
        }

        private void OnProjectileChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                //When a new Enemy is created it
                foreach (Projectile projectile in e.NewItems)
                {
                    projectile.PropertyChanged += Projectile_PropertyChanged;
                }
            }
            if (e.OldItems != null) { }
        }

        public void UpdateProjectiles()
        {
            //Update existing bullets already fired by the character
            foreach (Projectile projectile in this.projectiles)
            {
                projectile.Update();
            }
        }

        //Invoked when a projectile is updated
        private void Projectile_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Type cast sender as a Projectile type
            Projectile projectile = sender as Projectile;
            //Checks if the projectile has hit the user's or enemies, dependant on inheriting classes, hitBoxRadius
            this.CheckForHits(projectile);
            if (this.ProjectilePropertyChanged != null)
            {
                //Passes reference to projectile that changed
                this.ProjectilePropertyChanged(sender, e);
            }
        }

        public virtual void FireProjectile()
        {
            if (this.CanFire())
            {
                //Creates a new projectile to be added to the character's ObservableCollection of projectiles
                Projectile projectile = new Projectile(new StraightPath(this.position, new Vector2(0, 1), 2));
                //The projectiles position is set to the current character's position
                projectile.Position = this.position;
                this.projectiles.Add(projectile);
            }
        }

        protected bool CanFire()
        {
            //character has fired atleast one projectile
            if (this.lastFiredTime.HasValue)
            {
                //if the character has to wait longer until they can fire another projectile
                if (this.lastFiredTime + this.fireRate < this.timeAlive.ElapsedMilliseconds)
                {
                    return false;
                }
            }
            //sets last projectile fired time to current time
            this.lastFiredTime = this.timeAlive.ElapsedMilliseconds;
            return true;
        }

        protected abstract void CheckForHits(Projectile projectile);
    }
}