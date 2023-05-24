using BaseRPG.Controller.Input;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation.Attack;
using BaseRPG.View.Animation.Attack.SwordSwing;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Factory.LightAttackAnimation
{
    public class NormalSwordLightAnimationFactory : IAttackAnimationFactory
    {
        private readonly Controller.Controller controller;
        private Weapon weapon;
        private ViewManager viewManager;
        private InputHandler inputHandler;
        public NormalSwordLightAnimationFactory(
            ViewManager viewManager,
            InputHandler inputHandler, 
            Weapon weapon,
            Controller.Controller controller)
        {
            this.viewManager = viewManager;
            this.inputHandler = inputHandler;
            this.weapon = weapon;
            this.controller = controller;
        }

        public ImageSequenceAnimation CreateImageSequence(IAttackFactory attackFactory)
        {
            return null;
        }

        public TransformationAnimation2D CreateTransformation(IAttackFactory attackFactory)
        {
            var startingAngle = viewManager.CalculateAngle(weapon.Owner.Position,inputHandler.MousePosition);
            var angleRange = Angle.FromDegrees(120);
            var seconds = 0.3;
            LightSwordSwingAnimation swordSwingAnimation =
                    new LightSwordSwingAnimation(
                        startingAngle, 
                        new LightAttackMovementAngleStrategy(angleRange, seconds),
                        0.05
                        );
            swordSwingAnimation.OnAnimationAlmostEnding +=
                a => addAttack(attackFactory, swordSwingAnimation.StartingAngle);
               
            return swordSwingAnimation;
        }

        private void addAttack(IAttackFactory attackFactory, Angle  startingAngle) 
        {
            var position = new PhysicsFactory2D().CreatePosition(
                    Vector2D.FromPolar(25 * App.IMAGE_SCALE, startingAngle)
                    + PositionUnit2D.ToVector2D(weapon.Owner.Position));
            var movement = new MovementUnit2D(0, 0);
            controller.QueueAction(() => attackFactory.CreateLight(position, movement));
        }
    }
}
