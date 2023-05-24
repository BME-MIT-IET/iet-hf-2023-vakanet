using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Utility
{
    public class PositionObserver : IPositionProvider
    {
        private Func<Vector2D> positionGetterMethod;
        public PositionObserver(Func<Vector2D> positionGetterMethod)
        {
            this.positionGetterMethod = positionGetterMethod;
        }

        public Vector2D Position => positionGetterMethod();
        public static PositionObserver CreateForLastMovement(IMovementManager movementManager, double veryLargeNumber)
        {
            return new PositionObserver(() =>
            {
                var movement = MovementUnit2D.ToVector2D(movementManager.LastMovement);
                return movement * veryLargeNumber;
            });
        }

    }
}
