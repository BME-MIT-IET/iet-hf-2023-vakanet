using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.FacingPoint
{
    public class FacingMouseAnimation : FacingPointAnimationBase
    {
        public FacingMouseAnimation(double distanceOffsetTowardsPointer = 0) : base( distanceOffsetTowardsPointer)
        {
        }

        public override event Action<Interfaces.TransformationAnimation2D> OnAnimationCompleted;

        public override Vector2D GetFacingPoint(DrawingArgs args)
        {
            return args.MousePositionOnScreen;
        }
    }
}
