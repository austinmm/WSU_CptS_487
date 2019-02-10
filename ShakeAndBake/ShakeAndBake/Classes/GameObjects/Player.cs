using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShakeAndBake;

namespace GameClasses
{
    public class Player : Character
    {
        public Player() : base() {
            Velocity = 3;
            Acceleration = 2;
            FireRate = 40;
            sprite = ShakeAndBakeGame.player;
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState state = Keyboard.GetState();//gets the state of the keyboard and checks the combos as follows
            //utilizes the left control as the switch between speed modes essentially halving the speed when pressed.
            if (state.IsKeyDown(Keys.LeftControl))
            {
                GameBoard.GameSpeed = 1;
            }
            if (state.IsKeyUp(Keys.LeftControl))
            {
                GameBoard.GameSpeed = 2;
            }
            //Spacebar fires user projectile
            if (state.IsKeyDown(Keys.Space))
            {
                FireProjectile();
            }

            if (!isInWindow())
            {    
                return;
            }


            //down right
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Up)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                this.position.X = Position.X + (float)(Velocity * Acceleration);
                this.position.Y = Position.Y + (float)(Velocity * Acceleration);
            }
            //up right
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Down)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                this.position.X = Position.X + (float)(Velocity * Acceleration);
                this.position.Y = Position.Y - (float)(Velocity * Acceleration);
            }
            //right
            if (state.IsKeyDown(Keys.Right) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Left)))
            {
                this.position.X = Position.X + (float)(Velocity * Acceleration);
            }
            //left down
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Right)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                this.position.X = Position.X - (float)(Velocity * Acceleration);
                this.position.Y = Position.Y + (float)(Velocity * Acceleration);
            }
            //left up
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Right)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                this.position.X = Position.X - (float)(Velocity * Acceleration);
                this.position.Y = Position.Y - (float)(Velocity * Acceleration);
            }
            //left
            if (state.IsKeyDown(Keys.Left) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Right)))
            {
                this.position.X = Position.X - (float)(Velocity * Acceleration);
            }
            //up
            if (state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Right)) && !(state.IsKeyDown(Keys.Down)))
            {
                this.position.Y = Position.Y - (float)(Velocity * Acceleration);
            }
            //down
            if (state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Right)) && !(state.IsKeyDown(Keys.Up)))
            {
                this.position.Y = Position.Y + (float)(Velocity * Acceleration);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(ShakeAndBakeGame.player, position, Color.White);
        }

        public override void FireProjectile()
        {
            if (!this.CanFire()) return;
            Vector2 pos = Vector2.Add(position, new Vector2((ShakeAndBakeGame.player.Width - ShakeAndBakeGame.playerBullet.Width) / 2,
             -ShakeAndBakeGame.playerBullet.Height));
            Projectile projectile = new PlayerBullet(new StraightPath(pos, new Vector2(0, -1), 3));
            projectile.Sprite = ShakeAndBakeGame.playerBullet;
            
            //The projectiles position is set to the current character's position
            projectile.Position = pos;
            this.projectiles.Add(projectile);
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
}
