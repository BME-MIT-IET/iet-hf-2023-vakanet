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

namespace BaseRPG.View.Animation
{
    public class EmptyAnimation : Interfaces.TransformationAnimation2D
    {
        public override event Action<TransformationAnimation2D> OnAnimationCompleted;

        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            return Matrix3x2.Identity;
        }
    }
}
