using BaseRPG.Controller.Input;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Collision.Attacks;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Factory;
using BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Charge;
using BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Strike;
using BaseRPG.View.Animation.Factory.LightAttackAnimation;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.EntityView;
using BaseRPG.View.EntityView.Factory.AttackViewFactory;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces.Factory;
using BaseRPG.View.Interfaces.Providers;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.ItemView.Factory
{
    
    internal class BowViewFactory : IItemViewFactory
    {
        private readonly IImageProvider imageProvider;
        private Controller.Controller controller;
        private AnimationProvider animationProvider;
        private readonly InputHandler inputHandler;
        private IPositionProvider globalMousePositionObserver;
        private Weapon weapon;

        public BowViewFactory(ItemViewCreationParams creationParams, AnimationProvider animationProvider, Controller.Input.InputHandler inputHandler)
        {
            this.imageProvider = creationParams.ImageProvider;
            this.controller = creationParams.Controller;
            this.globalMousePositionObserver = creationParams.GlobalMousePositionObserver;
            this.animationProvider = animationProvider;
            this.inputHandler = inputHandler;
            this.weapon = creationParams.Weapon;
        }

        public Dictionary<string, IDrawable> Create()
        {
            var image = @"Assets\image\weapons\normal-bow\normal-bow-outlined.png";
            var creationParams = new WeaponViewCreationParams
            {
                Image = new(image,imageProvider),
                Weapon = weapon,
            };

            var builder = new WeaponViewBuilder(creationParams);
            var lightAttackFactory = new NormalBowAnimationFactory(animationProvider, imageProvider, controller, builder.Weapon, globalMousePositionObserver);
            var heavyAttackReleaseFactory = new BowHeavyAttackReleaseAnimationFactory(animationProvider, imageProvider, controller, builder.Weapon, globalMousePositionObserver);
            var chargeAnimationFactory = new BowChargeAnimationFactory(imageProvider, animationProvider, weapon, inputHandler);
            return builder
                .AttackCreatedCallback((s, v) => controller.AddVisible(new(s, v)))
                .LightAttackViewFactory(new BowAttackViewFactory(imageProvider))
                .LightAttackShapeFactory(new ArrowShapeFactory(weapon))
                .LightAttackAnimationFactory(lightAttackFactory)

                .HeavyAttackChargeAnimationFactory(chargeAnimationFactory)
                .HeavyAttackViewFactory(new BowAttackViewFactory(imageProvider))
                .HeavyAttackShapeFactory(new ArrowShapeFactory(weapon))
                .HeavyAttackStrikeAnimationFactory(heavyAttackReleaseFactory)
                .CreateWeapon();
        }
    }
}
