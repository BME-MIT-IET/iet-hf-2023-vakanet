using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.WorldInterfaces;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Worlds
{
    public class EmptyWorldFactory:IWorldFactory
    {
        private readonly string name;

        //private IPositionUnit initialHeroPosition;

        public EmptyWorldFactory(string name)
        {
            this.name = name;
            //this.initialHeroPosition = initialHeroPosition;
        }

        public World Create()
        {
            GameObjectContainer gameObjectContainer = new GameObjectContainer();
            var world = new World(gameObjectContainer, name);
            return world;
        }
    }
}
