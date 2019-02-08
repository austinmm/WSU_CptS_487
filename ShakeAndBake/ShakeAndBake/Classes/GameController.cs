using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

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
            currentStage = 1;
            ConfigureNextStage();
        }
        //Changes the GameBoard class to reflect the current stage
        static public void ConfigureNextStage() {

         }
         
        //Checks if current stage has finished
        static public void CheckBoard()
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
