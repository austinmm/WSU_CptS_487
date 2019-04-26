
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakeAndBake.View
{
    public class StartScreen : Screen
    {
        private readonly Controller.GameController gameController;

        // Constructor for GameBoard class.
        public StartScreen(Controller.GameController data)
        {
            this.gameController = data;
        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("background"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("titleScreen"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("startIcon"), new Vector2(50, 375), Color.White);
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("startIcon"), new Vector2(50, 450), Color.White); // todo settingsIcon
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("exitIcon"), new Vector2(50, 525), Color.White);
            float selectionY = 0;
            switch (gameController.MenuState)
            {
                case MenuState.START:
                    selectionY = 365;
                    break;
                case MenuState.SETTINGS:
                    selectionY = 440;
                    break;
                case MenuState.EXIT:
                    selectionY = 515;
                    break;
            }
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("selectionIcon"), new Vector2(150, selectionY), Color.White);
        }
    }
}
