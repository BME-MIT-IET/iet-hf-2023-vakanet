using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Factory.LightAttackAnimation
{
    public class NormalBowAnimationFactory : IAttackAnimationFactory
    {
        private AnimationProvider animationProvider;
        private IImageProvider imageProvider;
        private Controller.Controller controller;
        private Weapon weapon;
        private readonly IPositionProvider globalmousePositionProvider;

        public NormalBowAnimationFactory(AnimationProvider animationProvider, IImageProvider imageProvider, Controller.Controller controller, Weapon weapon, IPositionProvider globalmousePositionProvider)
        {
            this.animationProvider = animationProvider;
            this.imageProvider = imageProvider;
            this.controller = controller;
            this.weapon = weapon;
            this.globalmousePositionProvider = globalmousePositionProvider;
        }

        public ImageSequenceAnimation CreateImageSequence(IAttackFactory attackFactory)
        {
            var bowAttackAnimation = ImageSequenceAnimation
                .WithTimeFrame(imageProvider, animationProvider.Get("bow-attack"), 0.2);
            bowAttackAnimation.OnAnimationCompleted += a => addAttack(attackFactory);

            return bowAttackAnimation;
        }

        public TransformationAnimation2D CreateTransformation(IAttackFactory attackFactory)
        {
            return null;
        }
        private void addAttack(IAttackFactory attackFactory) 
        {
            var ownerToMouseDirection = (globalmousePositionProvider.Position - PositionUnit2D.ToVector2D(weapon.Owner.Position)).Normalize() * 100;
            controller.QueueAction(
                () => attackFactory
                    .CreateLight(
                        new PositionUnit2D(ownerToMouseDirection + PositionUnit2D.ToVector2D(weapon.Owner.Position)),
                        new MovementUnit2D(ownerToMouseDirection)
                    )
                );
        }
    }
}
