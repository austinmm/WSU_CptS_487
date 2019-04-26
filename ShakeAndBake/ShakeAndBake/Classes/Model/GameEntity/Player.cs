using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Model.Factories.ProjectileFactory;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace ShakeAndBake.Model.GameEntity
{
    public class Player : Character
    {
        private static Player instance;
        //This contains the time the character last fired a special projectile (Milliseconds)
        private double sp_lastFiredTime = 0.0;

        //This contains the speed at which the character can fire their special projectiles (Milliseconds)
        private double sp_fireRate = 10000;
        //this is the amount of time until the user can fire their special projectile again (Seconds)
        public int SpecialProjectileCountdown
        {
            get {
                int val = (int)(this.sp_lastFiredTime + this.sp_fireRate - this.timeAlive.ElapsedMilliseconds) / 1000;
                return val > 0 ? val : 0;
            }
        }

        // Player singleton
        public static Player Instance
        {
            get 
            {
                if (instance == null) {
                    instance = init();
                }
                return instance;    
            }
        }

        private static Player init()
        {
            StreamReader reader = GameData.GetPlayerStreamReader();
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<Player>(json);
        }

        public static void Reset()
        {
            instance = null;
        }
        
        private bool invincible;
        public bool Invincible
        {
            get { return invincible; }
            set { this.invincible = value; }
        }

        private int score;
        public int Score
        {
            get { return score; }
            set { this.score = value; }
        }

        private Player() : base()
        {
            this.Sprite = ShakeAndBakeGame.GetTexture("player_default");
            this.position = new Vector2
            (
                (GameConfig.Width / 2 - (float)this.hitBoxRadius),
                (GameConfig.Height - this.Sprite.Height)
            );
            //sets special projectile fired time to current time
            this.sp_lastFiredTime = this.timeAlive.ElapsedMilliseconds;
        }

        public void Move(float newX, float newY)
        {
            float oldX = this.position.X, oldY = this.position.Y;
            //Move Player
            this.position.X = newX;
            this.position.Y = newY;
            //If newest user position updates moves the user out of the window 
            if (!this.isInWindow())
            {
                if (!this.IsInWindowWidth())
                {
                    this.position.X = oldX;
                }
                if (!this.IsInWindowHeight())
                {
                    this.position.Y = oldY;
                }
            }
        }

        public override void Update(GameTime gameTime, CollisionBoard cb)
        {
            //Remove player's collision bucket before update
            cb.RemoveFromBucketIfExists(this);

            base.Update(gameTime, cb);
            //Update collision board
            cb.FillBucket(this);
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.isDestroyed)
            {
                if (this.Health == 0)
                {
                    instance = null;
                }
                return;
            }
            base.Draw(spriteBatch);
            spriteBatch.Draw(this.Sprite, position, Color.White);
        }

        public override Vector2 GetCenterCoordinates()
        {
            Vector2 pos = Vector2.Add(position, new Vector2(
               (ShakeAndBakeGame.GetTexture("player_default").Width - ShakeAndBakeGame.GetTexture("player_default_bullet").Width) / 2,
               -ShakeAndBakeGame.GetTexture("player_default_bullet").Height));
            return pos;
        }

        private Vector2 GetSpecialCenterCoordinates()
        {
            Vector2 pos = Vector2.Add(position, new Vector2(
               (ShakeAndBakeGame.GetTexture("player_default").Width - ShakeAndBakeGame.GetTexture("player_special_bullet").Width) / 2,
               -ShakeAndBakeGame.GetTexture("player_special_bullet").Height));
            return pos;
        }

        private bool CanFireSpecialProjectile()
        {
            //if the character has to wait longer until they can fire another projectile
            if (this.timeAlive.ElapsedMilliseconds < this.sp_lastFiredTime + this.sp_fireRate)
            {
                return false;
            }
            sp_lastFiredTime = this.timeAlive.ElapsedMilliseconds;
            return true;
        }

        public override void FireProjectile()
        {
            if (!this.CanFire()) { return; }
            this.ProjectileFactory = new DefaultPlayerProjectileFactory();
            Projectile projectile = this.ProjectileFactory.Create(new Vector2(this.GetCenterCoordinates().X - ShakeAndBakeGame.GetTexture("player_default_bullet").Width/2, this.GetCenterCoordinates().Y - ShakeAndBakeGame.GetTexture("player_default_bullet").Height));
            //The projectiles position is set to the current character's position
            this.projectiles.Add(projectile);
            ShakeAndBakeGame.GetSoundEffect("shot").CreateInstance().Play();
        }

        public void FireSpecialProjectile()
        {
            if (this.CanFireSpecialProjectile())
            {
                this.ProjectileFactory = new SpecialPlayerProjectileFactory();
                Projectile projectile = this.ProjectileFactory.Create(this.GetSpecialCenterCoordinates());
                this.projectiles.Add(projectile);
                ShakeAndBakeGame.GetSoundEffect("shot").CreateInstance().Play();
            }
        }
    }
}
