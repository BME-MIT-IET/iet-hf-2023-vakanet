using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.TransformAnimations;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Attack
{
    public class SwordChargeAnimation : TransformationAnimation2D
    {
        private bool charged = false;
        private readonly Angle finishingAngle;
        private readonly double seconds;
        private readonly Angle startingAngle;
        private readonly Angle step;
        private Angle currentAngle;
        private FacingPointAnimation facingPointAnimation;
        private VibratingAnimation vibratingAnimation = new();
        private double secondsSinceStarted;
        private Angle AngleRange 
        { 
            get
            { 
                return finishingAngle - startingAngle;
            } 
        }

        public Angle CurrentAngle { get { return currentAngle; } }

        public Angle StartingAngle => startingAngle;

        public bool Charged { get => charged;}

        public override event Action<TransformationAnimation2D> OnAnimationCompleted;

        public SwordChargeAnimation(Angle startingAngle, Angle finishingAngle, double seconds)
        {
            this.finishingAngle = finishingAngle;
            this.seconds = seconds;
            this.startingAngle = startingAngle;
            step = AngleRange / seconds;
            facingPointAnimation = new FacingPointAnimation(25 * App.IMAGE_SCALE);
        }


        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {

            facingPointAnimation.Point = nextPosition(animationArgs.PositionOnScreen);

            secondsSinceStarted += animationArgs.Delta;

            var result = facingPointAnimation.GetImage(animationArgs);
            result *= vibratingAnimation.GetImage(animationArgs);
            if (secondsSinceStarted >= seconds)
                charged = true;
            return result;
        }

        #region private functions
        private Vector2D nextPosition(Vector2D positionOnScreen)
        {   
            var movementAngle = Angle.FromRadians(CalculateMovementAngle(secondsSinceStarted));
            currentAngle = StartingAngle - movementAngle;
            return Vector2D.FromPolar(200, currentAngle) + positionOnScreen;
        }
        private double CalculateMovementAngle(double secondsPassed)
        {
            if (charged) return seconds * step.Radians;
            var result = secondsPassed * step.Radians;
            while (result < 0) result += Math.PI * 2;
            return result;
        }

        #endregion

    }
}
