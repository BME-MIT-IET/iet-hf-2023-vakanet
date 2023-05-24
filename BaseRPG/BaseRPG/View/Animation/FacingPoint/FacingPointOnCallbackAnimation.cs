using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.FacingPoint
{
    public class FacingPointOnCallbackAnimation : FacingPointAnimationBase
    {
        private readonly IPositionProvider positionProvider;

        public FacingPointOnCallbackAnimation(double distanceOffsetTowardsPointer, IPositionProvider positionProvider) : base(distanceOffsetTowardsPointer)
        {
            this.positionProvider = positionProvider;
        }

        public override Vector2D GetFacingPoint(DrawingArgs args)
        {
            var pos = positionProvider.Position;
            return pos;
        }
    }
}
