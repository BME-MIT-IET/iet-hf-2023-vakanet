using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseRPG.Model.Utility
{
    public class OneToManyProvider<ONE,MANY> 
        where ONE : class
        where MANY : class
    {
        private List<Pair> pairs = new();
        private Dictionary<string, MANY> lastRequestCache = new();
        private ONE lastRequestParam;
        public void Connect(ONE o1, MANY o2, string name = "default")
        {
            var objects = GetObjectsOf(o1);
            if (objects == null)
            {
                pairs.Add(new(o2, o1, name));
                return;
            }
            objects.Add(name, o2);
        }
        public void Connect(MANY o1, ONE o2, string name = "default")
        {
            Connect(o2, o1);
        }
        public bool AreConnected(MANY manyObject, ONE oneObject)
        {
            Dictionary<string, MANY> manyObjects = GetObjectsOf(oneObject);
            if (manyObjects == null) return false;
            return manyObjects.Values.Contains(manyObject);
        }

        public Dictionary<string, MANY> GetObjectsOf(ONE oneObject)
        {
            lastRequestParam = oneObject;
            lastRequestCache = pairs.Find(p => p.OneObject == oneObject)?.ManyObjects;
            return lastRequestCache;
        }
        public List<MANY> GetObjectsAsListOf(ONE oneObject)
        {
            return GetObjectsOf(oneObject).Values.ToList();
        }
        public MANY GetObject(ONE oneObject, string name, bool fromCache = false)
        {

            Dictionary<string, MANY> drawables = null;
            if (fromCache)
            {
                if (lastRequestParam == oneObject)
                    drawables = lastRequestCache;
            }
            else
                drawables = GetObjectsOf(oneObject);

            if (drawables == null) return null;
            if (!drawables.ContainsKey(name))
            {
                return null;
            }
            return drawables[name];
        }
        public MANY GetSingleObject(ONE oneObject) {
            return GetObjectsAsListOf(oneObject)[0];
        }
        public U GetObject<U>(ONE oneObject, string name, bool fromCache = false) where U : class, MANY
        {
            return GetObject(oneObject, name, fromCache) as U;
        }

        private class Pair
        {
            private Dictionary<string, MANY> objects = new();
            private ONE oneObject;
            public Pair(MANY manyObject, ONE oneObject, string name)
            {
                this.oneObject = oneObject;
                objects.Add(name, manyObject);
            }
            public Dictionary<string, MANY> ManyObjects => objects;
            public ONE OneObject => oneObject;
        }

    }
}
