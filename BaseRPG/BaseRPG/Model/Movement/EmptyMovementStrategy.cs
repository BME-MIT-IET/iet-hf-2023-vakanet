using BaseRPG.Model.Interfaces.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Movement
{
    public class EmptyMovementStrategy : IMovementStrategy
    {
        public IMovementUnit CalculateNextMovement(IMovementManager mover, double speed)
        {
            return null;
        }

    }
}
