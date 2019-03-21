using System.Collections.Generic;

namespace ShakeAndBake.Controller
{
    public enum GameStage { Stage1, Stage2, Stage3, Stage4 }

    public class StageData
    {
        
    }

    // Manages loading stage information and switching stages.
    public class StageManager
    {
        //Contains a list of all different phases available
        private List<GameStage> phases;
        public List<GameStage> Phases
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

        public StageManager()
        {
            phases = new List<GameStage>();
            phases.Add(GameStage.Stage1);
            phases.Add(GameStage.Stage2);
            phases.Add(GameStage.Stage3);
            phases.Add(GameStage.Stage4);
            currentPhase = 0;
        }

        public GameState CheckBoard(Model.GameData gameData, GameState currentState)
        {
            // If no enemies are left in this phase, then switch to next phase.
            if (gameData.VisibleEnemies.Count == 0)
            {
                //New phase
                if (++currentPhase >= phases.Count)
                {
                    return GameState.GAMEOVER;
                } else {
                    ConfigureNextPhase(gameData);
                }
            } else if (gameData.IsUserDead)
            {
                return GameState.GAMEOVER;
            }
            return currentState;
        }

        //Changes the GameBoard class to reflect the current phase
        public void ConfigureNextPhase(Model.GameData gameData)
        {
            gameData.Reset();
            GameStage phase = phases[currentPhase];
            switch (phase)
            {
                case GameStage.Stage1:
                    for (int i = 0; i < 5; i++) {
                        gameData.AddEnemy(EnemyType.Easy);
                    }
                    for (int i = 0; i < 2; i++) {
                        gameData.AddEnemy(EnemyType.Medium);
                    }
                    break;
                case GameStage.Stage2:
                    for (int i = 0; i < 2; i++) {
                        gameData.AddEnemy(EnemyType.MidBoss);
                    }
                    break;
                case GameStage.Stage3:
                    for (int i = 0; i < 3; i++) {
                        gameData.AddEnemy(EnemyType.Medium);
                    }
                    for (int i = 0; i < 2; i++) {
                        gameData.AddEnemy(EnemyType.Hard);
                    }
                    break;
                case GameStage.Stage4:
                    gameData.AddEnemy(EnemyType.FinalBoss);
                    break;
            }
        }

        public void loadJson(GameStage config)
        {

        }
    }
}