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

        public void HandleGamePlayInput(KeyboardState keyboardState)
        {
            this.UpdateGameSpeed(keyboardState);
            this.FireUserProjectile(keyboardState);
            if (this.DidUserMove(keyboardState, out float newX, out float newY))
            {
                Player.Instance.Move(newX, newY);
            }
        }

        public MenuState MenuMove(KeyboardState state, KeyboardState previousState, MenuState menuStateIn, out GameState newGameState)
        {
            if (state.IsKeyDown(Keys.Down))
            {
                newGameState = GameState.MENU;
                switch (menuStateIn)
                {
                    case MenuState.START:
                        return MenuState.EXIT;
                    case MenuState.EXIT:
                        return MenuState.EXIT;
                }
            }
            else if(state.IsKeyDown(Keys.Up))
            {
                newGameState = GameState.MENU;
                switch (menuStateIn)
                {
                    case MenuState.START:
                        return MenuState.START;
                    case MenuState.EXIT:
                        return MenuState.START;
                }
            }
            else if (state.IsKeyDown(Keys.Enter) & !previousState.IsKeyDown(Keys.Enter))
            {
                switch (menuStateIn)
                {
                    case MenuState.START:
                        newGameState = GameState.PLAYING;
                        return MenuState.START;
                    case MenuState.EXIT:
                        newGameState = GameState.EXIT;
                        return MenuState.EXIT;
                }
            }

            newGameState = GameState.MENU;
            return menuStateIn;

        }
        
        public EndMenuState EndMenuMove(KeyboardState state, EndMenuState endMenuStateIn, out GameState newGameState)
        {
            newGameState = GameState.GAMEOVER;
            if (state.IsKeyDown(Keys.Down))
            {
                switch (endMenuStateIn)
                {
                    case EndMenuState.MAIN:
                        return EndMenuState.EXIT;
                    case EndMenuState.EXIT:
                        return EndMenuState.EXIT;
                }
            }
            else if (state.IsKeyDown(Keys.Up))
            {
                switch (endMenuStateIn)
                {
                    case EndMenuState.MAIN:
                        return EndMenuState.MAIN;
                    case EndMenuState.EXIT:
                        return EndMenuState.MAIN;
                }
            }
            else if (state.IsKeyDown(Keys.Enter))
            {
                switch (endMenuStateIn)
                {
                    case EndMenuState.MAIN:
                        newGameState = GameState.RESET;
                        return EndMenuState.MAIN;
                    case EndMenuState.EXIT:
                        newGameState = GameState.EXIT;
                        return EndMenuState.EXIT;
                }
            }
            return endMenuStateIn;
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