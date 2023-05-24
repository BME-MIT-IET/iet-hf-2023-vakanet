using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation
{
    public class AnimationView : IDrawable
    {
        private bool reset = false;
        private readonly IPositionProvider positionProvider;
        private readonly CustomAnimator defaultAnimator;

        private AnimationView(bool reset, IPositionProvider positionProvider, CustomAnimator defaultAnimator)
        {
            this.reset = reset;
            this.positionProvider = positionProvider;
            this.defaultAnimator = defaultAnimator;
        }

        public bool Exists { get; set; } = true;

        public Vector2D ObservedPosition => positionProvider.Position;

        public void OnRender(DrawingArgs drawingArgs)
        {
            defaultAnimator.Animate(drawingArgs);
        }

        public class Builder {
            private IPositionProvider positionProvider;
            private bool reset = false;
            private readonly CustomAnimator defaultAnimator;
            Action<TransformationAnimation2D> transformationAnimationOverCallback;
            public Builder(CustomAnimator defaultAnimator)
            {
                this.defaultAnimator = defaultAnimator;
            }

            public Builder WithPositionProvider(IPositionProvider positionProvider) {
                this.positionProvider = positionProvider;
                return this;
            }
            public Builder WithFixPosition(Vector2D position)
            {
                this.positionProvider = new PositionObserver(()=>position);
                return this;
            }
            public Builder WithReset {
                get { 
                    reset = true;
                    return this;
                } 
            }
            public Builder TransformationCompletedCallback(Action<TransformationAnimation2D> action)
            {
                transformationAnimationOverCallback = action;
                return this;
            }
            public AnimationView Create() {
                AnimationView animationView = new AnimationView(reset, positionProvider, defaultAnimator);
                if (!reset) {
                    defaultAnimator.ImageSequenceAnimation.OnAnimationCompleted += (a) => animationView.Exists = false;
                    defaultAnimator.TransformationAnimation.OnAnimationCompleted += (a) => 
                        animationView.Exists = false;
                    defaultAnimator.TransformationAnimation.OnAnimationCompleted += transformationAnimationOverCallback;
                }
                    
                return animationView;
            }
        }
    }
}
