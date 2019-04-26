using Microsoft.Xna.Framework.Input;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.View;
using System;

namespace ShakeAndBake.Controller
{
    class InputHandler
    {
        public void UpdateGameSpeed(KeyboardState state)
        {
            // Utilizes the left control as the switch between speed
            // modes essentially halving the speed when pressed.
            if (state.IsKeyDown(Keys.G))
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

        public MenuState MenuMove(GameController controller, KeyboardState state, KeyboardState previousState, MenuState menuStateIn, out GameState newGameState)
        {
            if (state.IsKeyDown(Keys.Right) && previousState.IsKeyUp(Keys.Right)) {
                DifficultyLevel newlvl = DifficultyLevel.Easy;
                DifficultyLevel curlvl = controller.StageManager.CurrentLevel;
                if (curlvl == DifficultyLevel.Easy) {
                    newlvl = DifficultyLevel.Normal;
                } else if (curlvl == DifficultyLevel.Normal) {
                    newlvl = DifficultyLevel.Hard;
                } else if (curlvl == DifficultyLevel.Hard) {
                    newlvl = DifficultyLevel.Lunatic;
                } else if (curlvl == DifficultyLevel.Lunatic) {
                    newlvl = DifficultyLevel.Lunatic;
                }
                controller.StageManager.CurrentLevel = newlvl;
            }
            else if (state.IsKeyDown(Keys.Left) && previousState.IsKeyUp(Keys.Left)) {
                DifficultyLevel newlvl = DifficultyLevel.Easy;
                DifficultyLevel curlvl = controller.StageManager.CurrentLevel;
                if (curlvl == DifficultyLevel.Easy) {
                    newlvl = DifficultyLevel.Easy;
                } else if (curlvl == DifficultyLevel.Normal) {
                    newlvl = DifficultyLevel.Easy;
                } else if (curlvl == DifficultyLevel.Hard) {
                    newlvl = DifficultyLevel.Normal;
                } else if (curlvl == DifficultyLevel.Lunatic) {
                    newlvl = DifficultyLevel.Hard;
                }
                controller.StageManager.CurrentLevel = newlvl;
            }
            else if (state.IsKeyDown(Keys.Down) && previousState.IsKeyUp(Keys.Down))
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
            else if (state.IsKeyDown(Keys.Up) && previousState.IsKeyUp(Keys.Up))
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
            else if (state.IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                switch (menuStateIn)
                {
                    case MenuState.START:
                        newGameState = GameState.PLAYING;
                        return MenuState.START;
                    case MenuState.SETTINGS:
                        newGameState = GameState.MENU;
                        controller.ScreenManager.SetScreen(ScreenType.SETTINGS);
                        return MenuState.SETTINGS;                      
                    case MenuState.EXIT:
                        newGameState = GameState.EXIT;
                        return MenuState.EXIT;
                }
            }
            newGameState = GameState.MENU;
            return menuStateIn;
        }

        public void SettingsMenuMove(GameController controller, KeyboardState state, KeyboardState previousState)
        {
            if (state.IsKeyDown(Keys.Up))
            {
                GameConfig.MoveKeys = MoveKeys.ARROW;
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                GameConfig.MoveKeys = MoveKeys.WASD;
            }
            else if (state.IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                controller.ScreenManager.SetScreen(ScreenType.START);
            }
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
            else if (state.IsKeyDown(Keys.P))
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
            Keys leftKey = Keys.Space, rightKey = Keys.Space, upKey = Keys.Space, downKey = Keys.Space;
            if (GameConfig.MoveKeys == MoveKeys.ARROW) {
                leftKey = Keys.Left;
                rightKey = Keys.Right;
                upKey = Keys.Up;
                downKey = Keys.Down;
            } else if (GameConfig.MoveKeys == MoveKeys.WASD) {
                leftKey = Keys.A;
                rightKey = Keys.D;
                upKey = Keys.W;
                downKey = Keys.S;
            }
            if (state.IsKeyDown(rightKey) && state.IsKeyDown(downKey) && !(state.IsKeyDown(leftKey)) && !(state.IsKeyDown(upKey)))
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
            if (state.IsKeyDown(rightKey) && state.IsKeyDown(upKey) && !(state.IsKeyDown(leftKey)) && !(state.IsKeyDown(downKey)))
            {
                newX = originalX + movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY - movementSpeed * (float)Math.Sin(Math.PI / 4);
            }
            //right
            if (state.IsKeyDown(rightKey) && !(state.IsKeyDown(upKey)) && !(state.IsKeyDown(downKey)) && !(state.IsKeyDown(leftKey)))
            {
                newX = originalX + movementSpeed;
            }
            //left down
            if (state.IsKeyDown(leftKey) && state.IsKeyDown(downKey) && !(state.IsKeyDown(upKey)) && !(state.IsKeyDown(rightKey)))
            {
                newX = originalX - movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY + movementSpeed * (float)Math.Sin(Math.PI / 4);
            }
            //left up
            if (state.IsKeyDown(leftKey) && state.IsKeyDown(upKey) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(rightKey)))
            {
                newX = originalX - movementSpeed * (float)Math.Cos(Math.PI / 4);
                newY = originalY - movementSpeed * (float)Math.Sin(Math.PI / 4);
            }
            //left
            if (state.IsKeyDown(leftKey) && !(state.IsKeyDown(upKey)) && !(state.IsKeyDown(downKey)) && !(state.IsKeyDown(rightKey)))
            {
                newX = originalX - movementSpeed;
            }
            //up
            if (state.IsKeyDown(upKey) && !(state.IsKeyDown(leftKey)) && !(state.IsKeyDown(rightKey)) && !(state.IsKeyDown(downKey)))
            {
                newY = originalY - movementSpeed;
            }
            //down
            if (state.IsKeyDown(downKey) && !(state.IsKeyDown(leftKey)) && !(state.IsKeyDown(rightKey)) && !(state.IsKeyDown(upKey)))
            {
                newY = originalY + movementSpeed;
            }
            //returns true if the player was moved, false otherwise
            return (newX != originalX || newY != originalY) ? true : false;
        }
    }
}
