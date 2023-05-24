using BaseRPG.Model.Interfaces.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public class FollowingMovementStrategy : IMovementStrategy
    {
        private IMovementManager followed;
        private IMovementUnit nextMovement;
        public FollowingMovementStrategy(IMovementManager followed)
        {
            this.followed = followed;
        }

        public IMovementUnit CalculateNextMovement(IMovementManager mover, double speed)
        {

            if (nextMovement != null) return nextMovement;
            Vector2D moverPosition = new(mover.Position.Values[0], mover.Position.Values[1]);
            Vector2D followedPosition = new(followed.Position.Values[0], followed.Position.Values[1]);
            Vector2D direction = (followedPosition - moverPosition).Normalize();
            return new MovementUnit2D(direction * speed);
        }

    }
}
