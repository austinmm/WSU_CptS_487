using System;
using System.Collections.Generic;
using ShakeAndBake.Model.GameEntity;

namespace ShakeAndBake.Controller.Collision
{
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
