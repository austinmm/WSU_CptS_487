using Microsoft.Xna.Framework.Input;
using ShakeAndBake.Model.GameEntity;
using System;

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
            Player.Instance.Invincible = keyboardState.IsKeyDown(Keys.C);
            
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
                        return MenuState.SETTINGS;
                    case MenuState.SETTINGS:
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
                    case MenuState.SETTINGS:
                        return MenuState.START;
                    case MenuState.EXIT:
                        return MenuState.SETTINGS;
                }
            }
            else if (state.IsKeyDown(Keys.Enter) & !previousState.IsKeyDown(Keys.Enter))
            {
                switch (menuStateIn)
                {
                    case MenuState.START:
                        newGameState = GameState.PLAYING;
                        return MenuState.START;
                    case MenuState.SETTINGS:
                        newGameState = GameState.PLAYING;
                        return MenuState.SETTINGS;                      
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
            else if (state.IsKeyDown(Keys.S))
            {
                Player.Instance.FireSpecialProjectile();
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
                /***
                 * Magnitude of net velocity should be = movementSpeed
                 * 
                 * |      /   <--- magnitude
                 * |    /
                 * |  /      theta = 45 degrees
                 * |/_______
                 * 
                 * Sin(45) = yComp/magnitude
                 * yComp = magnitude * Sin(45)
                 * 
                 * Cos(45) = xComp/magnitude
                 * xComp = magnitude * Cos(45)
                 ***/
                newX = originalX + movementSpeed * (float)Math.Cos(Math.PI/4);
                newY = originalY + movementSpeed * (float)Math.Sin(Math.PI / 4);
                
            }
            //up right
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Down)))
            {
                newX = originalX + movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY - movementSpeed * (float)Math.Sin(Math.PI / 4);
            }
            //right
            if (state.IsKeyDown(Keys.Right) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Left)))
            {
                newX = originalX + movementSpeed;
            }
            //left down
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Right)))
            {
                newX = originalX - movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY + movementSpeed * (float)Math.Sin(Math.PI / 4);
            }
            //left up
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Right)))
            {
                newX = originalX - movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY - movementSpeed * (float)Math.Sin(Math.PI / 4);
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