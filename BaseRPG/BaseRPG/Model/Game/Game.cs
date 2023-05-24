using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;


namespace BaseRPG.Model.Game
{
    public class Game
    {
        private Game(WorldCatalogue worldCatalogue, IPhysicsFactory physicsFactory,ICollisionNotifier collisionNotifier) {
            
            this.worldCatalogue = worldCatalogue;
            PhysicsFactory = physicsFactory;
            this.collisionNotifier = collisionNotifier;
        }
        private ICollisionNotifier collisionNotifier;
        private WorldCatalogue worldCatalogue = new();
        private World currentWorld;
        private ItemCatalogue itemCatalogue = new();
        public event Action<string, World> CurrentWorldChanged;
        
        public IPhysicsFactory PhysicsFactory { get; set; }
        public World CurrentWorld { get { return currentWorld; } set { currentWorld = value; } }
        public Hero Hero { get {
                return currentWorld.Hero;
            }
            set {
                currentWorld.Hero = value;
            }
        }

        public WorldCatalogue WorldCatalogue { get => worldCatalogue;  }
        public ItemCatalogue ItemCatalogue { get => itemCatalogue; }
        public ICollisionNotifier CollisionNotifier => collisionNotifier;

        public void ChangeWorld(string name) {
            ChangeWorld(worldCatalogue[name].Create());
        }

        public void ChangeWorld(World world)
        {
            CurrentWorld = world;
            CurrentWorldChanged?.Invoke(world.Name, world);
        }
        public void OnTick(double delta)
        {
            collisionNotifier.NotifyCollisions(delta);
            CurrentWorld.OnTick(delta);
        }
        public static Game CreateDefault(Action<string,World> onWorldCreated, ICollisionNotifier collisionNotifier,IPhysicsFactory physicsFactory) {
            return new Builder()
                .WithCollisionNotifier(collisionNotifier)
                .CurrentWorldChanged(onWorldCreated)
                .InitialWorld("Empty")
                .WithPhysicsFactory(physicsFactory)
                .Build();
        }
        public class Builder
        {
            private Action<string, World> onCurrentWorldChanged;
            private World initialWorld;
            private WorldCatalogue worldCatalogue = new();
            private IPhysicsFactory physicsFactory;
            private ICollisionNotifier collisionNotifier;

            public World World => initialWorld;

            public Hero Hero { get { return initialWorld.Hero; } set { initialWorld.Hero = value; } }

            public IPhysicsFactory PhysicsFactory  => physicsFactory;

            public Builder CurrentWorldChanged(Action<string, World> value)
            {
                onCurrentWorldChanged = value;
                return this;
            }

            public Builder InitialWorld(string name)
            {
                initialWorld = worldCatalogue[name].Create();
                return this;
            }
            public Builder WithPhysicsFactory(IPhysicsFactory physicsFactory)
            {
                this.physicsFactory = physicsFactory;
                return this;
            }
            public Builder WithCollisionNotifier(ICollisionNotifier collisionNotifier)
            {
                this.collisionNotifier = collisionNotifier;
                return this;
            }

            public Game Build() {
                var game = new Game(worldCatalogue, physicsFactory,collisionNotifier);
                game.CurrentWorldChanged += onCurrentWorldChanged;
                game.ChangeWorld(initialWorld);
                game.Hero = Hero;
                return game;
            }
        }
    }
}
