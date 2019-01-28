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
    //https://docs.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1?view=netframework-4.7.2
    //https://dotnetcodr.com/2015/05/29/getting-notified-when-collection-changes-with-observablecollection-in-c-net/
    public enum GameBoardConfigs { Stage1, Stage2, Stage3 }
    public static class EnemyPaths
    {
        static public List<Vector2> EasyPath = new List<Vector2>();
        static public List<Vector2> MediumPath = new List<Vector2>();
        static public List<Vector2> HardPath = new List<Vector2>();
        static public List<Vector2> BossPath = new List<Vector2>();
        static EnemyPaths()
        {
            //Read in the different paths from file.
        }
    }
    public static class ProjectilePaths
    {
        static public List<Vector2> EasyPath = new List<Vector2>();
        static public List<Vector2> MediumPath = new List<Vector2>();
        static public List<Vector2> HardPath = new List<Vector2>();
        static public List<Vector2> BossPath = new List<Vector2>();
        static ProjectilePaths()
        {
            //Read in the different paths from file.
        }
    }
    public enum ProjectileTypes { Bullet, Rocket, FireBall }
    //GameController is static since their can only be one GameController in existance at any given time
    static class GameController
    {
        //Maybe Make a Tuple??
        //Contains a list of all different stages available
        static private List<GameBoardConfigs> stages;
        static public List<GameBoardConfigs> Stages
        {
            get { return stages; }
            set { stages = value; }
        }
        //Contains the index of the current stage
        static private int currentStage;
        static public int CurrentStage
        {
            get { return currentStage; }
            set { currentStage = value; }
        }
        //Constructor
        static GameController()
        {
            currentStage = 1;
            ConfigureNextStage();
        }
        //Changes the GameBoard class to reflect the current stage
        static public void ConfigureNextStage() { }
        //Checks if current stage has finished
        static public void CheckBoard()
        {
            //if no enemies left then update stage
            int enemyCount = GameBoard.VisibleEnemies.Count;
            if (enemyCount == 0 && GameBoard.EnemiesLeft == 0)
            {
                //New stage
                CurrentStage++;
                ConfigureNextStage();
            }
        }
    }
    //GameBoard is static since their can only be one GameBoard in existance at any given time
    static class GameBoard
    {
        //Contains a list of all enemies currently visible on the gameboard
        static private int enemiesLeft;
        static public int EnemiesLeft
        {
            get { return enemiesLeft; }
            set { enemiesLeft = value; }
        }
        //Contains a list of all enemies currently visible on the gameboard
        static private ObservableCollection<Enemy> visibleEnemies;
        static public ObservableCollection<Enemy> VisibleEnemies
        {
            get { return visibleEnemies; }
            set { visibleEnemies = value; }
        }
        //Contains a list of all enemies that have dies but still have projectiles on the gameboard
        static private List<Enemy> deadEnemies;
        static public List<Enemy> DeadEnemies
        {
            get { return deadEnemies; }
            set { deadEnemies = value; }
        }
        //This is our one and only player that is controlled by the user (keyboard input)
        static private Player user;
        static public Player User
        {
            get { return user; }
            set { user = value; }
        }
        //Constructor for GameBoard class
        static GameBoard()
        {
            visibleEnemies = new ObservableCollection<Enemy>();
            deadEnemies = new List<Enemy>();
            user = new Player();
            //When enemy is added or removed from collection "updateEnimies" is automatically called
            visibleEnemies.CollectionChanged += UpdateEnemies;
        }
        //When an Enemy is added or removed from the ObservableCollection "visibleEnemies"
        static public void UpdateEnemies(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Enemies that have been added
            if (e.NewItems != null)
            {
                //When an Enemy is created
            }
            //Enemies that have been removed
            if (e.OldItems != null)
            {
                //Each enemy is added to our deadEnemy list
                foreach (Enemy deadEnemy in e.OldItems)
                {
                    deadEnemies.Add(deadEnemy);
                }
            }
        }
        //MultiThread this Method call
        static public void UpdateBoard()
        {
            //Call update on all our visible enemies, this will automatically update their projectiles as well
            foreach (Enemy enemy in visibleEnemies)
            {
                enemy.Update();
            }
            //check deadEnimies list to see if they have any projectiles left on the board
            foreach (Enemy enemy in deadEnemies)
            {
                enemy.Update();
                if (enemy.Projectiles.Count == 0)
                {
                    deadEnemies.Remove(enemy);
                }
            }
            user.Update();
        }
        static public bool IsHit(Character character, Projectile projectile)
        {
            if (character != null && projectile != null)
            {
                //Unsure if logic is corrects
                double xLow = character.Position.X - character.HitBoxRadius;
                double xHigh = character.Position.X - character.HitBoxRadius;
                double yLow = character.Position.Y - character.HitBoxRadius;
                double yHigh = character.Position.Y - character.HitBoxRadius;
                if (projectile.Position.X >= xLow && projectile.Position.X >= xHigh
                    && projectile.Position.Y >= yLow && projectile.Position.Y >= yHigh)
                {
                    //character is Hit
                    character.Health -= projectile.HitDamage;
                    //Check if character is dead
                    if (character.Health < 0)
                    {
                        //if character is the player
                        if (character is Player)
                        {
                            GameBoard.PlayerDied();
                        }
                        //if character is an enemy
                        else if (character is Enemy)
                        {
                            GameBoard.visibleEnemies.Remove((Enemy)character);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        static public void PlayerDied() { }
    }
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
    public class Projectile : GameObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //This is a set path that the projectile objects will travel when update is called
        protected List<Vector2> path;
        public List<Vector2> Path
        {
            get { return this.path; }
            set { this.path = value; }
        }
        //This is the type of the projectile which will determine its image, path, hitDamage
        private ProjectileTypes type;
        public ProjectileTypes Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        //This is the amount of damage an enemy or player will take if hit by this projectile
        private double hitDamage;
        public double HitDamage
        {
            get { return this.hitDamage; }
            set { this.hitDamage = value; }
        }
        //Projectile Constructor
        public Projectile()
        {
            //changed based on projectile type
            this.path = ProjectilePaths.EasyPath;
        }
        public override void Update()
        {
            base.Update();
            //Update the position of the projectile based off of its spritePath
            //Tell the event handler that the projectile has moved and thus it needs to check if it has hit an enemy or player
            RaisePropertyChanged("Projectile_Position_Changed");
        }
        //https://github.com/jbe2277/waf/wiki/Implementing-and-usage-of-INotifyPropertyChanged
        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        //Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(PropertyChangedEventArgs property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, property);
            }
        }
    }
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
                Projectile projectile = new Projectile();
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
    public class Player : Character
    {
        public Player() : base() { }
        public override void Update()
        {
            base.Update();
        }
        //Checks if any of the player's projectiles have hit the visable enimies on the GameBoard
        protected override void CheckForHits(Projectile projectile)
        {
            foreach (Enemy enemy in GameBoard.VisibleEnemies)
            {
                GameBoard.IsHit(enemy, projectile);
            }
        }
    }
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
    public class Easy : Enemy
    {
        //Easy Enemy Constructor
        public Easy() : base()
        {
            this.path = EnemyPaths.EasyPath;
        }
        public override void Update()
        {
            base.Update();
            //Move Enemy to new position in its set path
        }
    }
    public class Medium : Enemy
    {
        //Medium Enemy Constructor
        public Medium() : base()
        {
            this.path = EnemyPaths.MediumPath;
        }
        public override void Update()
        {
            base.Update();
            //Move Enemy to new position in its set path
        }
    }
    public class Hard : Enemy
    {
        //Hard Enemy Constructor
        public Hard() : base()
        {
            this.path = EnemyPaths.HardPath;
        }
        public override void Update()
        {
            base.Update();
            //Move Enemy to new position in its set path
        }
    }
    public class Boss : Enemy
    {
        //Boss Enemy Constructor
        public Boss() : base()
        {
            this.path = EnemyPaths.BossPath;
        }
        public override void Update()
        {
            base.Update();
            //Move Enemy to new position in its set path
        }
    }
}