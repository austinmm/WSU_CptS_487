using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.View;

namespace ShakeAndBake.Controller
{
    // The "Controller" of our Game's MVC Architecture.
    public class GameController : Updatable
    {
        private InputHandler inputHandler;

        private ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }
        
        private StageManager stageManager;

        private GameState gameState;
        public GameState State
        {
            get { return gameState; }
            set {
                gameState = value;
                switch (gameState)
                {
                    case GameState.PLAYING:
                        screenManager.SetScreen(ScreenType.INGAME);
                        break;
                    case GameState.GAMEOVER:
                        // todo determine if lose or win
                        screenManager.SetScreen(ScreenType.GAMEWIN);
                        break;
                }
            }
        }

        // Contains a reference to the current this.gameData instance.
        private Model.GameData gameData;

        // Contains a reference to the current GameBoard instance.
        private View.GameBoard gameBoard;

        //Constructor
        public GameController(Model.GameData gameData, View.GameBoard gameBoard)
        {
            this.gameData = gameData;
            this.gameBoard = gameBoard;

            screenManager = new ScreenManager(gameData);
            screenManager.SetScreen(ScreenType.INGAME);
            
            gameState = GameState.PLAYING;
            inputHandler = new InputHandler();
            stageManager = new StageManager();
            stageManager.ConfigureNextStage(gameData);
        }
        
        public void Update(GameTime gameTime)
        {
            // Gets the state of the keyboard and checks the combos as follows.
            KeyboardState state = Keyboard.GetState();
            inputHandler.UpdateGameSpeed(state);
            inputHandler.FireUserProjectile(state);
            if (inputHandler.DidUserMove(state, out float newX, out float newY))
            {
                Player.Instance.Move(newX, newY);
            }

            gameData.Update(gameTime);
            //gameBoard.Update(gameTime);
            
            // Update the game state based on what the stage manager changes to.
            State = stageManager.CheckBoard(gameData, gameState);
        }
    }
}
