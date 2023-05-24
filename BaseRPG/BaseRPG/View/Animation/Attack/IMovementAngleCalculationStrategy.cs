using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Attack
{
    public interface IMovementAngleCalculationStrategy
    {
        double Seconds { get; }
        Angle AngleRange { get; }
        double CalculateMovementAngle(double secondsPassed);
    }
}
