using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameClasses
{
    //GameController is static since their can only be one GameController in existance at any given time
    static class GameController
    {
        public enum GameState
        {
            PLAYING, GAMEOVER
        }

        static private GameState gameState;
        static public GameState State
        {
            get { return gameState; }
            set { gameState = value; }
        }

        //Contains a list of all different stages available
        static private List<GameBoardConfigs> stages;
        static public List<GameBoardConfigs> Stages
        {
            get { return stages; }
            set { stages = value; }
        }

        //Contains the index of the current stage
        static private int currentStage;
        static public int CurrentStage
        {
            get { return currentStage; }
            set { currentStage = value; }
        }

        //Constructor
        static GameController()
        {
            stages = new List<GameBoardConfigs>();
            stages.Add(GameBoardConfigs.Phase1);
            stages.Add(GameBoardConfigs.Phase2);
            stages.Add(GameBoardConfigs.Phase3);
            stages.Add(GameBoardConfigs.Phase4);

            State = GameState.PLAYING;
            currentStage = 0;
            ConfigureNextStage();
        }

        //Changes the GameBoard class to reflect the current stage
        static public void ConfigureNextStage()
        {
            GameBoard.Reset();
            GameBoardConfigs phase = stages[currentStage];
            switch (phase)
            {
                case GameBoardConfigs.Phase1:
                    for (int i = 0; i < 5; i++) {
                        GameBoard.AddEnemy(EnemyType.Easy);
                    }
                    for (int i = 0; i < 2; i++) {
                        GameBoard.AddEnemy(EnemyType.Medium);
                    }
                    break;
                case GameBoardConfigs.Phase2:
                    for (int i = 0; i < 2; i++) {
                        GameBoard.AddEnemy(EnemyType.MidBoss);
                    }
                    break;
                case GameBoardConfigs.Phase3:
                    for (int i = 0; i < 3; i++) {
                        GameBoard.AddEnemy(EnemyType.Medium);
                    }
                    for (int i = 0; i < 2; i++) {
                        GameBoard.AddEnemy(EnemyType.Hard);
                    }
                    break;
                case GameBoardConfigs.Phase4:
                    GameBoard.AddEnemy(EnemyType.FinalBoss);
                    break;
            }
        }
        
        //Checks if current stage has finished
        static public void CheckBoard(GameTime gameTime)
        {
            //if no enemies left then update to next stage
            if (GameBoard.VisibleEnemies.Count == 0)
            {
                //New stage
                if (++currentStage >= stages.Count)
                {
                    State = GameState.GAMEOVER;
                    return;
                }
                ConfigureNextStage();
            }
        }
    }
}
