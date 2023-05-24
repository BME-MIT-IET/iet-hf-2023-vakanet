using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Worlds.Blocks
{
    public class Block : GameObject,ICollisionDetector
    {
        private readonly IPositionUnit positionUnit;

        public Block(IPositionUnit positionUnit, World currentWorld, bool addToWorldInstantly = true) : base(currentWorld, addToWorldInstantly)
        {
            this.positionUnit = positionUnit;
        }

        public override bool Exists => true;

        public IPositionUnit Position => positionUnit;
        public bool CanBeOver { get => false; }
        public override event Action OnCeaseToExist;

        public bool CanCollide(ICollisionDetector other)
        {
            return false;
        }

        public void OnCollision(ICollisionDetector other, double delta)
        {
            
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }

        public override void Step(double delta)
        {
            
        }
    }
}
