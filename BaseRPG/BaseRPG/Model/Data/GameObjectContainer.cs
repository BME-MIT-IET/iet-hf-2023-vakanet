using BaseRPG.Model.Exceptions;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseRPG.Model.Data
{
    public class GameObjectContainer
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        public List<GameObject> All { get { lock (this) { return gameObjects; } } }
        //public List<Enemy> Enemies { get { return GetGameObjects<Enemy>("Enemy"); } }

        private Hero hero;
        public Hero Hero {
            get { return hero; }
            set {
                Remove(hero);
                hero = value;
                if (!gameObjects.Contains(hero))
                    Add(hero);
            }
        }

        //public List<T> GetGameObjects<T>(string name)
        //{
        //    Dictionary<string, List<ISeparable>> dict = new Dictionary<string, List<ISeparable>>();
        //    All.ForEach(g => g.Separate(dict));
        //    if (!dict.ContainsKey(name)) return new List<T>();
        //    return dict[name].Select(e => (T)e).ToList();
        //}
        
        public void Add(GameObject gameObject) {
            lock (this) {
                if (gameObject == null)
                    return;
                if (gameObjects.Contains(gameObject)) throw new GameObjectAlreadyInWorldException(gameObject);
                gameObjects.Add(gameObject);
            }
            
        }
        public void Remove(GameObject gameObject){
            lock (this) gameObjects.Remove(gameObject);
        } 
    }
}
