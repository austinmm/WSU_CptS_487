using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ShakeAndBake;

namespace GameClasses
{
    //GameBoard is static since their can only be one GameBoard in existance at any given time
    static class GameBoard
    {
        //Variable containing the overall speed of the game changed by player input for speed mode
        static private double gameSpeed;
        static public double GameSpeed
        {
            get { return gameSpeed; }
            set { gameSpeed = value;//upon changing gamespeed all entities are updated
                foreach(Enemy enemy in visibleEnemies)
                {
                    enemy.Acceleration = value;
                    foreach (Projectile proj in enemy.Projectiles)
                    {
                        proj.Acceleration = value;
                    }
                }
                foreach (Enemy enemy in deadEnemies)
                {
                    enemy.Acceleration = value;
                    foreach (Projectile proj in enemy.Projectiles)
                    {
                        proj.Acceleration = value;
                    }
                }
                user.Acceleration = value;
                foreach (Projectile proj in user.Projectiles)
                {
                    proj.Acceleration = value;
                }
            }
        }

        //Contains a list of all enemies currently visible on the gameboard
        static private ObservableCollection<Enemy> visibleEnemies;
        static public ObservableCollection<Enemy> VisibleEnemies
        {
            get { return visibleEnemies; }
            set { visibleEnemies = value; }
        }

        //Contains a list of all enemies that have dies but still have projectiles on the gameboard
        static private List<Enemy> deadEnemies;
        static public List<Enemy> DeadEnemies
        {
            get { return deadEnemies; }
            set { deadEnemies = value; }
        }

        //This is our one and only player that is controlled by the user (keyboard input)
        static private Player user;
        static public Player User
        {
            get { return user; }
            set { user = value; }
        }

        //Constructor for GameBoard class
        static GameBoard()
        {
            visibleEnemies = new ObservableCollection<Enemy>();
            deadEnemies = new List<Enemy>();

            user = new Player();
            int width = ShakeAndBakeGame.graphics.GraphicsDevice.Viewport.Width;
            int height = ShakeAndBakeGame.graphics.GraphicsDevice.Viewport.Height;
            user.Position = new Vector2((width / 2 - ShakeAndBakeGame.player.Width / 2),
             height - ShakeAndBakeGame.player.Height);

            //When enemy is added or removed from collection "updateEnimies" is automatically called
            visibleEnemies.CollectionChanged += UpdateEnemies;
        }

        static Random rand = new Random();

        static public void AddEnemy(EnemyType type)
        {
            Enemy enemy = EnemeyFactory.CreateEnemy(type);

            int width = ShakeAndBakeGame.graphics.GraphicsDevice.Viewport.Width;
            int height = ShakeAndBakeGame.graphics.GraphicsDevice.Viewport.Height;

            // spawn enemy on top of screen, randomly between the width of the screen
            enemy.Position = new Vector2(rand.Next(0, width - enemy.Sprite.Width), -enemy.Sprite.Height);
            double velocity = Util.randDouble(1, 3);
            enemy.Velocity = velocity;
            enemy.Path = new StraightPath(enemy.Position, new Vector2(0, 1), (float) velocity);
            visibleEnemies.Add(enemy);
        }

        //When an Enemy is added or removed from the ObservableCollection "visibleEnemies"
        static public void UpdateEnemies(object sender, NotifyCollectionChangedEventArgs e)
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
                    deadEnemies.Add(deadEnemy);
                }
            }
        }

        //MultiThread this Method call
        static public void UpdateBoard(GameTime gameTime)
        {
            user.Update(gameTime);
            //Call update on all our visible enemies, this will automatically update their projectiles as well
            for (int j = 0; j < visibleEnemies.Count; j++)
            {
                Enemy enemy = visibleEnemies[j];
                if (enemy.IsDestroyed)
                {
                    visibleEnemies.RemoveAt(j);
                    deadEnemies.Add(enemy);
                    continue;
                }
                // must iterate using index to remove from list while iterating
                for (int i = 0; i < user.Projectiles.Count; i++)
                {
                    Projectile p = user.Projectiles[i];
                    if (p.IsDestroyed)
                    {
                        user.Projectiles.RemoveAt(i);
                        continue;
                    }
                    if (IsHit(enemy, p))
                    {
                        // player hit enemy, so remove enemy and the projectile
                        enemy.IsDestroyed = true;
                        visibleEnemies.RemoveAt(j);
                        deadEnemies.Add(enemy);

                        p.IsDestroyed = true;
                        user.Projectiles.RemoveAt(i);
                        break;
                    }
                }
                if (!enemy.IsDestroyed)
                {
                    enemy.Update(gameTime);
                }
            }
            //check deadEnimies list to see if they have any projectiles left on the board
            for (int i = 0; i < deadEnemies.Count; i++)
            {
                Enemy enemy = deadEnemies[i];
                enemy.Update(gameTime);
                if (enemy.Projectiles.Count == 0)
                {
                    deadEnemies.RemoveAt(i);
                }
            }
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in visibleEnemies)
            {
                enemy.Draw(spriteBatch);
            }
            // draw player last
            user.Draw(spriteBatch);
        }
        
        static public bool IsHit(Character character, Projectile projectile)
        {
            if (character != null && projectile != null)
            {
                if (character.BoundsContains(projectile.Position))
                {
                //Unsure if logic is correct
                // double xLow = character.Position.X - character.HitBoxRadius;
                // double xHigh = character.Position.X - character.HitBoxRadius;
                // double yLow = character.Position.Y - character.HitBoxRadius;
                // double yHigh = character.Position.Y - character.HitBoxRadius;
                // if (projectile.Position.X >= xLow && projectile.Position.X >= xHigh) {
                    //character is Hit
                    character.Health -= projectile.HitDamage;
                    //Check if character is dead
                    if (character.Health < 0)
                    {
                        //if character is the player
                        if (character is Player)
                        {
                            GameBoard.PlayerDied();
                        }
                        //if character is an enemy
                        else if (character is Enemy)
                        {
                            GameBoard.visibleEnemies.Remove((Enemy)character);
                        }
                        return true;
                    }
                    return true;
                }
            }
            return false;
        }

        // called before configuring a phase
        static public void Reset()
        {
            // clear player bullets
            user.Projectiles.Clear();
        }
        
        static public void PlayerDied() {
            // game over
        }
    }
}
