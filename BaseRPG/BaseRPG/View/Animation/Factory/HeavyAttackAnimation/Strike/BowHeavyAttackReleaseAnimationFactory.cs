using BaseRPG.Controller.Input;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation.FacingPoint;
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

namespace BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Strike
{
    internal class BowHeavyAttackReleaseAnimationFactory : IAttackAnimationFactory
    {
        private readonly IImageProvider imageProvider;
        private readonly AnimationProvider animationProvider;
        private double heavyAttackReleaseSeconds = 0.2;
        private Vector2D mousePositionOnScreen;
        private Controller.Controller controller;
        private Weapon weapon;
        private IPositionProvider globalmousePositionProvider;

        public Vector2D MousePositionOnScreen { set => mousePositionOnScreen = value; }

        public BowHeavyAttackReleaseAnimationFactory(AnimationProvider animationProvider, IImageProvider imageProvider, Controller.Controller controller, Weapon weapon, IPositionProvider globalmousePositionProvider)
        {
            this.animationProvider = animationProvider;
            this.imageProvider = imageProvider;
            this.controller = controller;
            this.weapon = weapon;
            this.globalmousePositionProvider = globalmousePositionProvider;
        }

        public ImageSequenceAnimation CreateImageSequence(IAttackFactory attackFactory)
        {
            var result = ImageSequenceAnimation.WithTimeFrameHoldLastItem(imageProvider,animationProvider.Get("bow-heavy-attack-release"), heavyAttackReleaseSeconds);
            addAttack(attackFactory, result);
            return result;
        }

        public TransformationAnimation2D CreateTransformation(IAttackFactory attackFactory)
        {
            var facingPointAnimation = new FacingPointAnimation(App.IMAGE_SCALE * 25, heavyAttackReleaseSeconds);
            facingPointAnimation.Point = mousePositionOnScreen;
            return facingPointAnimation;
        }

        //TODO: should not be in the view layer
        private void addAttack(IAttackFactory attackFactory, ImageSequenceAnimation animation)
        {
            var globalMousePosition = controller.ViewManager.CameraPosition + mousePositionOnScreen;
            var ownerToMouseDirection = (globalMousePosition - PositionUnit2D.ToVector2D(weapon.Owner.Position)).Normalize() * 100;
            controller.QueueAction(
                () => attackFactory
                .CreateHeavy(
                    new PositionUnit2D(ownerToMouseDirection + PositionUnit2D.ToVector2D(weapon.Owner.Position)),
                    new MovementUnit2D(ownerToMouseDirection))
                );
        }
    }
}
