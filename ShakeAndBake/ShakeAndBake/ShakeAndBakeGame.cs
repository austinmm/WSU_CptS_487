using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ShakeAndBake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ShakeAndBakeGame : Game
    {
        public static ShakeAndBakeGame Instance;

        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, SoundEffect> songs = new Dictionary<string, SoundEffect>();//because they are wav files it only recognizes them as soundeffects
        private static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;


        private View.GameBoard gameBoard;
        private Controller.GameController gameController;
        private Model.GameData gameData;

        public ShakeAndBakeGame()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameConfig.Height = graphics.GraphicsDevice.Viewport.Height;
            GameConfig.Width = graphics.GraphicsDevice.Viewport.Width;
            Initialize();
            
            //starts music
            // var instance = songs["music"].CreateInstance();
            // instance.IsLooped = true;
            // instance.Play();

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
            LoadAndStoreTexture("player_default_bullet"); LoadAndStoreTexture("player_default_bullet");

            //Enemy Images
            LoadAndStoreTexture("enemy_default");
            LoadAndStoreTexture("enemy_default_bullet");
            LoadAndStoreTexture("player_special_bullet");
            //Generic Bullet Images
            LoadAndStoreTexture("small_ball_bullet");
            LoadAndStoreTexture("small_square_bullet");
            //Menu,Lose and Win Screen Images
            LoadAndStoreTexture("titleScreen");
            LoadAndStoreTexture("background");
            LoadAndStoreTexture("startIcon");
            LoadAndStoreTexture("selectionIcon");
            LoadAndStoreTexture("exitIcon");
            LoadAndStoreTexture("mainMenuIcon");
            LoadAndStoreTexture("loseScreen");
            LoadAndStoreTexture("winScreen");
            LoadAndStoreTexture("settingsIcon");
            LoadAndStoreTexture("final");
            //Game Play Screen Images
            LoadAndStoreTexture("lives_left");
            //Fonts
            LoadAndStoreFont("Default");
            LoadAndStoreFont("Small");
            LoadAndStoreFont("File");
            //LoadAndStoreTexture("lifeIcon");

            //sounds
            LoadAndStoreSoundEffect("final_dead");
            LoadAndStoreSoundEffect("shakeandbake");
            LoadAndStoreSoundEffect("shot");
            LoadAndStoreSoundEffect("enemy_shot");
            
            songs["music"] = Content.Load<SoundEffect>("song");
        }

        private void LoadAndStoreSoundEffect(string name) {
            soundEffects[name] = Content.Load<SoundEffect>(name);
        }

        private void LoadAndStoreTexture(string name) {
            textures[name] = Content.Load<Texture2D>(name);
        }

        public static void PlaySoundEffect(string name)
        {
            SoundEffect effect = soundEffects[name];
            if (effect != null) {
                effect.CreateInstance().Play();
            }
        }
        
        public static Texture2D GetTexture(string name)
        {
            return textures[name];
        }

        public static SpriteFont GetFont(string name)
        {
            return fonts[name];
        }
        public static SoundEffect GetSoundEffect(string name)
        {
            return soundEffects[name];
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
