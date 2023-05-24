using BaseRPG.Model.Interfaces.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Movement
{
    /// <summary>
    /// An object with this strategy can move through any block in the world
    /// </summary>
    public class UnstoppableMovementBlockingStrategy : IMovementBlockingStrategy
    {
        public double CalculateTurnAngle(IPositionUnit position, double turnAngle)
        {
            return turnAngle;
        }

        public IMovementUnit GenerateMovement(IMovementUnit movement, IPositionUnit position)
        {
            return movement;
        }
    }
}
