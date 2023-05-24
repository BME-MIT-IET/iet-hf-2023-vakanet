using BaseRPG.View.Animation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces
{
    public abstract class TransformationAnimation2D:IAnimation<TransformationAnimation2D>
    {
        public static EmptyAnimation Empty => new EmptyAnimation();
        private Matrix3x2 lastTransformation;
        public Matrix3x2 LastTransformation => lastTransformation;
        public virtual event Action<TransformationAnimation2D> OnAnimationCompleted;
        public Matrix3x2 GetImage(DrawingArgs animationArgs) {
            lastTransformation = OnGetImage(animationArgs);
            return lastTransformation;
        }
        protected abstract Matrix3x2 OnGetImage(DrawingArgs animationArgs);
    }
}
