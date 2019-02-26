using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShakeAndBake.View
{
    //This Class is the "View" of our Game's MVC Architecture 
    public class GameBoard
    {
        //Contains a reference to the current GameData instance
        private Model.GameData gameData;

        //Constructor for GameBoard class
        public GameBoard(Model.GameData data)
        {
            this.gameData = data;
        }

        //MultiThread this Method call
        public void Update(GameTime gameTime)
        {
            //update player health info, this.gameData.User.Health
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Model.GameEntity.Enemy enemy in this.gameData.VisibleEnemies)
            {
                enemy.Draw(spriteBatch);
            }
            // draw player last
            this.gameData.User.Draw(spriteBatch);
        }

        public void NewPase()
        {
            // New Phase
            //Display new phase name for 5 seconds and then remove graphic and continue game play
        }

        // called before configuring a phase
        public void GameMenu()
        {
            // Game Menu
            //Display game config options
            // set GameConfig.GameSpeed = double value;
        }
        
        public void GameOver() {
            // game over Screen
            //Display outcome of game, i.e. win or loose
            //Redirect to gameMenu
        }
    }
}
