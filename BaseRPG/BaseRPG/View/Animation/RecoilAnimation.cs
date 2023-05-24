using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    public class RecoilAnimation : TransformationAnimation2D
    {
        private static double DEFAULT_LIFETIME_IN_SECONDS = 0.3;

        private FacingPointAnimation facingPointAnimation = new FacingPointAnimation(0);
        private AnimationTimer timer = new AnimationTimer(DEFAULT_LIFETIME_IN_SECONDS);
        private bool isFixed = false;
        public override event Action<TransformationAnimation2D> OnAnimationCompleted;
        public RecoilAnimation()
        {
            timer.Elapsed += () => OnAnimationCompleted?.Invoke(this);
        }
        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            fixIfNotFixed(animationArgs.MousePositionOnScreen);
            timer.Tick(animationArgs.Delta);
            var reciprocal = 1 / timer.MaxSeconds;
            var phase = Math.Sin(Math.PI * reciprocal * timer.SecondsSinceStarted);
            facingPointAnimation.DistanceOffsetTowardsPointer = 100-phase * 10 * App.IMAGE_SCALE;
            return facingPointAnimation.GetImage(animationArgs);
        }
        private void fixIfNotFixed(Vector2D position) {
            if (!isFixed)
            {
                facingPointAnimation.Point = position;
                isFixed = true;
            }
        }
    }
}
