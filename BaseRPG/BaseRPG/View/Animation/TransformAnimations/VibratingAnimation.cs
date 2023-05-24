using BaseRPG.Model.Utility;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.TransformAnimations
{
    
    public class VibratingAnimation : TransformationAnimation2D
    {

        private class DoubleRange {
            private double max;
            private double min;

            public DoubleRange(double min, double max)
            {
                this.max = max;
                this.min = min;
            }

            public bool IsInRange(double number) {
                return number <= max && number >= min;
            }
            public void Increase(double value) {
                max += value;
                min -= value;
            }
            public bool Between(DoubleRange other) {
                return min > other.min && max < other.max;
            }
        }
        private Vector2D currentShift = new(0,0);
        private Default<DoubleRange> rangeX = new(new(-5, 5));
        private Default<DoubleRange> rangeY = new(new(-5, 5));
        private Default<double> shakeSpeed = 100;
        private double secondsSinceBeggining = 0;


        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {

            secondsSinceBeggining += animationArgs.Delta;

            if (shakeSpeed.CurrentValue < 250) {
                shakeSpeed.CurrentValue = shakeSpeed.DefaultValue + (secondsSinceBeggining * 30);
            }
            if (rangeX.CurrentValue.Between(new(-10,10))) {
                rangeX.Reset();
                rangeX.CurrentValue.Increase(Math.Sqrt(secondsSinceBeggining));
            }
            if (rangeY.CurrentValue.Between(new(-10, 10)))
            {
                rangeY.Reset();
                rangeY.CurrentValue.Increase(Math.Sqrt(secondsSinceBeggining));
            }

            currentShift += generateMovement(animationArgs.Delta);
            return Matrix3x2.CreateTranslation((float)currentShift.X, (float)currentShift.Y);
        }
        private Vector2D generateMovement(double delta) {
            var randX = generateRandom() * delta ;
            var randY = generateRandom() * delta ;
            var result = new Vector2D(randX, randY);
            var shifted = currentShift + result;
            if (isInRange(shifted))
                return result;

            // I try multiplying with negative, so I don't have to return a 0,0 vector,
            // because that looks bad (it makes the vibration stop for a moment)
            shifted = currentShift - result;
            if (isInRange(shifted))
                return result * -1;
            return new Vector2D(0, 0);
        }
        private bool isInRange(Vector2D shifted) {
            return rangeX.CurrentValue.IsInRange(shifted.X)
                && rangeY.CurrentValue.IsInRange(shifted.Y);
        }
        private double generateRandom() {
            Random random = new Random();
            return (random.NextDouble() - 0.5) * shakeSpeed.CurrentValue ;
        }
    }
}
