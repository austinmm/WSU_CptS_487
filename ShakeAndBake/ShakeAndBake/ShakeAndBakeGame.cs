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
        public static ShakeAndBakeGame INSTANCE;

        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private View.GameBoard gameBoard;
        private Controller.GameController gameController;
        private Model.GameData gameData;

        public ShakeAndBakeGame()
        {
            INSTANCE = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameConfig.Height = graphics.GraphicsDevice.Viewport.Height;
            GameConfig.Width = graphics.GraphicsDevice.Viewport.Width;
            Initialize();

            //Initializes the MVC layers
            gameData = new Model.GameData(GetTexture("player_default"));
            gameBoard = new View.GameBoard(gameData);
            gameController = new Controller.GameController(gameData, gameBoard);
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
            //Player images
            LoadAndStoreTexture("player_default");
            LoadAndStoreTexture("player_default_bullet");
            //Enemy Images
            LoadAndStoreTexture("enemy_default");
            LoadAndStoreTexture("enemy_default_bullet");
            //Generic Bullet Images
            LoadAndStoreTexture("small_ball_bullet");
            LoadAndStoreTexture("small_square_bullet");
            //Menu,Loose and Win Screen Images
            LoadAndStoreTexture("titleScreen");
            LoadAndStoreTexture("background");
            LoadAndStoreTexture("startIcon");
            LoadAndStoreTexture("selectionIcon");
            LoadAndStoreTexture("exitIcon");
            LoadAndStoreTexture("mainMenuIcon");
            LoadAndStoreTexture("loseScreen");
            LoadAndStoreTexture("winScreen");
            //Game Play Screen Images
            LoadAndStoreTexture("lives_left");
            //Fonts
            LoadAndStoreFont("Default");
            LoadAndStoreFont("File");
            //LoadAndStoreTexture("lifeIcon");
        }
        
        private void LoadAndStoreTexture(string name) {
            textures[name] = Content.Load<Texture2D>(name);
        }
        
        public static Texture2D GetTexture(string name)
        {
            return textures[name];
        }

        public static SpriteFont GetFont(string name)
        {
            return fonts[name];
        }

        private void LoadAndStoreFont(string name)
        {
            fonts[name] = Content.Load<SpriteFont>(name);
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
            gameController.ScreenManager.Draw(graphics, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
