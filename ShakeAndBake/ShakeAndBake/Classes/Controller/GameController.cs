using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace ShakeAndBake.Controller
{
    //This Class is the "Controller" of our Game's MVC Architecture 
    public class GameController
    {
        private GameState gameState;
        public GameState State
        {
            get { return gameState; }
            set { gameState = value; }
        }

        //Contains a list of all different phases available
        private List<GameBoardConfigs> phases;
        public List<GameBoardConfigs> Phases
        {
            get { return phases; }
            set { phases = value; }
        }

        //Contains the index of the current phase
        private int currentPhase;
        public int Currentphase
        {
            get { return currentPhase; }
            set { currentPhase = value; }
        }

        //Contains a reference to the current this.gameData instance
        private Model.GameData gameData;

        //Contains a reference to the current GameBoard instance
        private View.GameBoard gameBoard;

        //Constructor
        public GameController(Model.GameData data, View.GameBoard board)
        {
            this.gameData = data;
            this.gameBoard = board;
            this.phases = new List<GameBoardConfigs>();
            this.phases.Add(GameBoardConfigs.Phase1);
            this.phases.Add(GameBoardConfigs.Phase2);
            this.phases.Add(GameBoardConfigs.Phase3);
            this.phases.Add(GameBoardConfigs.Phase4);
            this.State = GameState.PLAYING;
            this.currentPhase = 0;
            ConfigureNextPhase();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();//gets the state of the keyboard and checks the combos as follows
            this.UpdateGameSpeed(state);
            this.FireUserProjectile(state);
            if(this.DidUserMove(state, out float newX, out float newY))
            {
                this.gameData.User.Move(newX, newY);
            }
            this.gameData.Update(gameTime);
            this.gameBoard.Update(gameTime);
            this.CheckBoard();
        }

        private void UpdateGameSpeed(KeyboardState state)
        {
            //utilizes the left control as the switch between speed modes essentially halving the speed when pressed.
            if (state.IsKeyDown(Keys.S))
            {
                GameConfig.GameSpeed = 2;
            }
            else
            {
                GameConfig.GameSpeed = 1;
            }
        }

        private void FireUserProjectile(KeyboardState state)
        {
            //Spacebar fires user projectile
            if (state.IsKeyDown(Keys.Space))
            {
                this.gameData.User.FireProjectile();
            }
        }
    
        public bool DidUserMove(KeyboardState state, out float newX, out float newY)
        {
            float originalX = this.gameData.User.Position.X;
            float originalY = this.gameData.User.Position.Y;
            newX = originalX;
            newY = originalY;
            //variables to hold calculations preventing redundant calculations
            float movementSpeed = (float)(this.gameData.User.Velocity * this.gameData.User.Acceleration);
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

        //Changes the GameBoard class to reflect the current phase
        public void ConfigureNextPhase()
        {
            this.gameData.Reset();
            GameBoardConfigs phase = phases[currentPhase];
            switch (phase)
            {
                case GameBoardConfigs.Phase1:
                    for (int i = 0; i < 5; i++) {
                        this.gameData.AddEnemy(EnemyType.Easy);
                    }
                    for (int i = 0; i < 2; i++) {
                        this.gameData.AddEnemy(EnemyType.Medium);
                    }
                    break;
                case GameBoardConfigs.Phase2:
                    for (int i = 0; i < 2; i++) {
                        this.gameData.AddEnemy(EnemyType.MidBoss);
                    }
                    break;
                case GameBoardConfigs.Phase3:
                    for (int i = 0; i < 3; i++) {
                        this.gameData.AddEnemy(EnemyType.Medium);
                    }
                    for (int i = 0; i < 2; i++) {
                        this.gameData.AddEnemy(EnemyType.Hard);
                    }
                    break;
                case GameBoardConfigs.Phase4:
                    this.gameData.AddEnemy(EnemyType.FinalBoss);
                    break;
            }
        }
        
        //Checks if current phase has finished
        public void CheckBoard()
        {
            //if no enemies left then update to next phase
            if (this.gameData.VisibleEnemies.Count == 0)
            {
                //New phase
                if (++currentPhase >= phases.Count)
                {
                    this.State = GameState.GAMEOVER;
                }
                else { this.ConfigureNextPhase(); }
            }
            else if (this.gameData.IsUserDead)
            {
                this.State = GameState.GAMEOVER;
            }
        }
    }
}
