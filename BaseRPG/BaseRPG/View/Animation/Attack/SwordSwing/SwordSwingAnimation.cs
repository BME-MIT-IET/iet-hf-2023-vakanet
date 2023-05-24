using BaseRPG.View.Animation.FacingPoint;
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

namespace BaseRPG.View.Animation.Attack.SwordSwing
{

    public class SwordSwingAnimation : TransformationAnimation2D
    {

        private bool isOver = false;
        private bool hasInvokedAnimationAlmostEnding = false;
        private readonly Angle angleRange;
        private Angle startingAngle;
        private Angle currentAngle;
        private FacingPointAnimation facingPointAnimation;
        private AnimationTimer timer;
        private IMovementAngleCalculationStrategy movementAngleCalculationStrategy;

        public Angle CurrentAngle { get { return currentAngle; } }
        public Angle StartingAngle => startingAngle;
        public Angle FinishAngle => StartingAngle + AngleRange;
        public Angle AngleRange => angleRange;

        public event Action<SwordSwingAnimation> OnAnimationAlmostEnding;
        public override event Action<TransformationAnimation2D> OnAnimationCompleted;

        public SwordSwingAnimation(
            Angle startingAngle,
            IMovementAngleCalculationStrategy movementAngleCalculationStrategy,
            double lastFrameHoldDurationInSeconds
            )
        {
            this.angleRange = movementAngleCalculationStrategy.AngleRange;
            var seconds = movementAngleCalculationStrategy.Seconds;
            initTimers(seconds, lastFrameHoldDurationInSeconds);

            facingPointAnimation = new FacingPointAnimation(25 * App.IMAGE_SCALE);
            OnAnimationAlmostEnding += a => hasInvokedAnimationAlmostEnding = true;
            this.movementAngleCalculationStrategy = movementAngleCalculationStrategy;
            this.startingAngle = startingAngle;
        }

        public Angle GetCurrentAngle()
        {
            return currentAngle;
        }

        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            if (!isOver)
            {
                facingPointAnimation.Point = nextPosition(animationArgs.PositionOnScreen);
                setStateForAnimation();
            }
            timer.Tick(animationArgs.Delta);
            if (timer.SecondsSinceStarted / timer.MaxSeconds >= 0.85)
                if (!hasInvokedAnimationAlmostEnding)
                    OnAnimationAlmostEnding?.Invoke(this);
            return facingPointAnimation.GetImage(animationArgs);
        }
        protected virtual Vector2D calculateFirstPointOffset()
        {
            return new Vector2D(0, 0);
        }
        protected virtual double calculateDistanceOffsetTowardsPointer()
        {
            return 100;
        }
        protected double CalculatedMovementAngle =>
    movementAngleCalculationStrategy.CalculateMovementAngle(timer.SecondsSinceStarted);
        #region private functions
        private void setStateForAnimation()
        {
            facingPointAnimation.FirstPointOffset = calculateFirstPointOffset();
            facingPointAnimation.DistanceOffsetTowardsPointer = calculateDistanceOffsetTowardsPointer();

        }



        private Vector2D nextPosition(Vector2D positionOnScreen)
        {
            var movementAngle = CalculatedMovementAngle;
            currentAngle = StartingAngle - Angle.FromRadians(movementAngle);
            return Vector2D.FromPolar(200, currentAngle) + positionOnScreen;
        }
        private void initTimers(double seconds, double secondsAfterOver)
        {
            timer = new(seconds);
            timer.Elapsed += () =>
            {
                if (!hasInvokedAnimationAlmostEnding)
                    OnAnimationAlmostEnding?.Invoke(this);

                isOver = true;
                timer = new(secondsAfterOver);
                timer.Elapsed += () =>
                {
                    OnAnimationCompleted?.Invoke(this);
                };
            };
        }

        #endregion
        
    }
}
