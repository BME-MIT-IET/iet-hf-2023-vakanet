using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision.Attacks
{
    public class Attack2DBuilderHelper
    {
        private Attack attack;
        private IPositionUnit ownerPosition;
        private bool rotated;
        public Attack2DBuilderHelper(Attack attack)
        {
            this.Attack = attack;
        }
        public Attack2DBuilderHelper()
        {
        }
        public IPositionUnit OwnerPosition { set => ownerPosition = value; }
        public bool Rotated {set => rotated = value; }
        public IMovementManager MovementManager => Attack.MovementManager;

        public Attack Attack { get => attack; set => attack = value; }

        public double calculateInitialRotaion()
        {
            double initialRotation = 0;
            if (rotated)
            {
                double[] values = ownerPosition.MovementTo(Attack.Position).Values;
                initialRotation = Math.Atan2(values[1], values[0]);
            }
            return initialRotation;
        }
    }
}
