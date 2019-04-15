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

        private Player() : base()
        {
            this.Sprite = ShakeAndBakeGame.GetTexture("player_default");
            this.position = new Vector2
            (
                (GameConfig.Width / 2 - (float)this.hitBoxRadius),
                (GameConfig.Height - this.Sprite.Height)
            );
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
                if(this.Health == 0)
                {
                    instance = null;
                }
                return;
            }
            base.Draw(spriteBatch);
            spriteBatch.Draw(this.Sprite, position, Color.White);
        }

        public override void FireProjectile()
        {
            if (!this.CanFire()) { return; }
            //Vector2 pos = Vector2.Add(position, new Vector2(
            //    (ShakeAndBakeGame.GetTexture("player_default").Width - ShakeAndBakeGame.GetTexture("player_default_bullet").Width) / 2,
            //    -ShakeAndBakeGame.GetTexture("player_default_bullet").Height));
            //Projectile projectile = new PlayerBullet(new StraightPath(pos, new Vector2(0, -1), 3));
            this.ProjectileFactory = new DefaultPlayerProjectileFactory();
            Projectile projectile = this.ProjectileFactory.Create(this.GetCenterCoordinates());
            //The projectiles position is set to the current character's position
            this.projectiles.Add(projectile);
        }
    }
}
