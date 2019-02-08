﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameClasses
{
    //GameController is static since their can only be one GameController in existance at any given time
    static class GameController
    {
        //Maybe Make a Tuple??
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
            stages.Add(GameBoardConfigs.Stage1);
            stages.Add(GameBoardConfigs.Stage2);
            stages.Add(GameBoardConfigs.Stage3);
            
            currentStage = 0;
            ConfigureNextStage();
        }

        //Changes the GameBoard class to reflect the current stage
        static public void ConfigureNextStage()
        {
            GameBoardConfigs stage = stages[currentStage];
            switch (stage)
            {
                case GameBoardConfigs.Stage1:
                    for (int i = 0; i < 3; i++) {
                        GameBoard.VisibleEnemies.Add(EnemeyFactory.CreateEnemy(EnemyType.Easy));
                    }
                    for (int i = 0; i < 2; i++) {
                        GameBoard.VisibleEnemies.Add(EnemeyFactory.CreateEnemy(EnemyType.Medium));
                    }
                    break;
                case GameBoardConfigs.Stage2:
                    for (int i = 0; i < 3; i++) {
                        GameBoard.VisibleEnemies.Add(EnemeyFactory.CreateEnemy(EnemyType.Medium));
                    }
                    GameBoard.VisibleEnemies.Add(EnemeyFactory.CreateEnemy(EnemyType.MidBoss));
                    break;
                case GameBoardConfigs.Stage3:
                    for (int i = 0; i < 2; i++) {
                        GameBoard.VisibleEnemies.Add(EnemeyFactory.CreateEnemy(EnemyType.Hard));
                    }
                    GameBoard.VisibleEnemies.Add(EnemeyFactory.CreateEnemy(EnemyType.FinalBoss));
                    break;
            }
        }
        
        //Checks if current stage has finished
        static public void CheckBoard(GameTime gameTime)
        {
            //if no enemies left then update stage
            int enemyCount = GameBoard.VisibleEnemies.Count;
            if (enemyCount == 0 && GameBoard.EnemiesLeft == 0)
            {
                //New stage
                CurrentStage++;
                ConfigureNextStage();
            }
        }
    }
}
