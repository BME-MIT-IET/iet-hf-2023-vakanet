using BaseRPG.Controller.Input;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Collision.Attacks;
using BaseRPG.View.Animation.Factory;
using BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Charge;
using BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Strike;
using BaseRPG.View.Animation.Factory.LightAttackAnimation;
using BaseRPG.View.EntityView;
using BaseRPG.View.EntityView.Factory.AttackViewFactory;
using BaseRPG.View.EntityView.Factory.AttackViewFactory.Sword;
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
    public class SwordViewFactory : IItemViewFactory
    {
        private readonly IImageProvider imageProvider;
        private readonly InputHandler inputHandler;
        private Controller.Controller controller;
        private Weapon weapon;

        public SwordViewFactory(ItemViewCreationParams viewCreationParams, InputHandler inputHandler)
        {
            this.imageProvider = viewCreationParams.ImageProvider;
            this.controller = viewCreationParams.Controller;
            this.weapon = viewCreationParams.Weapon;
            this.inputHandler = inputHandler;
        }


        public Dictionary<string, IDrawable> Create()
        {
            var image = @"Assets\image\weapons\normal-sword-outlined.png";
            var creationParams = new WeaponViewCreationParams
            {
                Image = new(image, imageProvider),
                Weapon = weapon,
            };
            var builder = new WeaponViewBuilder(creationParams);
            var lightFactoy = new NormalSwordLightAnimationFactory(controller.ViewManager, inputHandler, weapon,controller);
            var heavyStrikeFactoy = new SwordHeavyStrikeAnimationFactory(controller, builder.Weapon,controller.ViewManager);
            var heavyChargeFactoy = new SwordChargeAnimationFactory(controller.ViewManager, inputHandler, weapon);
            //new SwordChargeAnimation(Angle.FromDegrees(200), 1)
            return builder
                .AttackCreatedCallback((s, v) => controller.AddVisible(new(s, v)))
                .LightAttackViewFactory(new LightSwordAttackViewFactory(imageProvider))
                .LightAttackAnimationFactory(lightFactoy)
                .LightAttackShapeFactory(new LightSwordAttackShapeFactory(weapon))
                
                .HeavyAttackStrikeAnimationFactory(heavyStrikeFactoy)
                .HeavyAttackChargeAnimationFactory(heavyChargeFactoy)
                .HeavyAttackViewFactory(new HeavySwordAttackViewFactory(imageProvider))
                .HeavyAttackShapeFactory(new HeavySwordAttackShapeFactory(weapon))
                .CreateWeapon();
        }
    }
}
