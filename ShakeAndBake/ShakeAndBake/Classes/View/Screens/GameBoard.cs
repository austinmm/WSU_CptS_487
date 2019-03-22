using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Controller;

namespace ShakeAndBake.View
{
    // This Class is the "View" of our Game's MVC Architecture. 
    public class GameBoard : Screen
    {
        // Contains a reference to the current GameData instance.
        private readonly Model.GameData gameData;

        // Constructor for GameBoard class.
        public GameBoard(Model.GameData data)
        {
            this.gameData = data;
        }
        
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("background"), new Vector2(0,0), Color.White);
            foreach (Model.GameEntity.Enemy enemy in gameData.VisibleEnemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (Model.GameEntity.Enemy enemy in gameData.DeadEnemies)
            {
                enemy.Draw(spriteBatch);
            }

            Player.Instance.Draw(spriteBatch);
            int xOffset = 60, yOffset = 5;
            for (int lives = 1; lives <= Player.Instance.Health; lives++)
            {
                if(lives % 4 == 0)
                {
                    yOffset += 60;
                    xOffset = 60;
                }
                Rectangle destinationRectangle = new Rectangle(x: GameConfig.Width-xOffset, y:yOffset, width:50, height:50);
                spriteBatch.Draw(ShakeAndBakeGame.GetTexture("trophy"), destinationRectangle, Color.White);
                xOffset += 60;
            }


            // draw game menu/status here (to overlap everything)
        }
    }
}
