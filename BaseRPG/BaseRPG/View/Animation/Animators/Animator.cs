using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Animators
{
    public abstract class Animator
    {
        public abstract Tuple<double, double> Size { get; }
        public void Start(TransformationAnimation2D transformationAnimation, ImageSequenceAnimation imageSequenceAnimation)
        {
            if (transformationAnimation != null) Start(transformationAnimation);
            if (imageSequenceAnimation != null) Start(imageSequenceAnimation);
        }
        public abstract void Start(TransformationAnimation2D animation);
        public abstract void Start(ImageSequenceAnimation animation);

        public void ResetAll()
        {
            ResetImageSequence();
            ResetTransformation();
        }
        public abstract void ResetTransformation();
        public abstract void ResetImageSequence();
        public abstract void Animate(DrawingArgs animationArgs);
        public abstract void Freeze();

    }
}
