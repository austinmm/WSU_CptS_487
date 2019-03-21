using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShakeAndBake.View
{
    public enum ScreenType
    {
        START, INGAME, GAMEWIN, GAMELOSE
    }
    
    public interface Screen
    {
        void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch);
    }
    
    public class ScreenManager
    {
        private ScreenType current;
        private Dictionary<ScreenType, Screen> screens;

        public ScreenManager(Model.GameData gameData)
        {
            screens = new Dictionary<ScreenType, Screen>();
            AddScreen(ScreenType.START, new StartScreen());
            AddScreen(ScreenType.INGAME, new GameBoard(gameData));
            AddScreen(ScreenType.GAMEWIN, new GameWinScreen());
            AddScreen(ScreenType.GAMELOSE, new GameLoseScreen());
        }
        
        public void AddScreen(ScreenType type, Screen screen)
        {
            screens[type] = screen;
        }

        public void SetScreen(ScreenType type)
        {
            current = type;
        }
        
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Screen screen = screens[current];
            if (screen != null)
            {
                screen.Draw(graphics, spriteBatch);
            }
        }
    }
}