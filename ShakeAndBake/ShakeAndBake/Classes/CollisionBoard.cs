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
using System.Numerics;

namespace GameClasses
{
    public class CollisionBoard
    {
        public CollisionBoard(Int16 windowHeight, Int16 windowWidth, Int16 bucketWidth) {
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

        public Vector2 getCoordinates(Int16 x, Int16 y) {
            Vector2 ret = new Vector2();
            ret.X = x / this.bucketWidth;
            ret.Y = y / this.bucketWidth;
            return ret;
        }

        public HashSet<GameObject> getObjectsCollided(GameObject gameObject, Type type = GameObject /* Optional filter parameter */)
        {
            HashSet<GameObject> ret = new HashSet<GameObject>();
            double radius = gameObject.HitBoxRadius;

            /* Directions */
            int[] dirs = new int[][] {  { 1,1 }, { 0, 1 }, { -1, 1 },
                                        { 1, 0 } , { 0, 0 } , { -1, 0 }
                                        { 1, -1 } , { 0, -1 } , { -1, -1 } };

            /* Check all neighboring cells */
            for (int i = 0; i < 9; ++i)
            {
                int[] dir = dirs[i];
                int xOffset = dir[0];
                int yOffset = dir[1];

                CollisionBucket cb = fetchBucket(gameObject, xOffset, yOffset);

                /* null indicates that the coordinates were invalid */
                if (cb != null)
                {
                    /* For all game objects in the bucket, if within the collision region add to return set */                
                    for (GameObject go in cb.getObjects()) {
                        if (go.getType() != type)
                            continue;

                        if (Vector2.Distance(gameObject, go) <= radius || Vector2.Distance(gameObject, go) <= go.HitBoxRadius)
                        {
                            ret.Add(cb);
                        }
                    }
                }
            }
            return ret;
        }

        private CollisionBucket fetchBucket(GameObject gameObject)
        {
            Vector2 coordinates = getCoordinates(gameObject.Position);
            if (!isValidBucketCoordinates(coordinates.X + xOffset, coordinates.Y + yOffset))
                return null;

            CollisionBucket bucket = collisionBuckets[coordinats.X][coordinates.Y];
            return bucket;
        }

        private Boolean isValidBucketCoordinates(int x, int y)
        {
            return !(coordinates.X + xOffset >= this.width || coordinates.X + xOffset < 0
                || coordinates.Y + yOffset >= this.height || coordinates.Y + yOffset < 0);
        }

        private CollisionBucket fetchBucket(GameObject gameObject, int xOffset, int yOffset)
        {
            Vector2 coordinates = getCoordinates(gameObject.Position);
            if (!isValidBucketCoordinates(coordinates.X + xOffset, coordinates.Y + yOffset))
                return null;

            CollisionBucket bucket = collisionBuckets[coordinates.X + xOffset][coordinates.Y + yOffset];
            return bucket;
        }

        public void fillBucket(GameObject gameObject) 
        {
            CollisionBucket bucket = fetchBucket(gameObject);
            bucket.addElement(gameObject);
        }

        public Boolean removeFromBucketIfExists(GameObject gameObject)
        {
            CollisionBucket bucket = fetchBucket(gameObject);
            return bucket.removeElement(gameObject);
        }

        public class CollisionBucket
        {
            public CollisionBucket()
            {
                objects = new HashSet<GameObject>();
            }

            public HashSet<GameObject> getObjects()
            {
                return this.objects;
            }

            public void addElement(GameObject obj)
            {
                objects.Add(obj);
            }

            public Boolean removeElement(GameObject obj)
            {
                return objects.remove(obj);
            }

            private HashSet<GameObject> objects;
        }

        private List<List<CollisionBucket>> collisionBuckets;
        private Int16 width;
        private Int16 height;
        private Int16 bucketWidth;
    }

}
