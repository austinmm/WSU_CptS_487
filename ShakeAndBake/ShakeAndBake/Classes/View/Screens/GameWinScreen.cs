using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakeAndBake.View
{
    public class GameWinScreen : Screen
    {
        private readonly Controller.GameController gameController;

        // Constructor for GameBoard class.
        public GameWinScreen(Controller.GameController data)
        {
            this.gameController = data;
        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("background"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("winScreen"), new Vector2(0, 0), Color.White);
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("mainMenuIcon"), new Vector2(50, 375), Color.White);
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("exitIcon"), new Vector2(50, 450), Color.White);
            switch (gameController.EndMenuState)
            {
                case EndMenuState.MAIN:
                    spriteBatch.Draw(ShakeAndBakeGame.GetTexture("selectionIcon"), new Vector2(50 + ShakeAndBakeGame.GetTexture("mainMenuIcon").Width, 365), Color.White);
                    break;
                case EndMenuState.EXIT:
                    spriteBatch.Draw(ShakeAndBakeGame.GetTexture("selectionIcon"), new Vector2(150, 440), Color.White);
                    break;
            }
        }
    }
}
