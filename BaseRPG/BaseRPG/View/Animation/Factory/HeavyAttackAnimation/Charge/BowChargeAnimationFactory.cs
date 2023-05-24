using BaseRPG.Controller.Input;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Animation.TransformAnimations;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Charge
{
    internal class BowChargeAnimationFactory : IAttackAnimationFactory
    {
        private IImageProvider imageProvider;
        private AnimationProvider animationProvider;
        private readonly InputHandler inputHandler;
        private Weapon weapon;

        public BowChargeAnimationFactory(
            IImageProvider imageProvider, 
            AnimationProvider animationProvider,
            Weapon weapon, 
            InputHandler inputHandler)
        {
            this.imageProvider = imageProvider;
            this.animationProvider = animationProvider;
            this.weapon = weapon;
            this.inputHandler = inputHandler;
        }

        public ImageSequenceAnimation CreateImageSequence(IAttackFactory attackFactory)
        {
            return ImageSequenceAnimation.WithTimeFrameHoldLastItem(imageProvider, animationProvider.Get("bow-attack"), 1);
        }

        public TransformationAnimation2D CreateTransformation(IAttackFactory attackFactory)
        {
            var facingPointAnimation = new FacingPointAnimation(App.IMAGE_SCALE*25);
            facingPointAnimation.Point = inputHandler.MousePosition;
            var animations = new List<TransformationAnimation2D> {
                new VibratingAnimation(),
                facingPointAnimation
            };
            return new CompositeAnimation(animations);

        }
    }
}
