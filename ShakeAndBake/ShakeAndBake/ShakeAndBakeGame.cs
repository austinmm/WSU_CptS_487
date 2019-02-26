using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShakeAndBake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ShakeAndBakeGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static Texture2D circle, player, playerBullet, enemyBullet;
        private View.GameBoard gameBoard;
        private Controller.GameController gameController;
        private Model.GameData gameData;
        //Constructor
        public ShakeAndBakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            GameConfig.Height = this.graphics.GraphicsDevice.Viewport.Height;
            GameConfig.Width = this.graphics.GraphicsDevice.Viewport.Width;
            this.Initialize();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            this.gameData = new Model.GameData(ShakeAndBakeGame.player);
            this.gameBoard = new View.GameBoard(this.gameData);
            this.gameController = new Controller.GameController(data: this.gameData, board: this.gameBoard);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);        
            circle = Content.Load<Texture2D>("circle");
            player = Content.Load<Texture2D>("player");
            playerBullet = Content.Load<Texture2D>("player_bullet");
            enemyBullet = Content.Load<Texture2D>("enemy_bullet");
        }
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            this.gameController.Update(gameTime);
            if (this.gameController.State == GameState.GAMEOVER)
            {
                this.GameOver();
            }
            // Check game after updating game board
            base.Update(gameTime);
        }

        public void GameOver()
        {
            this.gameBoard.GameOver();
            GraphicsDevice.Clear(Color.LimeGreen);
            this.Initialize();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            gameBoard.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
