using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using ShakeAndBake;

namespace GameClasses
{
    public class Projectile : GameObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //This is a set path that the projectile objects will travel when update is called
        protected Path path;
        public Path Path
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
        public Projectile(Path path)
        {
            this.path = path;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (position.Y + this.sprite.Height < 0) 
            {
                IsDestroyed = true;
                return;
            }

            //Update the position of the projectile based off of its spritePath
            position = path.NextPoint();
            
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

    public class PlayerBullet : Projectile
    {
        public PlayerBullet(Path path) : base(path)
        {
            sprite = ShakeAndBakeGame.playerBullet;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
