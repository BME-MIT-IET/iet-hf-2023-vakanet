using BaseRPG.Controller.Utility;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace BaseRPG.View.Animation.Animators
{
    public class CustomAnimator : Animator
    {

        private AnimationLifeCycle<TransformationAnimation2D> transformationAnimation;
        private AnimationLifeCycle<ImageSequenceAnimation> sequenceAnimation;
        private Transform2DEffect lastFrame;
        private bool freezed = false;
        private readonly bool cancelAnimations;

        public CustomAnimator(
            TransformationAnimation2D defaultTransformAnimation,
            ImageSequenceAnimation defaultImageSequenceAnimation,
            bool cancelAnimations = false)
        {
            transformationAnimation = new(defaultTransformAnimation);
            sequenceAnimation = new(defaultImageSequenceAnimation);
            this.cancelAnimations = cancelAnimations;
            Start(defaultTransformAnimation);
            Start(defaultImageSequenceAnimation);
        }
        public override void Animate(DrawingArgs animationArgs)
        {
            transformationAnimation.ResetIfQueued();
            sequenceAnimation.ResetIfQueued();

            if (!freezed) 
            { 
                lastFrame = new Transform2DEffect
                {
                    Source = sequenceAnimation.CurrentAnimation.CalculateImage(animationArgs.Delta),
                    TransformMatrix = transformationAnimation.CurrentAnimation.GetImage(animationArgs)
                };
            }
            

            animationArgs.DrawingSession.DrawImage(lastFrame
                , (float)animationArgs.PositionOnScreen.X
                , (float)animationArgs.PositionOnScreen.Y);
        }
        public override Tuple<double, double> Size
        {
            get
            {
                var scaleFactor = transformationAnimation.CurrentAnimation.LastTransformation.GetDeterminant();
                var currentImageSize = sequenceAnimation.CurrentAnimation.CurrentImageSize;
                return new(
                    currentImageSize.Item1 * scaleFactor,
                    currentImageSize.Item2 * scaleFactor);
            }
        }

        public TransformationAnimation2D TransformationAnimation { get => transformationAnimation.CurrentAnimation; }
        public ImageSequenceAnimation ImageSequenceAnimation { get => sequenceAnimation.CurrentAnimation; }

        public override void Start(TransformationAnimation2D animation) =>
            transformationAnimation.Start(animation, cancelAnimations);

        public override void ResetTransformation() =>
            transformationAnimation.Reset();

        public override void Start(ImageSequenceAnimation animation) =>
            sequenceAnimation.Start(animation, cancelAnimations);

        public override void ResetImageSequence() =>
            sequenceAnimation.Reset();
        public override void Freeze() => freezed = true;
    }
}
