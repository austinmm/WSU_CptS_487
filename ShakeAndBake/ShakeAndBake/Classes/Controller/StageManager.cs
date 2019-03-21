using System.Collections.Generic;

namespace ShakeAndBake.Controller
{
    public enum GameStage { Stage1, Stage2, Stage3, Stage4 }

    public class Wave
    {
        private EnemyType enemyType;
        private int enemyAmount;

        public EnemyType EnemyType
        {
            get { return enemyType; }
        }

        public int EnemyAmount
        {
            get { return enemyAmount; }
        }

        public Wave(EnemyType enemyType, int enemyAmount)
        {
            this.enemyType = enemyType;
            this.enemyAmount = enemyAmount;
        }
    }

    public class StageData
    {
        private List<Wave> waves;
        public List<Wave> Waves
        {
            get { return waves; }
        }

        public StageData(List<Wave> waves)
        {
            this.waves = waves;
        }
        
        public void Configure(Model.GameData gameData)
        {
            foreach (Wave wave in waves)
            {
                for (int i = 0; i < wave.EnemyAmount; ++i)
                {
                    gameData.AddEnemy(wave.EnemyType);
                }
            }
        }
    }

    // Manages loading stage information and switching stages.
    public class StageManager
    {
        //Contains a list of all different phases available
        private List<GameStage> stageTypes;
        public List<GameStage> StageTypes
        {
            get { return stageTypes; }
            set { stageTypes = value; }
        }

        private Dictionary<GameStage, StageData> stages;

        //Contains the index of the current phase
        private int current;
        public int CurrentStage
        {
            get { return current; }
            set { current = value; }
        }

        public StageManager()
        {
            stageTypes = new List<GameStage>() {
                GameStage.Stage1, GameStage.Stage2, GameStage.Stage3, GameStage.Stage4 
            };
            stages = new Dictionary<GameStage, StageData>();
            initStages();      
            current = 0;
        }

        private void initStages()
        {
            stages[GameStage.Stage1] = new StageData(new List<Wave>(){
                new Wave(EnemyType.Easy, 5),
                new Wave(EnemyType.Medium, 2)
            });
            stages[GameStage.Stage2] = new StageData(new List<Wave>(){
                new Wave(EnemyType.MidBoss, 2),
            });
            stages[GameStage.Stage3] = new StageData(new List<Wave>(){
                new Wave(EnemyType.Medium, 3),
                new Wave(EnemyType.Hard, 2),
            });
            stages[GameStage.Stage4] = new StageData(new List<Wave>(){
                new Wave(EnemyType.FinalBoss, 1),
            });  
        }
        
        public GameState CheckBoard(Model.GameData gameData, GameState currentState)
        {
            // If no enemies are left in this phase, then switch to next phase.
            if (gameData.VisibleEnemies.Count == 0)
            {
                // Next stage.
                if (++current >= stageTypes.Count)
                {
                    return GameState.GAMEOVER;
                } else {
                    ConfigureNextStage(gameData);
                }
            } else if (gameData.IsUserDead)
            {
                return GameState.GAMEOVER;
            }
            return currentState;
        }

        // Changes the GameBoard class to reflect the current stage.
        public void ConfigureNextStage(Model.GameData gameData)
        {
            gameData.Reset();       
            GameStage stage = stageTypes[current];     
            StageData data = stages[stage];
            data.Configure(gameData);
        }

        public void loadJson(GameStage config)
        {

        }
    }
}