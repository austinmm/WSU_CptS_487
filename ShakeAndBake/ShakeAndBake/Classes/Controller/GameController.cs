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
                    case GameState.MENU:
                        screenManager.SetScreen(ScreenType.START);
                        break;
                    case GameState.PLAYING:
                        screenManager.SetScreen(ScreenType.INGAME);
                        break;
                    case GameState.GAMEOVER:
                        if (Player.Instance.IsDestroyed)
                        {
                            screenManager.SetScreen(ScreenType.GAMELOSE);
                        }
                        else
                        {
                            screenManager.SetScreen(ScreenType.GAMEWIN);
                        }
                        break;
                }
            }
        }

        private MenuState menuState;
        public MenuState MenuState
        {
            get { return menuState; }
            set { menuState = value; }
        }

        private EndMenuState endMenuState;
        public EndMenuState EndMenuState
        {
            get { return endMenuState; }
            set { endMenuState = value; }
        }

        // Contains a reference to the current this.gameData instance.
        private Model.GameData gameData;

        // Contains a reference to the current GameBoard instance.
        private View.GameBoard gameBoard;

        private KeyboardState previousKeyboardState;

        //Constructor
        public GameController(Model.GameData gameData, View.GameBoard gameBoard)
        {
            this.gameData = gameData;
            this.gameBoard = gameBoard;
            this.previousKeyboardState = Keyboard.GetState();//initialize with blank state

            screenManager = new ScreenManager(gameData,this);
            screenManager.SetScreen(ScreenType.START);
            
            gameState = GameState.MENU;
            menuState = MenuState.START;
            inputHandler = new InputHandler();
            stageManager = new StageManager();
            stageManager.ConfigureNextStage(gameData);
        }

        public void Update(GameTime gameTime)
        {
            // Gets the state of the keyboard and checks the combos as follows.
            KeyboardState keyboardState = Keyboard.GetState();
            switch (gameState) {
                case GameState.PLAYING:
                    inputHandler.UpdateGameSpeed(keyboardState);
                    inputHandler.FireUserProjectile(keyboardState);
                    if (inputHandler.DidUserMove(keyboardState, out float newX, out float newY))
                    {
                        Player.Instance.Move(newX, newY);
                    }

                    gameData.Update(gameTime);
                    if (Player.Instance.IsDestroyed)
                    {
                        State = GameState.GAMEOVER;
                    }
                    else
                    {
                        // Update the game state based on what the stage manager changes to.
                        State = stageManager.CheckBoard(gameData, gameState);
                    }
                    break;
                case GameState.MENU:
                    GameState newGameState;
                    menuState = inputHandler.MenuMove(keyboardState, previousKeyboardState, menuState, out newGameState);
                    State = newGameState;
                    break;
                case GameState.GAMEOVER:
                    endMenuState = inputHandler.EndMenuMove(keyboardState, endMenuState, out newGameState);
                    State = newGameState;
                    break;
                case GameState.RESET:
                    stageManager.CurrentStage = 0;
                    stageManager.ConfigureNextStage(gameData);
                    State = GameState.MENU;
                    Player.Instance.Health = 5;
                    break;
                case GameState.EXIT:
                    System.Environment.Exit(0);
                    break;
            }
            previousKeyboardState = keyboardState;
        }
    }
}
