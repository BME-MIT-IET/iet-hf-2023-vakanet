using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Physics.TwoDimensional.Collision.Attacks;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BaseRPG.View.EntityView
{
    public class AttackView:IDrawable
    {
        
        private Attack attack;
        private Animator animator;
        private readonly double secondsVisibleAfterDestroyed;
        private bool exists = true;
        private AttackView(Attack attack, Animator animator, double initialRotation, double secondsVisibleAfterDestroyed = 0)
        {
            this.attack = attack;
            this.animator = animator;
            this.secondsVisibleAfterDestroyed = secondsVisibleAfterDestroyed;
            //imageRenderer.SetImageRotation(initialRotation + Math.PI/2);
        }

        public Vector2D ObservedPosition => new(attack.Position.Values[0], attack.Position.Values[1]);

        public bool Exists => exists;

        public void OnRender(DrawingArgs drawingArgs)
        {
            if (!attack.Exists)
            {
                if (secondsVisibleAfterDestroyed > 0.00001)
                {
                    var timer = new Timer(secondsVisibleAfterDestroyed * 1000);
                    timer.Elapsed += (a, b) => exists = false;
                    timer.AutoReset = false;
                    timer.Start();
                }
                else {
                    exists = false;
                }
            }
            //var temp = imageRenderer.ImageRotation;
            //imageRenderer.Render(drawingArgs);
            animator.Animate(drawingArgs);
        }
        public class Builder {

            private string imageName;
            private IImageProvider imageProvider;
            private double secondsVisibleAfterDestroyed;
            private Attack2DBuilderHelper helper;
            private ImageSequenceAnimation imageSequenceAnimation;
            public Builder()
            {
                this.helper = new(null);
            }
            public Builder(string imageName,Attack attack = null)
            {

                this.helper = new(attack);
                this.imageName = imageName;
                
            }
            //public Builder(string imageName, Attack attack = null)
            //{
            //    this.imageName = imageName;
            //    this.helper = new(attack);
            //}
            public Builder Attack(Attack attack)
            {
                helper.Attack = attack;
                return this;
            }
            public Builder WithImageSequenceAnimation(ImageSequenceAnimation imageSequenceAnimation)
            {
                this.imageSequenceAnimation = imageSequenceAnimation;
                return this;
            }
            public Builder OwnerPosition(IPositionUnit ownerPosition)
            {
                helper.OwnerPosition = ownerPosition;
                return this;
            }
            public Builder ImageProvider(IImageProvider imageProvider)
            {
                this.imageProvider = imageProvider;
                imageSequenceAnimation = ImageSequenceAnimation.SingleImage(imageProvider, imageName);

                return this;
            }
            public Builder Rotated(bool rotated)
            {
                helper.Rotated = rotated;
                return this;
            }
            public Builder SecondsVisibleAfterDestroyed(double secondsVisibleAfterDestroyed) {
                this.secondsVisibleAfterDestroyed = secondsVisibleAfterDestroyed;
                return this;
            }
            public AttackView Create()
            {
                //if (imageName == null) throw new ArgumentNullException("imageName");
                var initialRotation = helper.calculateInitialRotaion();
                DefaultImageRenderer attackImageRenderer = new DefaultImageRenderer(
                        imageProvider.GetByFilename(imageName),
                        imageProvider.GetSizeByFilename(imageName)
                    );

                var animator = new CustomAnimator(
                    new ConstantRotationAnimation((float)initialRotation + (float)Math.PI/2),
                    imageSequenceAnimation);
                
                return new AttackView(helper.Attack, animator, initialRotation, secondsVisibleAfterDestroyed);
            }
        }
    }
}
