using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Collision.Attacks;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.EntityView;
using BaseRPG.View.EntityView.Factory.AttackViewFactory;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using System;
using System.Collections.Generic;

namespace BaseRPG.View.ItemView
{
    public class WeaponViewCreationParams
    {
        private DrawingImage image;
        private Weapon weapon;
        //private IAttackAnimationFactory lightAttackAnimationFactory;
        //private IAttackAnimationFactory heavyAttackAnimationFactory;

        public DrawingImage Image { get => image; init => image = value; }
        public Weapon Weapon { get => weapon; init => weapon = value; }
        //public IAttackAnimationFactory LightAttackAnimationFactory { get => lightAttackAnimationFactory; init => lightAttackAnimationFactory = value; }
        //public IAttackAnimationFactory HeavyAttackAnimationFactory { get => heavyAttackAnimationFactory; init => heavyAttackAnimationFactory = value; }
    }
    public class WeaponViewBuilder
    {


        private WeaponAttackViewFactory lightAttackViewFactory;
        private IAttackShapeFactory lightAttackShapeFactory;
        private IAttackAnimationFactory lightAttackAnimationFactory;
        private IAttackAnimationFactory heavyAttackStrikeAnimationFactory;
        private IAttackAnimationFactory heavyAttackChargeAnimationFactory;
        private WeaponAttackViewFactory heavyAttackViewFactory;
        private IAttackShapeFactory heavyAttackShapeFactory;
        private readonly DrawingImage image;
        private readonly Weapon weapon;
        public Weapon Weapon { get => weapon; }
        public WeaponViewBuilder(WeaponViewCreationParams creationParams)
        {
            image = creationParams.Image;
            weapon = creationParams.Weapon;
        }

        public WeaponViewBuilder LightAttackShapeFactory(IAttackShapeFactory factory)
        {
            lightAttackShapeFactory = factory;
            return this;
        }
        public WeaponViewBuilder LightAttackViewFactory(WeaponAttackViewFactory b)
        {
            lightAttackViewFactory = b;
            return this;
        }
        public WeaponViewBuilder LightAttackAnimationFactory(IAttackAnimationFactory factory)
        {
            lightAttackAnimationFactory = factory;
            return this;
        }
        public WeaponViewBuilder HeavyAttackShapeFactory(IAttackShapeFactory factory)
        {
            heavyAttackShapeFactory = factory;
            return this;
        }
        public WeaponViewBuilder HeavyAttackStrikeAnimationFactory(IAttackAnimationFactory factory)
        {
            heavyAttackStrikeAnimationFactory = factory;
            return this;
        }
        public WeaponViewBuilder HeavyAttackChargeAnimationFactory(IAttackAnimationFactory factory)
        {
            heavyAttackChargeAnimationFactory = factory;
            return this;
        }
        public WeaponViewBuilder HeavyAttackViewFactory(WeaponAttackViewFactory b)
        {
            heavyAttackViewFactory = b;
            return this;
        }
        public WeaponViewBuilder AttackCreatedCallback(Action<IShape2D, IDrawable> onAttackCreated)
        {
            weapon.AttackFactory.LightAttackCreated += (a) =>
                onAttackCreated(lightAttackShapeFactory.Create(a), createLightAttackView(a));
            weapon.AttackFactory.HeavyAttackCreated += (a) =>
                onAttackCreated(heavyAttackShapeFactory.Create(a), createHeavyAttackView(a));
            return this;
        }

        public Dictionary<string, IDrawable> CreateWeapon()
        {
            var animator = new CustomAnimator(
                new FacingMouseAnimation(distanceOffsetTowardsPointer: 25 * App.IMAGE_SCALE),
                ImageSequenceAnimation.SingleImage(image)
                );
            Dictionary<string, IDrawable> weaponViews = new();
            EquippedWeaponView equippedItemView =
                new EquippedWeaponView(
                    weapon,
                    animator,
                    lightAttackAnimationFactory,
                    heavyAttackStrikeAnimationFactory, 
                    heavyAttackChargeAnimationFactory
                    );

            weaponViews.Add("equipped", equippedItemView);
            weaponViews.Add("inventory", new InventoryItemView(weapon, image));
            return weaponViews;
        }

        private AttackView createLightAttackView(Attack a)
        {
            lightAttackViewFactory.CreationParams = createCreationParams(a);
            return lightAttackViewFactory.Create();
        }
        private AttackView createHeavyAttackView(Attack a)
        {

            heavyAttackViewFactory.CreationParams = createCreationParams(a);
            return heavyAttackViewFactory.Create();
        }
        private WeaponAttackCreationParams createCreationParams(Attack a) {
            return new WeaponAttackCreationParams
            {
                Rotated = true,
                OwnerPosition = weapon.Owner.Position,
                Attack = a
            };
        }
    }
}
