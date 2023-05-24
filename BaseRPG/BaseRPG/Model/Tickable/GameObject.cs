using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Utility;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;


namespace BaseRPG.Model.Tickable
{
    public abstract class GameObject : ITickable, ISeparable, IExisting, ICanQueueAction
    {

        public abstract bool Exists { get; }
        public abstract event Action OnCeaseToExist;
        public virtual event Action<Hero, GameObject> InteractionStarted;
        public abstract void Step(double delta);
        public abstract void Separate(Dictionary<string, List<ISeparable>> dict);
        public GameObject(World currentWorld, bool addToWorldInstantly = true)
        {
            this.currentWorld = currentWorld;
            if (addToWorldInstantly)
                currentWorld.Add(this);
        }
        private CallbackQueue callbackQueue = new();
        public World CurrentWorld { get { return currentWorld; } }
        private readonly World currentWorld;

        public virtual void BeforeStep(double delta)
        {

        }
        public void OnTick(double delta)
        {
            BeforeStep(delta);
            callbackQueue.ExecuteAll();
            Step(delta);
        }
        public void QueueAction(Action action)
        {
            callbackQueue.QueueAction(action);
        }

        public virtual void InteractWith(Hero interactionStarter)
        {

        }
    }
}
