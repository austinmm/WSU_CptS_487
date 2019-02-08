using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

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
        static private int enemiesLeft;
        static public int EnemiesLeft
        {
            get { return enemiesLeft; }
            set { enemiesLeft = value; }
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
            //When enemy is added or removed from collection "updateEnimies" is automatically called
            visibleEnemies.CollectionChanged += UpdateEnemies;
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
            foreach (Enemy enemy in visibleEnemies)
            {
                enemy.Update(gameTime);
            }
            //check deadEnimies list to see if they have any projectiles left on the board
            foreach (Enemy enemy in deadEnemies)
            {
                enemy.Update(gameTime);
                if (enemy.Projectiles.Count == 0)
                {
                    deadEnemies.Remove(enemy);
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
                //Unsure if logic is corrects
                double xLow = character.Position.X - character.HitBoxRadius;
                double xHigh = character.Position.X - character.HitBoxRadius;
                double yLow = character.Position.Y - character.HitBoxRadius;
                double yHigh = character.Position.Y - character.HitBoxRadius;
                if (projectile.Position.X >= xLow && projectile.Position.X >= xHigh
                    && projectile.Position.Y >= yLow && projectile.Position.Y >= yHigh)
                {
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
                }
            }
            return false;
        }

        static public void PlayerDied() {
            // game over
        }
    }
}
