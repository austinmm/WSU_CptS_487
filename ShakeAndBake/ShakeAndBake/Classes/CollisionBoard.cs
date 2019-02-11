using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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

        public Vector2 GetCoordinates(float x, float y) {
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
                int xOffset = dirs[i, 0];
                int yOffset = dirs[i, 1];

                CollisionBucket cb = FetchBucket(gameObject, xOffset, yOffset);
                /* null indicates that the coordinates were invalid */
                if (cb != null)
                {
                    /* For all game objects in the bucket, if within the collision region add to return set */                
                    foreach (GameObject go in cb.GetObjects()) {
                        if (!go.GetType().IsSubclassOf(type) && !go.GetType().Equals(type))
                            continue;
                        if (gameObject.BoundsContains(go.Position)
                            || go.BoundsContains(gameObject.Position))
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
