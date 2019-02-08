namespace GameClasses
{
    public enum EnemyType
    {
        Easy, Medium, Hard, MidBoss, FinalBoss
    }
    
    public class Enemy : Character
    {
        //This is a set path that the enemy objects will travel when update is called
        protected Path path;
        
        public Path Path
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
