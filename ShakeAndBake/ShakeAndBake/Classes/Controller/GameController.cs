using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
            this.gameData.Update(gameTime);
            this.gameBoard.Update(gameTime);
            this.CheckBoard();
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
