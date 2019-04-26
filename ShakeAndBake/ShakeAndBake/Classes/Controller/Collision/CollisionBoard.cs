using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Controller.Collision
{
    public class CollisionBoard
    {
        private List<List<CollisionBucket>> collisionBuckets;
        private int width, height, bucketWidth;

        public CollisionBoard(int windowHeight, int windowWidth, int bucketWidth)
        {
            this.collisionBuckets = new List<List<CollisionBucket>>();

            this.width = windowWidth / bucketWidth + bucketWidth;
            this.height = windowHeight / bucketWidth + bucketWidth;

            for (int i = 0; i < this.width; ++i)
            {
                this.collisionBuckets.Add(new List<CollisionBucket>());
                for (int j = 0; j < this.height; ++j)
                {
                    this.collisionBuckets[i].Add(new CollisionBucket());
                }
            }
            this.bucketWidth = bucketWidth;
        }

        public Vector2 GetCoordinates(Vector2 position)
        {
            return GetCoordinates(position.X, position.Y);
        }

        public Vector2 GetCoordinates(float x, float y)
        {
            Vector2 ret = new Vector2();
            ret.X = (int)(x / this.bucketWidth);
            ret.Y = (int)(y / this.bucketWidth);
            return ret;
        }

        public HashSet<GameObject> GetObjectsCollided(GameObject gameObject, Type type) /* Optional filter parameter */
        {
            HashSet<GameObject> ret = new HashSet<GameObject>();
            double radius = gameObject.HitBoxRadius;

            /* Directions */
            int[,] dirs = new int[,] {
                { 1, 1 }, { 0, 1 }, { -1, 1 },
                { 1, 0 } , { 0, 0 } , { -1, 0 },
                { 1, -1 } , { 0, -1 } , { -1, -1 }
            };

            /* Check all neighboring cells */
            for (int i = 0; i < 9; ++i)
            {
                /* In the future, we can use j to expand the radius of our checks if needed */
                for (int j = 1; j <= 1; ++j)
                {
                    int xOffset = dirs[i, 0];
                    int yOffset = dirs[i, 1];

                    CollisionBucket cb = FetchBucket(gameObject, xOffset, yOffset);
                    /* null indicates that the coordinates were invalid */
                    if (cb != null)
                    {
                        /* For all game objects in the bucket, if within the collision region add to return set */
                        foreach (GameObject go in cb.GetObjects())
                        {
                            if (!go.GetType().IsSubclassOf(type) && !go.GetType().Equals(type))
                                continue;
                            if (gameObject.BoundsContains(go)
                                || go.BoundsContains(gameObject))
                            {
                                ret.Add(go);
                            }
                        }
                    }
                }
            }
            return ret;
        }

        private CollisionBucket FetchBucket(GameObject gameObject)
        {
            Vector2 coordinates = GetCoordinates(gameObject.Position);
            if (!IsValidBucketCoordinates((int)coordinates.X, (int)coordinates.Y))
                return null;

            CollisionBucket bucket = collisionBuckets[(int)coordinates.X][(int)coordinates.Y];
            return bucket;
        }

        /* This only checks to ensure that x and y do not fall out of bounds on the collision board.
           In the future this may have additional checks, but this seems sufficient for now.  
        */
        private Boolean IsValidBucketCoordinates(int x, int y)
        {
            return !(x >= this.width || x < 0
              || y >= this.height || y < 0);
        }

        private CollisionBucket FetchBucket(GameObject gameObject, int xOffset, int yOffset)
        {
            Vector2 coordinates = GetCoordinates(gameObject.Position);

            /* If the coordinates are out of bounds, we return null. */
            if (!IsValidBucketCoordinates((int)coordinates.X + xOffset, (int)coordinates.Y + yOffset))
                return null;

            CollisionBucket bucket = collisionBuckets[(int)coordinates.X + xOffset][(int)coordinates.Y + yOffset];
            return bucket;
        }

        public void FillBucket(GameObject gameObject)
        {
            CollisionBucket bucket = FetchBucket(gameObject);

            if (bucket == null)
            {
                return;
            }
            bucket.AddElement(gameObject);
        }

        public Boolean RemoveFromBucketIfExists(GameObject gameObject)
        {
            var t = gameObject;
            CollisionBucket bucket = FetchBucket(gameObject);
            if (bucket == null)
                return false;

            return bucket.RemoveElement(gameObject);
        }

        public void HandleEnemyBulletCollisions(EnemyBullet bullet)
        {
            HashSet<GameObject> collidedBullets = GetObjectsCollided(bullet, typeof(EnemyBullet));
            HashSet<GameObject> collidedEnemies = GetObjectsCollided(bullet, typeof(Enemy));

            //player takes damage from enemy bullet
            if (Player.Instance.BoundsContains(bullet) || bullet.BoundsContains(Player.Instance))
            {
                Player.Instance.TakeDamage(bullet.HitDamage);
                bullet.IsDestroyed = true;
            }

            // see if deflected bullet hits enemy bullet
            foreach (EnemyBullet go in collidedBullets)
            {
                if (bullet.IsDestroyed) { break; }
                else if (go.IsDestroyed || go == bullet) { continue; }
                if (bullet.Path.WasDeflected)
                {
                    HandleProjectileCollision(bullet, go);
                }

            }
            // see if deflected enemy bullet hits an enemy
            foreach (Enemy go in collidedEnemies)
            {
                if (bullet.IsDestroyed) { break; }
                else if (go.IsDestroyed) { continue; }
                if (bullet.Path.WasDeflected)
                {
                    go.TakeDamage(bullet.HitDamage);
                    bullet.IsDestroyed = true;
                }

            }
            if (bullet.IsDestroyed)
            {
                RemoveFromBucketIfExists(bullet);
            }
        }

        public void HandlePlayerBulletCollisions(PlayerBullet bullet)
        {
            // player hits itself from defelcted bullet
            if ((Player.Instance.BoundsContains(bullet) || bullet.BoundsContains(Player.Instance)) && bullet.Path.WasDeflected == true)
            {
                Player.Instance.TakeDamage(bullet.HitDamage);
                bullet.IsDestroyed = true;
            }

            HashSet<GameObject> collidedEnemies = GetObjectsCollided(bullet, typeof(Enemy));
            HashSet<GameObject> collidedBullets = GetObjectsCollided(bullet, typeof(EnemyBullet));

            //collision with enemy
            foreach (Enemy enemyObject in collidedEnemies)
            {
                if (enemyObject.IsDestroyed) { continue; }
                enemyObject.TakeDamage(bullet.HitDamage);
                bullet.IsDestroyed = true;
            }
            //collision with enemy bullets
            foreach (EnemyBullet enemyBullet in collidedBullets)
            {
                if (bullet.IsDestroyed) { break; }
                else if (enemyBullet.IsDestroyed) { continue; }
                HandleProjectileCollision(bullet, enemyBullet);
            }

            if (bullet.IsDestroyed)
            {
                RemoveFromBucketIfExists(bullet);
            }
        }

        private void HandleProjectileCollision(Projectile bullet, Projectile other)
        {
            if (bullet.Path.WasDeflected)
            {
                bullet.IsDestroyed = true;
            }
            if (!other.IsBouncy && !bullet.IsBouncy)
            {
                bullet.IsDestroyed = true;
                other.IsDestroyed = true;
            }
            float m1 = (float)(bullet.Sprite.Width * bullet.Sprite.Height) * (float)bullet.Density;
            float m2 = (float)(other.Sprite.Width * other.Sprite.Height) * (float)other.Density;

            Vector2 P = m1 * bullet.Path.GetVelocityVector()
                + m2 * other.Path.GetVelocityVector();

            Vector2 C = bullet.Path.GetVelocityVector()
                - other.Path.GetVelocityVector();

            // P = m1v1' + m2v2'
            // v1 - v2 = v2' - v1'
            // let c = v1 - v2

            // m1v1' + m2v2' = P
            // -v1'  +  v2'  = C

            // m1v1' + m2v2' = P
            // -m1v1'+ m1v2' = m1C

            // (m1 + m2)v2' = P + m1C

            // v2' = (P + m1C)/(m1 + m2)

            Vector2 v2f = (P + m1 * C) / (m1 + m2);

            // c = v2' - v1'

            // v1' = v2' - c

            Vector2 v1f = v2f - C;


            // perfecly elastic for now
            float heatLoss = 1f;

            // normalize vectors if too fast!
            v2f.Normalize();
            v1f.Normalize();

            other.Path.SetVelocityVector(v2f * heatLoss);
            other.Velocity = v2f.Length();
            bullet.Path.SetVelocityVector(v1f * heatLoss);
            bullet.Velocity = v1f.Length();
        }
    }
}
