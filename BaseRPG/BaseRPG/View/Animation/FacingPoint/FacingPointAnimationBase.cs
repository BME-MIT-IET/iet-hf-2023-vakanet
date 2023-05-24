using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.FacingPoint
{
    // Draws the given image on the line defined by
    // - the position given when calling the animation, and
    // - the position given when creating the object. 
    public abstract class FacingPointAnimationBase : Interfaces.TransformationAnimation2D
    {
        private readonly double offsetStep;
        private Vector2D lastSecondPoint = new Vector2D(0,0);
        private double currentOffsetTowardsPointer;
        private readonly double offsetTowardsPointerAtEnd;

        public FacingPointAnimationBase(double distanceOffsetTowardsPointer)
            :this(distanceOffsetTowardsPointer, distanceOffsetTowardsPointer,10)
        {
        }
        public FacingPointAnimationBase(double offsetTowardsPointerAtStart, double offsetTowardsPointerAtEnd, double time)
        {
            DistanceOffsetTowardsPointer = currentOffsetTowardsPointer;
            FirstPointOffset = new(0, 0);
            this.currentOffsetTowardsPointer = offsetTowardsPointerAtStart;
            this.offsetTowardsPointerAtEnd = offsetTowardsPointerAtEnd;
            this.offsetStep = (offsetTowardsPointerAtEnd - offsetTowardsPointerAtStart) / time;
        }

        public Vector2D FirstPointOffset { protected get; set; }
        public double DistanceOffsetTowardsPointer { private get => currentOffsetTowardsPointer; set => currentOffsetTowardsPointer = value; }
        public abstract Vector2D GetFacingPoint(DrawingArgs args);
        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            tickOffset(animationArgs.Delta);
            Vector2D firstPoint = animationArgs.PositionOnScreen + FirstPointOffset;
            Vector2D secondpoint = GetFacingPoint(animationArgs);
            if (secondpoint.Length < 0.0000000001) secondpoint = lastSecondPoint;
            else lastSecondPoint = secondpoint;
            var distanceVector = secondpoint - firstPoint;
            float angle = MathF.Atan2((float)distanceVector.Y, (float)distanceVector.X) + MathF.PI / 2;
            return Matrix3x2.CreateTranslation(0, (float)-currentOffsetTowardsPointer) *
                Matrix3x2.CreateRotation(angle); ;
        }
        private void tickOffset(double delta) {
            if (Math.Abs(offsetStep) < 0.000000001) return;
            currentOffsetTowardsPointer += offsetStep * delta;
            if (offsetStep < 0)
            {
                if (currentOffsetTowardsPointer < offsetTowardsPointerAtEnd)
                    currentOffsetTowardsPointer = offsetTowardsPointerAtEnd;
            }
            else 
            {
                if (currentOffsetTowardsPointer > offsetTowardsPointerAtEnd)
                    currentOffsetTowardsPointer = offsetTowardsPointerAtEnd;
            }
            
        }
    }
}
