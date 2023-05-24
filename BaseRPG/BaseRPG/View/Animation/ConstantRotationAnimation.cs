using BaseRPG.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    internal class ConstantRotationAnimation : TransformationAnimation2D
    {
        private float rotationRadians;

        public ConstantRotationAnimation(float rotationRadians)
        {
            this.rotationRadians = rotationRadians;
        }

        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            return Matrix3x2.CreateRotation(rotationRadians);  
        }
    }
}
