using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Attack
{
    internal class LinearSwingStrategy : IMovementAngleCalculationStrategy
    {
        private readonly double seconds;
        private readonly Angle angleRange;
        private readonly Angle step;

        public double Seconds => seconds;

        public Angle AngleRange => angleRange;
        public event Action OnFinished;
        public LinearSwingStrategy(double seconds, Angle angleRange)
        {
            this.seconds = seconds;
            this.angleRange = angleRange;
            this.step = angleRange / seconds;

        }

        public double CalculateMovementAngle(double secondsPassed)
        {
            
            var result = secondsPassed * step.Radians;// * anglePerSecond.Radians;
            while (result < 0) {
                result += Math.PI*2;
            }
           
            return result;
        }



    }
}
