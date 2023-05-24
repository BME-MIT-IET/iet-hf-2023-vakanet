using BaseRPG.View.Animation.TransformAnimations;
using BaseRPG.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    internal class CompositeAnimation : TransformationAnimation2D
    {
        private IEnumerable<TransformationAnimation2D> animations;

        public CompositeAnimation(IEnumerable<TransformationAnimation2D> animations)
        {
            if (!animations.Any()) throw new ArgumentException("the animations enumeration must contain at least 1 element!");
            this.animations = animations;
        }

        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            var result = Matrix3x2.Identity;
            foreach (var a in animations)
            {
                result *= a.GetImage(animationArgs);
            }
            return result;
        }
    }
}
