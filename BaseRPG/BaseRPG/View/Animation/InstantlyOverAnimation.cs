using BaseRPG.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    internal class InstantlyOverAnimation :EmptyAnimation
    {
        public override event Action<TransformationAnimation2D> OnAnimationCompleted;
        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            try
            {
                return base.OnGetImage(animationArgs);
            }
            finally 
            {
                OnAnimationCompleted?.Invoke(this);
            }
            
        }
    }
}
