using BaseRPG.Model.Interfaces.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public class StraightMovementStrategy2D : IMovementStrategy
    {
        private Vector2D movementVector;

        public StraightMovementStrategy2D(Vector2D movementVector)
        {
            this.movementVector = movementVector;
        }

        public IMovementUnit CalculateNextMovement(IMovementManager mover, double speed)
        {
            return new MovementUnit2D(movementVector*speed);
        }
    }
}
