using BaseRPG.Controller.Input;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Collision.Attacks;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Factory;
using BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Charge;
using BaseRPG.View.Animation.Factory.LightAttackAnimation;
using BaseRPG.View.EntityView;
using BaseRPG.View.EntityView.Factory.AttackViewFactory;
using BaseRPG.View.Interfaces;
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
    internal class WandViewFactory : IItemViewFactory
    {
        private readonly IImageProvider imageProvider;
        private readonly IPositionProvider globalMousePositionObserver;
        private readonly IViewManager viewManager;
        private readonly InputHandler inputHandler;
        private Controller.Controller controller;
        private Weapon weapon;

        public WandViewFactory(ItemViewCreationParams viewCreationParams, IViewManager viewManager, InputHandler inputHandler)
        {
            this.imageProvider = viewCreationParams.ImageProvider;
            this.globalMousePositionObserver = viewCreationParams.GlobalMousePositionObserver;
            this.controller = viewCreationParams.Controller;
            this.weapon = viewCreationParams.Weapon;
            this.viewManager = viewManager;
            this.inputHandler = inputHandler;
        }

        public Dictionary<string, IDrawable> Create()
        {
            var image = @"Assets\image\weapons\wand-outlined.png";
            var creationParams = new WeaponViewCreationParams
            {
                Image = new(image, imageProvider),
                Weapon = weapon,
            };
            var builder = new WeaponViewBuilder(creationParams);
            var factory = new NormalWandAnimationFactory(imageProvider, controller, builder.Weapon, globalMousePositionObserver);
            var chargefactory = new WandChargeAnimationFactory(viewManager, inputHandler, builder.Weapon);
            return builder
                .AttackCreatedCallback((s, v) => controller.AddVisible(new(s, v)))
                .LightAttackViewFactory(new WandAttackViewFactory(imageProvider))
                .LightAttackShapeFactory(new WandAttackShapeFactory(weapon))
                .LightAttackAnimationFactory(factory)

                .HeavyAttackChargeAnimationFactory(chargefactory)
                .HeavyAttackShapeFactory(new WandAttackShapeFactory(weapon))
                .HeavyAttackStrikeAnimationFactory(new EmptyAnimationFactory())
                .HeavyAttackViewFactory(new WandAttackViewFactory(imageProvider))
                
                .CreateWeapon();
        }
    }
}
