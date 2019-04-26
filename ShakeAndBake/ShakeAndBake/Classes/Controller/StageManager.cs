using ShakeAndBake.Controller;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using ShakeAndBake.Model;

namespace ShakeAndBake.Controller
{
    public enum GameStage { Stage1, Stage2, Stage3, Stage4 }

    public class Wave
    {
        private EnemyType enemyType;
        public EnemyType EnemyType
        {
            get { return enemyType; }
        }

        private int enemyAmount;
        public int EnemyAmount
        {
            get { return enemyAmount; }
        }

        private List<EnemyConfig> enemyConfigs;
        public List<EnemyConfig> EnemyConfigs
        {
            get { return this.enemyConfigs;  }
        }

        public Wave(List<EnemyConfig> enemyConfigs)
        {
            this.enemyConfigs = enemyConfigs;
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
        
        public void Configure(Model.GameData gameData, int waveIndex)
        {
            Wave wave = waves[waveIndex];
            foreach (EnemyConfig config in wave.EnemyConfigs)
            {
                gameData.AddEnemy(config);
            }
        }
    }

    // Manages loading stage information and switching stages.
    public class StageManager
    {
        public static int STAGE;

        //Contains a list of all different phases available
        private List<GameStage> stageTypes;
        public List<GameStage> StageTypes
        {
            get { return stageTypes; }
            set { stageTypes = value; }
        }

        private DifficultyLevel currentLevel;
        public DifficultyLevel CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        private Dictionary<DifficultyLevel, List<StageData>> difficultyLevels;

        //Contains the index of the current phase
        private int currentStage;
        public int CurrentStage
        {
            get { return currentStage; }
            set
            {
                STAGE = this.currentStage = value;
           }
        }

        //Index of the current wave in the current stage
        private int currentWave;
        
        public StageManager()
        {
            //Load JSON Stage Data Here
            currentLevel = DifficultyLevel.Easy;
            initStages();
        }

        private void initStages()
        {
            difficultyLevels = new Dictionary<DifficultyLevel, List<StageData>>();

            StreamReader r = GameData.GetStagesConfigStreamReader();
            string json = r.ReadToEnd();
            Dictionary<string, Dictionary<string, WaveConfigs>> lvls = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, WaveConfigs>>>(json);
            foreach (string levelName in lvls.Keys)
            {
                Dictionary<string, WaveConfigs> inStages = lvls[levelName];
                List<StageData> stages = new List<StageData>();
                foreach (string stageName in inStages.Keys)
                {
                    WaveConfigs waveConfigs = inStages[stageName];
                    List<Wave> waves = new List<Wave>();
                    for (int i = 0; i < waveConfigs.waves.Count; ++i)
                    {
                        List<EnemyConfig> waveConfig = waveConfigs.waves[i];
                        List<EnemyConfig> wave = new List<EnemyConfig>();
                        for (int j = 0; j < waveConfig.Count; ++j)
                        {
                            EnemyConfig enemyConfig = waveConfig[j];
                            double xOffset = 0;
                            double yOffset = 0;
                            for (int k = 0; k < enemyConfig.count; ++k)
                            {
                                /* Hard coded formations */
                                // TODO: This should be handled by some other class!
                                EnemyConfig copyConfig = new EnemyConfig(enemyConfig);
                                if (copyConfig.formation != null && enemyConfig.formation.Equals("leftDiagonalLine"))
                                {
                                    xOffset = xOffset - 75; 
                                    yOffset = yOffset - 75;
                                }
                                copyConfig.startPosition.X += xOffset;
                                copyConfig.startPosition.Y += yOffset;
                                wave.Add(copyConfig);
                            }
                        }
                        waves.Add(new Wave(wave));
                    }
                    stages.Add(new StageData(waves));
                }
                DifficultyLevel.TryParse(levelName, out DifficultyLevel lvl);
                difficultyLevels[lvl] = stages;
            }
        }

        public bool isCurrentStageWaveCompleted(Model.GameData gameData)
        {
            return gameData.AreAllEnemiesDestroyed;
        }

        public bool IsCurrentStageCompleted(Model.GameData gameData)
        {
            StageData stage = difficultyLevels[currentLevel][currentStage];
            return isCurrentStageWaveCompleted(gameData) && currentWave == stage.Waves.Count - 1;
        }

        public bool AreAllStagesCompleted()
        {
            List<StageData> stages = difficultyLevels[currentLevel];
            return currentStage >= stages.Count;
        }

        public GameState CheckStageStatus(Model.GameData gameData, GameState currentState)
        {
            // If no enemies are left in this phase, then switch to next phase.
            if (this.IsCurrentStageCompleted(gameData))
            {
                //Incrament to next stage;
                this.CurrentStage = this.CurrentStage + 1;
                // Next stage
                if (this.AreAllStagesCompleted()) {
                    return GameState.GAMEOVER;
                } else {
                    this.ConfigureNextStage(gameData);
                    return GameState.PAUSE;
                }
            } else {
                if (isCurrentStageWaveCompleted(gameData)) {
                    currentWave++;
                    StageData stage = difficultyLevels[currentLevel][currentStage];
                    if (currentWave < stage.Waves.Count) {
                        ConfigureWave(gameData, stage);
                    }
                }
            }
            return currentState;
        }
        
        public void ConfigureWave(Model.GameData gameData, StageData stage)
        {
            stage.Configure(gameData, currentWave);
        }

        // Changes the GameBoard class to reflect the current stage.
        public void ConfigureNextStage(Model.GameData gameData)
        {
            gameData.Reset();
            List<StageData> stages = difficultyLevels[currentLevel];       
            StageData stage = stages[this.CurrentStage];
            currentWave = 0;
            ConfigureWave(gameData, stage);
            ShakeAndBakeGame.PlaySoundEffect("shakeandbake");
        }
    }
}
