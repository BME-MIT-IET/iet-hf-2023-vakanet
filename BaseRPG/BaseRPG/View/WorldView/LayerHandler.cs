using BaseRPG.View.EntityView;
using System;
using System.Collections.Generic;

namespace BaseRPG.View.WorldView
{
    public class LayerHandler
    {
        public static readonly int LOWEST_LAYER = int.MinValue;
        private SortedDictionary<int, List<IDrawable>> layers = new();
        public void AddToLayer(int layer, IDrawable drawable) {
            lock (layers) { 
                if (layers.ContainsKey(layer))
                {
                    layers[layer].Add(drawable);
                    return;
                }
                layers.Add(layer, new List<IDrawable> { drawable });
            }
            
        }
        public IEnumerable<IDrawable> Drawables
        {
            get
            {
                lock (layers)
                {
                    foreach (int key in layers.Keys)
                    {
                        foreach (var drawable in layers[key])
                        {
                            yield return drawable;
                        }
                    }
                }
                    
            }
        }

        internal void RemoveAll(Predicate<IDrawable> condition)
        {
            lock (layers)
            { 
                foreach (int key in layers.Keys)
                {
                    layers[key].RemoveAll(condition);
                }
            }
            
        }
        public void Remove(IDrawable drawable) {
            lock (layers)
            {
                foreach (int key in layers.Keys)
                {
                    layers[key].Remove(drawable);
                }
            }
            
        }
    }
}