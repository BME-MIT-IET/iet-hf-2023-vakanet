using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Attack
{
    internal class LightAttackMovementAngleStrategy : IMovementAngleCalculationStrategy
    {
        private Angle angleRange;
        private double seconds;

        public LightAttackMovementAngleStrategy(Angle angleRange, double seconds)
        {
            this.angleRange = angleRange;
            this.seconds = seconds;
        }

        public double Seconds => seconds;

        public Angle AngleRange => angleRange;

        // Summary:
        //     The default parameterization for CalculateMovementAngle(double t, double sinExponent, double firstMinimumX)
        public double CalculateMovementAngle(double secondsPassed)
        {
            return CalculateMovementAngle(secondsPassed, 1.9, 0.4798398);
        }

        // Summary:
        //     The current angle is calculated by a sine function of time.
        //     The animation starts in one direction (charge-up of the swing),
        //     and gradually slows down as switches directions once, then the swing happens.
        //     The animation stops after the current angle reaches the first positive minimum
        //     of the function, so it only switches directions once
        //     the function for the angle is in
        //                  A/2 * sin( (c(m)*x(t))^d )
        //     form, where
        //     A is defined by the angle that the sword swing should have (angleRange)
        //     c(m) is the coefficient that dictates the speed of the animation, to make sure it ends in time
        //     m is the first positive local minimum of the sin(x(t)^d) function
        //     x is just a linear function of t: x = t*3/2*pi. This is because the sin function has the first positive minimum at 3/2
        //     d is the exponent which serves as a way to alter the speed distribution of the animation
        //     (higher d will result in the angle speed being slower when the sword is close to the side of the given angle range)
        //     
        //     
        // Parameters:
        //   t:
        //     the current time. Same as t in the upper formula
        //   sinExponent:
        //      same as d in the upper formula.
        //   fistMinimumX:
        //      same as m in the upper formula. The first positive local minimum of the sin( (t * 3/2 * pi) ^ sinExponent ) function.
        private double CalculateMovementAngle(double t, double sinExponent, double firstMinimumX)
        {

            double oneAndAHalfPi = Math.PI * (3.0 / 2.0);
            double sinParam = Math.Pow(InnerSinCoefficient(firstMinimumX) * t * oneAndAHalfPi, sinExponent);
            return Math.Sin(sinParam) * angleRange.Radians / 2;


        }

        private double InnerSinCoefficient(double firstMinimumX)
        {
            return 1 / (seconds / firstMinimumX);
        }
        

    }
}
