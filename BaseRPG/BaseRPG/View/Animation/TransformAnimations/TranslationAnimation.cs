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
    public class TranslationAnimation : TransformationAnimation2D
    {
        private float time;
        private float originalTime;
        private readonly Vector2D startPosition;
        private readonly Vector2D endPosition;
        private Vector2D velocity;
        public override event Action<TransformationAnimation2D> OnAnimationCompleted;
        public TranslationAnimation(float time, Vector2D startPosition, Vector2D endPosition)
        {
            originalTime = time;
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.time = time;
            velocity = (endPosition - startPosition);
        }

        protected override Matrix3x2 OnGetImage(DrawingArgs animationArgs)
        {
            time -= (float)animationArgs.Delta;
            var timePassedRelativeToMax = animationArgs.Delta / originalTime;

            var result =  Matrix3x2.CreateTranslation(
                (float)(velocity.X *(time/originalTime)),
                (float)(velocity.Y * (time / originalTime)));
            if (time <= 0.00001) { 
                OnAnimationCompleted?.Invoke(this);
            }
            return result;
        }
    }
}
