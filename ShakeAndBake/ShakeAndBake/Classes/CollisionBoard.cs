using Microsoft.Xna.Framework;
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
    public class CollisionBoard
    {
        private List<List<CollisionBucket>> collisionBuckets;
        private int width;
        private int height;
        private int bucketWidth;

        public CollisionBoard(int windowHeight, int windowWidth, int bucketWidth) {
            this.collisionBuckets = new List<List<CollisionBucket>>();
            this.width = windowWidth / bucketWidth;
            this.height = windowHeight / bucketWidth;

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

        public Vector2 GetCoordinates(float x, float y) {
            Vector2 ret = new Vector2();
            ret.X = x / this.bucketWidth;
            ret.Y = y / this.bucketWidth;
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
                int xOffset = dirs[i, 0];
                int yOffset = dirs[i, 1];

                CollisionBucket cb = FetchBucket(gameObject, xOffset, yOffset);
                /* null indicates that the coordinates were invalid */
                if (cb != null)
                {
                    /* For all game objects in the bucket, if within the collision region add to return set */                
                    foreach (GameObject go in cb.GetObjects()) {
                        if (go.GetType() != type)
                            continue;

                        if (Vector2.Distance(gameObject.Position, go.Position) <= radius
                         || Vector2.Distance(gameObject.Position, go.Position) <= go.HitBoxRadius)
                        {
                            ret.Add(go);
                        }
                    }
                }
            }
            return ret;
        }

        private CollisionBucket FetchBucket(GameObject gameObject)
        {
            Vector2 coordinates = GetCoordinates(gameObject.Position);
            if (!IsValidBucketCoordinates(coordinates.X, coordinates.Y))
                return null;

            CollisionBucket bucket = collisionBuckets[(int)coordinates.X][(int)coordinates.Y];
            return bucket;
        }

        private Boolean IsValidBucketCoordinates(float x, float y)
        {
            // TODO: what is this code trying to do?

            // return !(coordinates.X + xOffset >= this.width || coordinates.X + xOffset < 0
            //     || coordinates.Y + yOffset >= this.height || coordinates.Y + yOffset < 0);
            
            return false;
        }

        private CollisionBucket FetchBucket(GameObject gameObject, int xOffset, int yOffset)
        {
            Vector2 coordinates = GetCoordinates(gameObject.Position);
            if (!IsValidBucketCoordinates(coordinates.X + xOffset, coordinates.Y + yOffset))
                return null;

            CollisionBucket bucket = collisionBuckets[(int)coordinates.X + xOffset][(int)coordinates.Y + yOffset];
            return bucket;
        }

        public void FillBucket(GameObject gameObject) 
        {
            CollisionBucket bucket = FetchBucket(gameObject);
            bucket.AddElement(gameObject);
        }

        public Boolean RemoveFromBucketIfExists(GameObject gameObject)
        {
            CollisionBucket bucket = FetchBucket(gameObject);
            return bucket.RemoveElement(gameObject);
        }
    }
    
    public class CollisionBucket
    {
        private HashSet<GameObject> objects;

        public CollisionBucket()
        {
            objects = new HashSet<GameObject>();
        }

        public HashSet<GameObject> GetObjects()
        {
            return this.objects;
        }

        public void AddElement(GameObject obj)
        {
            objects.Add(obj);
        }

        public Boolean RemoveElement(GameObject obj)
        {
            return objects.Remove(obj);
        }
    }
}
