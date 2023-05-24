using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Movement
{
    public interface IPositionUnit
    {
        void MoveBy(IMovementUnit movementUnit);
        IMovementUnit MovementTo(IPositionUnit positionUnit);
        IPositionUnit Copy();
        double[] Values { get; }
    }
}
