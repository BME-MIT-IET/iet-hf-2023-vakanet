using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Movement
{
    public interface IMovementUnit
    {
        IMovementUnit Scaled(double scalar);
        IMovementUnit WithLength(double newLength);
        double[] Values { get; }
        IMovementUnit Clone();
        static IMovementUnit operator +(IMovementUnit movement1, IMovementUnit movement2) {
            return movement1.Add(movement2);
        }
        static IMovementUnit Unite(List<IMovementUnit> movementUnits)
        {
            if (movementUnits.Count == 0) return null;
            var first = movementUnits[0];
            movementUnits.RemoveAt(0);
            return first.UniteWith(movementUnits);
        }
        IMovementUnit UniteWith(List<IMovementUnit> otherMovementUnits);
        IMovementUnit Add(IMovementUnit otherMovement);
        bool GreaterThan(IMovementUnit otherMovement);
    }
}
