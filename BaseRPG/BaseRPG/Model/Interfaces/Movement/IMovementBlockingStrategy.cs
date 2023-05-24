using BaseRPG.Physics.TwoDimensional.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Movement
{
    public interface IMovementBlockingStrategy
    {
        IMovementUnit GenerateMovement(IMovementUnit movement, IPositionUnit position);
        public double CalculateTurnAngle(IPositionUnit position, double turnAngle);
    }
}
