using Microsoft.Xna.Framework;
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

namespace GameClasses
{
    public class Player : Character
    {
        public Player() : base() { }
        public override void Update()
        {
            KeyboardState state = Keyboard.GetState();//gets the state of the keyboard and checks the combos as follows
            //down right
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Up)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                this.position.X = Position.X + (float)(Velocity * Acceleration);
                this.position.Y = Position.Y - (float)(Velocity * Acceleration);
            }
            //up right
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Down)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                this.position.X = Position.X + (float)(Velocity * Acceleration);
                this.position.Y = Position.Y + (float)(Velocity * Acceleration);
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
                this.position.Y = Position.Y - (float)(Velocity * Acceleration);
            }
            //left up
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Right)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                this.position.X = Position.X - (float)(Velocity * Acceleration);
                this.position.Y = Position.Y + (float)(Velocity * Acceleration);
            }
            //left
            if (state.IsKeyDown(Keys.Left) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Right)))
            {
                this.position.X = Position.X - (float)(Velocity * Acceleration);
            }
            //up
            if (state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Right)) && !(state.IsKeyDown(Keys.Down)))
            {
                this.position.Y = Position.Y + (float)(Velocity * Acceleration);
            }
            //down
            if (state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Right)) && !(state.IsKeyDown(Keys.Up)))
            {
                this.position.Y = Position.Y - (float)(Velocity * Acceleration);
            }
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
}
