using BaseRPG.Controller.Input;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.Attack;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml;
using System;
using System.Numerics;
using Windows.UI;

namespace BaseRPG.View.ItemView
{
    public class EquippedWeaponView : BaseItemView
    {
        private Animator animator;
        private readonly IAttackAnimationFactory lightAttackAnimationFactory;
        private readonly IAttackAnimationFactory heavyAttackStrikeAnimationFactory;
        private readonly IAttackAnimationFactory heavyAttackChargeAnimationFactory;
        private Weapon observedWeapon;
        private bool isCharging = false;
        public override bool Exists => Owner.Exists && observedWeapon.Exists; 
        protected override Item ObservedItem { get { return observedWeapon; } }
        public EquippedWeaponView(
            Weapon weapon, 
            Animator animator,
            IAttackAnimationFactory lightAnimationFactory, 
            IAttackAnimationFactory heavyAttackAnimationFactory, 
            IAttackAnimationFactory heavyAttackChargeAnimationFactory)
        {
            this.observedWeapon = weapon;
            this.animator = animator;
            this.lightAttackAnimationFactory = lightAnimationFactory;
            this.heavyAttackStrikeAnimationFactory = heavyAttackAnimationFactory;
            this.heavyAttackChargeAnimationFactory = heavyAttackChargeAnimationFactory;
        }

        public override Vector2D ObservedPosition => PositionUnit2D.ToVector2D(Owner.Position);

        public Unit Owner { get => observedWeapon.Owner; }

        public IAttackAnimationFactory HeavyAttackChargeAnimationFactory => heavyAttackChargeAnimationFactory;
        public IAttackAnimationFactory HeavyAttackStrikeAnimationFactory => heavyAttackStrikeAnimationFactory;

        public override void OnRender(DrawingArgs drawingArgs)
        {
            animator.Animate(drawingArgs);
        }

        public void StartHeavyAttackChargeAnimation() {
            isCharging = true;
            animator.ResetAll();
            animator.Start(
                HeavyAttackChargeAnimationFactory.CreateTransformation(null),
                HeavyAttackChargeAnimationFactory.CreateImageSequence(null));

        }

        public void StartHeavyAttackStrikeAnimation(IAttackFactory attackFactory)
        {
            isCharging = false;
            animator.ResetAll();
            animator.Start(
                heavyAttackStrikeAnimationFactory.CreateTransformation(attackFactory),
                heavyAttackStrikeAnimationFactory.CreateImageSequence(attackFactory));
        }
        public void StartLightAttackAnimation(IAttackFactory attackFactory) {

            animator.ResetAll();
            animator.Start(
                lightAttackAnimationFactory.CreateTransformation(attackFactory),
                lightAttackAnimationFactory.CreateImageSequence(attackFactory));
        }

        internal void CancelHeavyChargeAnimation()
        {
            if (isCharging) { 
                animator.ResetAll();
                isCharging = false;
            }
            
        }
    }
}
