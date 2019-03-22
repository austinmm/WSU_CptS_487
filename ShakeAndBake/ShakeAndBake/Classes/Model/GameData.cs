using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ShakeAndBake.Model.GameEntity;
using ShakeAndBake.Model.Factories.EnemyFactory;
using ShakeAndBake.Extras.Paths;
using ShakeAndBake.Classes.Model.Factories.PathFactory;

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

        public void AddEnemy(EnemyType type)
        {
            Random rand = new Random(this.visibleEnemies.Count);
            
            Enemy enemy = EnemeyFactoryProducer.CreateEnemy(type);
            // spawn enemy on top of screen, randomly between the width of the screen
            enemy.Position = new Vector2(rand.Next(enemy.Sprite.Width, GameConfig.Width - enemy.Sprite.Width), -enemy.Sprite.Height);
            double velocity = Util.randDouble(1, 3);
            enemy.Velocity = velocity;


            /***
             * TODO: This should be handled by enemy factory
             ***/
            if (enemy is MidBoss || enemy is FinalBoss)
            {
                PathAbstractFactory factory = new WavePathFactory();
                Path path = factory.Create(enemy.Position, new Vector2(0, 1), (float)velocity);
                enemy.Path = path;
            }
            else
            {
                enemy.Path = new StraightPath(enemy.Position, new Vector2(0, 1), (float)velocity);
            }
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
            for (int j = 0; j < this.visibleEnemies.Count; j++)
            {
                Enemy enemy = this.visibleEnemies[j];
                if (enemy.IsDestroyed)
                {
                    this.visibleEnemies.RemoveAt(j);
                    this.deadEnemies.Add(enemy);
                    this.collisionBoard.RemoveFromBucketIfExists(enemy);
                    continue;
                }

                if (!enemy.IsDestroyed)
                {
                    enemy.Update(gameTime, this.collisionBoard);
                }
            }

            //Check if player bullets destroyed
            for (int i = 0; i < Player.Instance.Projectiles.Count; i++)
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
            for (int i = 0; i < this.deadEnemies.Count; i++)
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
