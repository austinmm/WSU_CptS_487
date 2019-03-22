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
            Player.Instance.Draw(spriteBatch);

            // draw game menu/status here (to overlap everything)
        }
    }
}
