﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Controller;

namespace ShakeAndBake.View
{
    // This Class is the "View" of our Game's MVC Architecture. 
    public class GameBoard : Screen
    {
        // Contains a reference to the current GameData instance.
        private readonly Model.GameData gameData;

        // Constructor for GameBoard class.
        public GameBoard(Model.GameData data)
        {
            this.gameData = data;
        }
        
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ShakeAndBakeGame.GetTexture("background"), new Vector2(0,0), Color.White);
            foreach (Model.GameEntity.Enemy enemy in gameData.VisibleEnemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (Model.GameEntity.Enemy enemy in gameData.DeadEnemies)
            {
                enemy.Draw(spriteBatch);
            }
            this.gameData.PlayerPowerUp.Draw(spriteBatch);
            Player.Instance.Draw(spriteBatch);
            this.DrawPlayerHealth(graphics, spriteBatch);
            this.DrawCurrentStageGraphic(graphics, spriteBatch);
            this.DrawScoreGraphic(graphics, spriteBatch);
            this.DrawPlayerSpecialProjectileCountdown(graphics, spriteBatch);
            
            if (Player.Instance.CheatMode) {
                spriteBatch.DrawString(ShakeAndBakeGame.GetFont("Small"), "Cheating Mode", new Vector2(10, GameConfig.Height - 40), Color.Lime);
            }
        }

        public void DrawCurrentStageGraphic(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ShakeAndBakeGame.GetFont("Small"), $"Stage {StageManager.STAGE + 1}", new Vector2(10, 10), Color.Gold);
        }

        public void DrawScoreGraphic(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ShakeAndBakeGame.GetFont("Small"), $"Score: {Player.Instance.Score}", new Vector2(10, 60), Color.Gold);
        }
        
        public void DrawPlayerHealth(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            int xOffset = 60, yOffset = 5;
            for (int lives = 1; lives <= Player.Instance.Health; lives++)
            {
                if (lives % 4 == 0) //Displays three lives per row
                {
                    yOffset += 60;
                    xOffset = 60;
                }
                Rectangle destinationRectangle = new Rectangle(x: GameConfig.Width - xOffset, y: yOffset, width: 50, height: 50);
                spriteBatch.Draw(ShakeAndBakeGame.GetTexture("lives_left"), destinationRectangle, Color.White);
                xOffset += 60;
            }
        }

        public void DrawPlayerSpecialProjectileCountdown(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            string count = Player.Instance.SpecialProjectileCountdown == 0 ? "Ready" : $"{Player.Instance.SpecialProjectileCountdown}";
            spriteBatch.DrawString(ShakeAndBakeGame.GetFont("Small"), $"SW: {count}", new Vector2(10, 110), Color.OrangeRed);
        }
    }
}
