using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.WorldInterfaces;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Worlds
{
    public class World : ITickable
    {
        private GameObjectContainer gameObjectContainer = new GameObjectContainer();
        private CallbackQueue callbackQueue = new();
        private readonly string name;

        //private IWorldInitializationStrategy initializationStrategy;

        public Hero Hero { get { return GameObjectContainer.Hero; } set { GameObjectContainer.Hero = value; } }

        public GameObjectContainer GameObjectContainer { get => gameObjectContainer; set => gameObjectContainer = value; }

        public string Name => name;

        public World(GameObjectContainer gameObjectContainer, string name)
        {
            this.GameObjectContainer = gameObjectContainer;
            this.name = name;
        }

        //public void Initialize() {
        //    initializationStrategy.Initialize(gameObjectContainer);
        //}

        public void OnTick(double delta)
        {
            callbackQueue.ExecuteAll();
            List<GameObject> all = GameObjectContainer.All;
            lock (GameObjectContainer) {
                for (int i = 0; i < all.Count; i++) {
                    GameObject gameObject = all[i];
                    try
                    {
                        gameObject.OnTick(delta);
                    }
                    catch (NullReferenceException e){
                        Console.WriteLine(e.StackTrace);
                    }
                    
                }
                all.RemoveAll(g => !g.Exists);
            }
        }
        public void Add(GameObject gameObject) {
            GameObjectContainer.Add(gameObject);
        }
        public void Remove(GameObject gameObject) {
            GameObjectContainer.Remove(gameObject);
        }
        public void QueueForAdd(GameObject gameObject) {
            callbackQueue.QueueAction(()=>Add(gameObject));
        }
    }
}
