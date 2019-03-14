using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace ShakeAndBake.Controller
{
    public class StageManager
    {
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

        public StageManager()
        {

        }

        public void ConfigureNextPhase(Model.GameData gameData)
        {
            
        }

        public void loadJson(GameBoardConfigs config)
        {

        }
    }
}