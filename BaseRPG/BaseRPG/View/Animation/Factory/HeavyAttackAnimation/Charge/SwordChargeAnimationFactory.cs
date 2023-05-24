using BaseRPG.Controller.Input;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.View.Animation.Attack;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Charge
{
    public class SwordChargeAnimationFactory : IAttackAnimationFactory
    {
        private ViewManager viewManager;
        private InputHandler inputHandler;
        private Weapon weapon;
        public static Angle ChargeRange => Angle.FromDegrees(130);
        public SwordChargeAnimationFactory(ViewManager viewManager, InputHandler inputHandler, Weapon weapon)
        {
            this.viewManager = viewManager;
            this.inputHandler = inputHandler;
            this.weapon = weapon;
        }

        public ImageSequenceAnimation CreateImageSequence(IAttackFactory attackFactory)
        {
            return null;
        }

        public TransformationAnimation2D CreateTransformation(IAttackFactory attackFactory)
        {
            var initialAngle =
                (inputHandler.MousePosition
                - viewManager.CalculatePositionOnScreen(weapon.Owner.Position))
                .SignedAngleTo(new(1, 0), true);
            var startingAngle = initialAngle;
            //new SwordChargeAnimation(Angle.FromDegrees(200), 1)
            var swordSwingAnimation = new SwordChargeAnimation(startingAngle, startingAngle + ChargeRange, 1);
            return swordSwingAnimation;
        }
    }
}
