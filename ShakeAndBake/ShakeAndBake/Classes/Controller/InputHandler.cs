using Microsoft.Xna.Framework.Input;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Controller
{
    class InputHandler
    {
        public void UpdateGameSpeed(KeyboardState state)
        {
            // Utilizes the left control as the switch between speed
            // modes essentially halving the speed when pressed.
            if (state.IsKeyDown(Keys.S))
            {
                GameConfig.GameSpeed = 2;
            } else {
                GameConfig.GameSpeed = 1;
            }
        }
        
        public void FireUserProjectile(KeyboardState state)
        {
            // Spacebar fires user projectile.
            if (state.IsKeyDown(Keys.Space))
            {
                Player.Instance.FireProjectile();
            }
        }

        public bool DidUserMove(KeyboardState state, out float newX, out float newY)
        {
            float originalX = Player.Instance.Position.X;
            float originalY = Player.Instance.Position.Y;
            newX = originalX;
            newY = originalY;
            //variables to hold calculations preventing redundant calculations
            float movementSpeed = (float)(Player.Instance.Velocity * Player.Instance.Acceleration);
            //down right
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Up)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                newX = originalX + movementSpeed;
                newY = originalY + movementSpeed;
            }
            //up right
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Down)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                newX = originalX + movementSpeed;
                newY = originalY - movementSpeed;
            }
            //right
            if (state.IsKeyDown(Keys.Right) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Left)))
            {
                newX = originalX + movementSpeed;
            }
            //left down
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Right)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                newX = originalX - movementSpeed;
                newY = originalY + movementSpeed;
            }
            //left up
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Right)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                newX = originalX - movementSpeed;
                newY = originalY - movementSpeed;
            }
            //left
            if (state.IsKeyDown(Keys.Left) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Right)))
            {
                newX = originalX - movementSpeed;
            }
            //up
            if (state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Right)) && !(state.IsKeyDown(Keys.Down)))
            {
                newY = originalY - movementSpeed;
            }
            //down
            if (state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Right)) && !(state.IsKeyDown(Keys.Up)))
            {
                newY = originalY + movementSpeed;
            }
            //returns true if the player was moved, false otherwise
            return (newX != originalX || newY != originalY) ? true : false;
        }
    }
}