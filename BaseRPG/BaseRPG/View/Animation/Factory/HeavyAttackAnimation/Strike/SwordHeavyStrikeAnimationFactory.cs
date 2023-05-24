using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation.Attack;
using BaseRPG.View.Animation.Attack.SwordSwing;
using BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Charge;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Camera;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Strike
{
    internal class SwordHeavyStrikeAnimationFactory : IAttackAnimationFactory
    {
        private readonly Controller.Controller controller;
        private Weapon weapon;
        private readonly ViewManager viewManager;
        private Vector2D mousePositionOnScreen;

        public Vector2D MousePositionOnScreen {  set => mousePositionOnScreen = value; }
        public static double HEAVY_SWORD_SWING_SECONDS => 0.1;
        public SwordHeavyStrikeAnimationFactory(Controller.Controller controller, Weapon weapon,ViewManager viewManager)
        {
            this.controller = controller;
            this.weapon = weapon;
            this.viewManager = viewManager;
        }
        public ImageSequenceAnimation CreateImageSequence(IAttackFactory attackFactory)
        {
            return null;
        }

        public TransformationAnimation2D CreateTransformation(IAttackFactory attackFactory)
        {
            var angleRange = -2*SwordChargeAnimationFactory.ChargeRange;
            var initialAngle = viewManager.CalculateAngle(weapon.Owner.Position,mousePositionOnScreen);
            var startingAngle = initialAngle + angleRange/2;

            var swordSwingAnimation = new SwordSwingAnimation(startingAngle, new LinearSwingStrategy(HEAVY_SWORD_SWING_SECONDS, angleRange),0.1);
            //swordSwingAnimation.OnAnimationAlmostEnding += a => addAttack(attackBuilder,swordSwingAnimation);
            addAttack(attackFactory, swordSwingAnimation);
            return swordSwingAnimation;
        }

        //TODO: should not be in the view layer
        private void addAttack(IAttackFactory attackFactory, SwordSwingAnimation swordSwingAnimation) {
            var ownerPosition = PositionUnit2D.ToVector2D(weapon.Owner.Position);
            var averageAngle = (swordSwingAnimation.StartingAngle - swordSwingAnimation.AngleRange/2) / 1;
            var offset = Vector2D.FromPolar(15 * App.IMAGE_SCALE, averageAngle);
            var positionAsVector = ownerPosition + offset;
            var position = new PhysicsFactory2D().CreatePosition(positionAsVector);
            controller.QueueAction(() => attackFactory.CreateHeavy(position,new MovementUnit2D(0,0)));
        }
    }
}
