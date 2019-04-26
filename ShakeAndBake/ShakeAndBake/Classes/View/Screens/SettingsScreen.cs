using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakeAndBake.View
{
    public class SettingsScreen : Screen
    {
        private readonly Controller.GameController gameController;

        // Constructor for GameBoard class.
        public SettingsScreen(Controller.GameController data)
        {
            this.gameController = data;
        }
        
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("background"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("titleScreen"), new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(ShakeAndBakeGame.GetFont("Small"), "Arrows", new Vector2(50, 350), Color.Gold);
            spriteBatch.DrawString(ShakeAndBakeGame.GetFont("Small"), "WASD", new Vector2(50, 425), Color.Gold);

            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("exitIcon"), new Vector2(50, 525), Color.White);
            
            float selectionY = 0;
            switch (GameConfig.MoveKeys)
            {
                case MoveKeys.ARROW:
                    selectionY = 350;
                    break;
                case MoveKeys.WASD:
                    selectionY = 425;
                    break;
            }
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("selectionIcon"), new Vector2(200, selectionY), Color.White);
        }
    }
}
