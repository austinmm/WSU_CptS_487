using System;
using System.Collections.Generic;
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
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

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
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            LoadAndStoreTexture("player");
            LoadAndStoreTexture("player_bullet");
            LoadAndStoreTexture("enemy_bullet");

            LoadAndStoreTexture("circle");
            LoadAndStoreTexture("final_boss");
            LoadAndStoreTexture("mid_boss");
            LoadAndStoreTexture("easy_enemy");
            LoadAndStoreTexture("medium_enemy");
            LoadAndStoreTexture("hard_enemy");


            gameData = new Model.GameData(GetTexture("player"));
            gameBoard = new View.GameBoard(gameData);
            gameController = new Controller.GameController(gameData, gameBoard);
        }
        
        private void LoadAndStoreTexture(string name) {
            textures[name] = Content.Load<Texture2D>(name);
        }
        
        public static Texture2D GetTexture(string name)
        {
            return textures[name];
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
