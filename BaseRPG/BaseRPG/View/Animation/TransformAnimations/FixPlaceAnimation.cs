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
    public class FixPlaceAnimation : TransformationAnimation2D
    {
        private readonly Vector2D position;

        public FixPlaceAnimation(Vector2D position)
        {
            this.position = position;
        }

        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            return Matrix3x2.CreateTranslation((float)position.X, (float)position.Y);
        }
    }
}
