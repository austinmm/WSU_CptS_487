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

        //This property controlls which screen to load 
        private ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }
        
        private StageManager stageManager;

        private double timePaused;
        private GameTime timeElapsed;

        private GameState currentGameState;
        private GameState CurrentGameState
        {
            get { return this.currentGameState; }
            set {
                if (this.currentGameState != value)
                {
                    this.currentGameState = value;
                    switch (this.currentGameState)
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
                        case GameState.PAUSE:
                            this.timePaused = this.timeElapsed.TotalGameTime.TotalSeconds;
                            break;
                        default: break;
                    }
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
            
            this.CurrentGameState = GameState.MENU;
            this.menuState = MenuState.START;
            this.inputHandler = new InputHandler();
            this.stageManager = new StageManager();
            this.stageManager.ConfigureNextStage(gameData);
        }

        public void Update(GameTime gameTime)
        {
            this.timeElapsed = gameTime;
            // Gets the state of the keyboard and checks the combos as follows.
            KeyboardState keyboardState = Keyboard.GetState();
            switch (this.CurrentGameState) {
                case GameState.PAUSE:
                    if(timePaused + 3 < timeElapsed.TotalGameTime.TotalSeconds)
                    {
                        this.CurrentGameState = GameState.PLAYING;
                    }
                    break;
                case GameState.PLAYING:
                    this.inputHandler.HandleGamePlayInput(keyboardState);
                    this.gameData.Update(gameTime);
                    if (Player.Instance.IsDestroyed)
                    {
                        this.CurrentGameState = GameState.GAMEOVER;
                    }
                    else
                    {
                        // Update the game state based on what the stage manager changes to.
                        this.CurrentGameState = stageManager.CheckStageStatus(this.gameData, this.CurrentGameState);
                    }
                    break;
                case GameState.MENU:
                    GameState newGameState;
                    this.menuState = inputHandler.MenuMove(keyboardState, previousKeyboardState, menuState, out newGameState);
                    this.CurrentGameState = newGameState;
                    break;
                case GameState.GAMEOVER:
                    this.endMenuState = inputHandler.EndMenuMove(keyboardState, endMenuState, out newGameState);
                    this.CurrentGameState = newGameState;
                    break;
                case GameState.RESET:
                    this.stageManager.CurrentStage = 0;
                    Player.Reset();
                    this.stageManager.ConfigureNextStage(gameData);
                    this.CurrentGameState = GameState.MENU;                
                    break;
                case GameState.EXIT:
                    System.Environment.Exit(0);
                    break;
            }
            previousKeyboardState = keyboardState;
        }
    }
}
