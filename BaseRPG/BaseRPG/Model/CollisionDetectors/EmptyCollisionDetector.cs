using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.CollisionDetectors
{
    public class EmptyCollisionDetector : ICollisionDetector
    {
        private bool exists = true;
        public bool Exists { 
            get => exists;
            set {
                if (!value)
                    OnCeaseToExist?.Invoke();
                exists = value;
            } 
        }

        public event Action OnCeaseToExist;

        public void CanCollide(ICollisionDetector other)
        {
            throw new NotImplementedException();
        }

        public void OnCollision(ICollisionDetector other, double delta)
        {

        }


        bool ICollisionDetector.CanCollide(ICollisionDetector other)
        {
            return true;
        }
    }
}
