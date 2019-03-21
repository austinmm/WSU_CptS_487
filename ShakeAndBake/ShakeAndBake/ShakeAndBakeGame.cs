using System;
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
        public static Controller.AssetManager AssetManager;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private View.GameBoard gameBoard;
        private Controller.GameController gameController;
        private Model.GameData gameData;

        public ShakeAndBakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            GameConfig.Height = graphics.GraphicsDevice.Viewport.Height;
            GameConfig.Width = graphics.GraphicsDevice.Viewport.Width;
            Initialize();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            AssetManager = new Controller.AssetManager(Content);
            gameData = new Model.GameData(AssetManager.GetTexture("player"));
            gameBoard = new View.GameBoard(gameData);
            gameController = new Controller.GameController(gameData, gameBoard);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            gameController.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            
            // does this violate MVC?
            gameController.ScreenManager.Draw(graphics, spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
