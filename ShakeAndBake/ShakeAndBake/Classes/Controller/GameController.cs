using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Controller
{
    // The "Controller" of our Game's MVC Architecture.
    public class GameController
    {
        private AssetManager assetManager;
        private StageManager stageManager;

        private GameState gameState;
        public GameState State
        {
            get { return gameState; }
            set { gameState = value; }
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
            gameState = GameState.PLAYING;
            assetManager = new AssetManager();
            stageManager = new StageManager();
            stageManager.ConfigureNextPhase(gameData);
        }
        
        public void Update(GameTime gameTime)
        {
            // Gets the state of the keyboard and checks the combos as follows.
            KeyboardState state = Keyboard.GetState();
            UpdateGameSpeed(state);
            FireUserProjectile(state);
            if (DidUserMove(state, out float newX, out float newY))
            {
                Player.Instance.Move(newX, newY);
            }
            gameData.Update(gameTime);
            gameBoard.Update(gameTime);
            gameState = stageManager.CheckBoard(gameData, gameState);
        }

        private void UpdateGameSpeed(KeyboardState state)
        {
            //utilizes the left control as the switch between speed modes essentially halving the speed when pressed.
            if (state.IsKeyDown(Keys.S))
            {
                GameConfig.GameSpeed = 2;
            } else {
                GameConfig.GameSpeed = 1;
            }
        }

        private void FireUserProjectile(KeyboardState state)
        {
            //Spacebar fires user projectile
            if (state.IsKeyDown(Keys.Space))
            {
                Player.Instance.FireProjectile();
            }
        }
    
        public bool DidUserMove(KeyboardState state, out float newX, out float newY)
        {
            float originalX = Player.Instance.Position.X;
            float originalY = Player.Instance.Position.Y;
            newX = originalX;
            newY = originalY;
            //variables to hold calculations preventing redundant calculations
            float movementSpeed = (float)(Player.Instance.Velocity * Player.Instance.Acceleration);
            //down right
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Up)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                newX = originalX + movementSpeed;
                newY = originalY + movementSpeed;
            }
            //up right
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Down)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                newX = originalX + movementSpeed;
                newY = originalY - movementSpeed;
            }
            //right
            if (state.IsKeyDown(Keys.Right) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Left)))
            {
                newX = originalX + movementSpeed;
            }
            //left down
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Right)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                newX = originalX - movementSpeed;
                newY = originalY + movementSpeed;
            }
            //left up
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Right)))
            {
                //looking into better computation for diagonal cause this moves farther than it should
                newX = originalX - movementSpeed;
                newY = originalY - movementSpeed;
            }
            //left
            if (state.IsKeyDown(Keys.Left) && !(state.IsKeyDown(Keys.Up)) && !(state.IsKeyDown(Keys.Down)) && !(state.IsKeyDown(Keys.Right)))
            {
                newX = originalX - movementSpeed;
            }
            //up
            if (state.IsKeyDown(Keys.Up) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Right)) && !(state.IsKeyDown(Keys.Down)))
            {
                newY = originalY - movementSpeed;
            }
            //down
            if (state.IsKeyDown(Keys.Down) && !(state.IsKeyDown(Keys.Left)) && !(state.IsKeyDown(Keys.Right)) && !(state.IsKeyDown(Keys.Up)))
            {
                newY = originalY + movementSpeed;
            }
            //returns true if the player was moved, false otherwise
            return (newX != originalX || newY != originalY) ? true : false;
        }
    }
}
