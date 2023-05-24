using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public struct MovementUnit2D : IMovementUnit
    {
        private Vector2D movement;

        public MovementUnit2D(Vector2D movement)
        {
            this.movement = movement;
        }

        public MovementUnit2D(double v1, double v2)
        {
            this.movement = new Vector2D(v1, v2);
        }

        public double[] Values => new double[] { movement.X, movement.Y};

        public IMovementUnit Add(IMovementUnit otherMovement)
        {
            return new MovementUnit2D(new(otherMovement.Values[0] + movement.X, otherMovement.Values[1] + movement.Y));
        }

        public IMovementUnit UniteWith(List<IMovementUnit> otherMovementUnits)
        {
            var avgMovement = movement;
            var avgLength = movement.Length;
            foreach (var otherMovementUnit in otherMovementUnits)
            {
                Vector2D otherMovementUnit2D = new(otherMovementUnit.Values[0], otherMovementUnit.Values[1]);
                avgMovement += otherMovementUnit2D;
                avgLength += otherMovementUnit2D.Length;
            }
            if (avgMovement.Length < 0.000001) return new MovementUnit2D(avgMovement);
            avgMovement = avgMovement.Normalize() * (avgLength / (otherMovementUnits.Count + 1));
            return new MovementUnit2D(avgMovement);
        }

        internal static Vector2D ToVector2D(IMovementUnit movement)
        {
            var values = movement.Values;
            return new Vector2D(values[0], values[1]);
        }

        public IMovementUnit Clone()
        {
            return new MovementUnit2D(movement);
        }

        public IMovementUnit Scaled(double scalar)
        {
            return new MovementUnit2D(movement * scalar);
        }

        public IMovementUnit WithLength(double newLength)
        {
            return new MovementUnit2D(movement.Normalize() * newLength);
        }

        public bool GreaterThan(IMovementUnit otherMovement)
        {
            return movement.Length > new Vector2D(otherMovement.Values[0], otherMovement.Values[1]).Length;
        }

        //public IMovementUnit Unite(IMovementUnit movementUnit,int weight)
        //{
        //    var otherMovement = new Vector2D(movementUnit.Values[0], movementUnit.Values[1])*weight;
        //    var sum = (movement*otherMovement.Length/Math.Sqrt(2) + otherMovement);
        //    if (sum.Length<0.0001) return new MovementUnit2D(sum);
        //    return new MovementUnit2D(sum.Normalize()*((movement.Length+otherMovement.Length)/(weight+1.0)));
        //}
    }
}
