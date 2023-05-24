using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.FacingPoint
{
    public class FacingPointAnimation : FacingPointAnimationBase
    {
        private double? lifetimeSeconds;
        private Vector2D point;

        public FacingPointAnimation(double distanceOffsetTowardsPointer, double? lifetimeSeconds = null) : base(distanceOffsetTowardsPointer)
        {
            this.lifetimeSeconds = lifetimeSeconds;
        }

        public FacingPointAnimation(double offsetTowardsPointerAtStart, double offsetTowardsPointerAtEnd, double seconds) 
            : base(offsetTowardsPointerAtStart, offsetTowardsPointerAtEnd, seconds)
        {
            this.lifetimeSeconds = seconds;
        }
        public Vector2D Point { get { return point; } set { point = value; } }

        public override event Action<Interfaces.TransformationAnimation2D> OnAnimationCompleted;

        public override Vector2D GetFacingPoint(DrawingArgs args)
        {
            if (lifetimeSeconds.HasValue) { 
                lifetimeSeconds -= args.Delta;
                if (lifetimeSeconds <= 0) OnAnimationCompleted?.Invoke(this);
            }
            
            return point;
        }
        public static FacingPointAnimation WithChangingOffset(double offsetTowardsPointerAtStart, double offsetTowardsPointerAtEnd, double time) {
            return new FacingPointAnimation(offsetTowardsPointerAtStart, offsetTowardsPointerAtEnd, time);
        }
    }
}
