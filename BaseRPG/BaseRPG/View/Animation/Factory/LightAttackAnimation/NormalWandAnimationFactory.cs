using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Factory.LightAttackAnimation
{
    public class NormalWandAnimationFactory : IAttackAnimationFactory
    {
        private IImageProvider imageProvider;
        private Controller.Controller controller;
        private Weapon weapon;
        private IPositionProvider globalMousePosition;

        public NormalWandAnimationFactory(IImageProvider imageProvider, Controller.Controller controller, Weapon weapon, IPositionProvider globalMousePosition)
        {
            this.imageProvider = imageProvider;
            this.controller = controller;
            this.weapon = weapon;
            this.globalMousePosition = globalMousePosition;
        }

        public ImageSequenceAnimation CreateImageSequence(IAttackFactory attackFactory)
        {
            return ImageSequenceAnimation.SingleImage(imageProvider, @"Assets\image\weapons\wand-outlined.png");
        }

        public TransformationAnimation2D CreateTransformation(IAttackFactory attackFactory)
        {

            var ownerToMouseDirection = (globalMousePosition.Position - PositionUnit2D.ToVector2D(weapon.Owner.Position)).Normalize() * 100;
            if (attackFactory != null) { 
                controller.QueueAction(
                    () => attackFactory
                        .CreateLight(
                        new PositionUnit2D(ownerToMouseDirection + PositionUnit2D.ToVector2D(weapon.Owner.Position)),
                        new MovementUnit2D(ownerToMouseDirection)
                        )
                    );
            }
            
            return new RecoilAnimation();
        }
    }
}
