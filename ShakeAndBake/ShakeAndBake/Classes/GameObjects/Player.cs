using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
using ShakeAndBake;

namespace GameClasses
{
    public class Player : Character
    {
        public Player() : base() {

        }
        
        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(" " + Keyboard.GetState());
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
                this.FireProjectile();
            }

            Velocity = 3;
            Acceleration = 1;

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
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ShakeAndBakeGame.player, position, Color.White);
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
