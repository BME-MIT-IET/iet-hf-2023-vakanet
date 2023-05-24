using BaseRPG.Model.Effects;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System.Collections.Generic;

namespace BaseRPG
{
    public class EffectView : IDrawable
    {
        private Effect effect;
        private readonly IMovementManager movementManager;
        private Animator animator;
        public EffectView(Effect effect, IMovementManager movementManager, Animator animator)
        {
            this.effect = effect;
            this.movementManager = movementManager;
            this.animator = animator;
        }

        public bool Exists => effect.Exists;

        public Vector2D ObservedPosition => new(movementManager.Position.Values[0], movementManager.Position.Values[1]);
        public void OnRender(DrawingArgs drawingArgs)
        {
            animator.Animate(drawingArgs);
        }

        public class Builder {
            private Effect effect;
            private readonly IMovementManager movementManager;
            private readonly ImageSequenceAnimation imageSequenceAnimation;
            private TransformationAnimation2D transformationAnimation;
            public Builder(Effect effect, IMovementManager movementManager, ImageSequenceAnimation imageSequenceAnimation)
            {
                this.effect = effect;
                this.movementManager = movementManager;
                this.imageSequenceAnimation = imageSequenceAnimation;
            }
            public Builder DefaultTransformationAnimation(TransformationAnimation2D animation) {
                this.transformationAnimation = animation;
                return this;
            }
            public EffectView Build() {
                var animator = new CustomAnimator(
                    transformationAnimation, imageSequenceAnimation
                    );
                return new EffectView(effect,movementManager, animator);
            }
        }
    }
}