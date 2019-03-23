﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Model.Factories.EnemyFactory;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Classes.Model.Factories.PathFactory;
using ShakeAndBake.Classes.Controller;
using System.IO;

namespace ShakeAndBake.Model
{
    //This Class is the "Model" of our Game's MVC Architecture 
    public class GameData
    {
        private CollisionBoard collisionBoard;

        public bool IsUserDead { get { return Player.Instance.IsDestroyed; } }
        //Contains a list of all enemies currently visible on the gameboard
        private ObservableCollection<Enemy> visibleEnemies;
        public ObservableCollection<Enemy> VisibleEnemies
        {
            get { return visibleEnemies; }
        }

        public bool AreAllEnemiesDestroyed
        {
            get { return visibleEnemies.Count == 0; }
        }

        //Contains a list of all enemies that have dies but still have projectiles on the gameboard
        private List<Enemy> deadEnemies;
        public List<Enemy> DeadEnemies
        {
            get { return deadEnemies; }
        }

        public static StreamReader GetStagesConfigStreamReader()
        {
            return new StreamReader("../../../../JSON/stages.json");
        }
        //Constructor for GameData class
        public GameData(Texture2D playerTexture)
        {
            this.collisionBoard = new CollisionBoard(
                GameConfig.Height,
                GameConfig.Width,
                playerTexture.Width
            );

            this.Initialize();
            Player.Instance.Position = new Vector2
            (
                (GameConfig.Width / 2 - playerTexture.Width / 2),
                (GameConfig.Height - playerTexture.Height)
            );
        }
        
        private void Initialize()
        {
            this.collisionBoard = new CollisionBoard(
                GameConfig.Height,
                GameConfig.Width,
                ShakeAndBakeGame.GetTexture("player").Width
            );
            //Enemies
            this.visibleEnemies = new ObservableCollection<Enemy>();
            //When enemy is added or removed from collection "updateEnimies" is automatically called
            this.visibleEnemies.CollectionChanged += UpdateEnemies;
            this.deadEnemies = new List<Enemy>();
            //User
        }

        public void AddEnemy(EnemyConfig config)
        {
            Random rand = new Random(this.visibleEnemies.Count);
            
            Enemy enemy = EnemeyFactoryProducer.CreateEnemy(config);

            this.visibleEnemies.Add(enemy);
        }

        //When an Enemy is added or removed from the ObservableCollection "visibleEnemies"
        public void UpdateEnemies(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Enemies that have been added
            if (e.NewItems != null)
            {
                //When an Enemy is created
            }
            //Enemies that have been removed
            if (e.OldItems != null)
            {
                //Each enemy is added to our deadEnemy list
                foreach (Enemy deadEnemy in e.OldItems)
                {
                    this.deadEnemies.Add(deadEnemy);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            Player.Instance.Update(gameTime, collisionBoard);
            //Call update on all our visible enemies, this will automatically update their projectiles as well
            for (int j = this.visibleEnemies.Count - 1; j >= 0; --j)
            {
                Enemy enemy = this.visibleEnemies[j];
                if (enemy.IsDestroyed)
                {
                    this.visibleEnemies.RemoveAt(j); // This adds an enemy to deadEnemies? Why would we implment it like this??? Very confusing
                    //this.deadEnemies.Add(enemy);
                    this.collisionBoard.RemoveFromBucketIfExists(enemy);
                    continue;
                }
                enemy.Update(gameTime, this.collisionBoard);
            }

            //Check if player bullets destroyed
            for (int i = Player.Instance.Projectiles.Count - 1; i >=0; --i)
            {
                Projectile p = Player.Instance.Projectiles[i];
                if (p.IsDestroyed)
                {
                    Player.Instance.Projectiles.RemoveAt(i);
                    collisionBoard.RemoveFromBucketIfExists(p);
                    continue;
                }
            }

            //check deadEnimies list to see if they have any projectiles left on the board
            for (int i = this.deadEnemies.Count - 1; i >= 0; --i)
            {
                Enemy enemy = deadEnemies[i];
                enemy.Update(gameTime, collisionBoard);
                if (enemy.Projectiles.Count == 0)
                {
                    this.deadEnemies.RemoveAt(i);
                }
            }
        }
        
        // called before configuring a phase
        public void Reset()
        {
            Player.Instance.Projectiles.Clear();
            this.ResetPlayerPosition();
            this.Initialize();
        }

        private void ResetPlayerPosition()
        {
            Player.Instance.Position = new Vector2
            (
                (GameConfig.Width / 2 - Player.Instance.Sprite.Width / 2),
                (GameConfig.Height - Player.Instance.Sprite.Height)
            );
        }
    }
}
