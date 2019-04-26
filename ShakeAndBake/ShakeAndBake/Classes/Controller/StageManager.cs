using ShakeAndBake.Classes.Controller;
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
        
        public void Configure(Model.GameData gameData)
        {
            foreach (Wave wave in waves)
            {
                foreach (EnemyConfig config in wave.EnemyConfigs)
                {
                    gameData.AddEnemy(config);
                }
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

        private List<StageData> stages;

        //Contains the index of the current phase
        private int currentStage = 4;
        public int CurrentStage
        {
            get { return currentStage; }
            set
            {
                STAGE = this.currentStage = value;
           }
        }

        public StageManager()
        {
            //Load JSON Stage Data Here
            initStages();
        }

        private void initStages()
        {
            stages = new List<StageData>();
            StreamReader r = GameData.GetStagesConfigStreamReader();
            string json = r.ReadToEnd();
            Dictionary<string, WaveConfigs> stageDict = JsonConvert.DeserializeObject<Dictionary<string, WaveConfigs>>(json);

            foreach (string stageName in stageDict.Keys)
            {
                WaveConfigs waveConfigs = stageDict[stageName];
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
        }

        public bool IsCurrentStageCompleted(Model.GameData gameData)
        {
            if (gameData.AreAllEnemiesDestroyed)
            {
                return true;
            }
            return false;
        }

        public bool AreAllStagesCompleted()
        {
            if (this.currentStage >= this.stages.Count)
            {
                return true;
            }
            return false;
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
            }
            return currentState;
        }

        // Changes the GameBoard class to reflect the current stage.
        public void ConfigureNextStage(Model.GameData gameData)
        {
            gameData.Reset();       
            StageData data = stages[this.CurrentStage];
            data.Configure(gameData);
        }
    }
}