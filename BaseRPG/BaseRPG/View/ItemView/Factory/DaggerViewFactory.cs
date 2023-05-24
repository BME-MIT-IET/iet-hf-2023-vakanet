using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Collision.Attacks;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Factory;
using BaseRPG.View.Animation.Factory.LightAttackAnimation;
using BaseRPG.View.EntityView;
using BaseRPG.View.EntityView.Factory.AttackViewFactory;
using BaseRPG.View.Interfaces.Factory;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.ItemView.Factory
{
    internal class DaggerViewFactory : IItemViewFactory
    {
        private readonly IImageProvider imageProvider;
        private Controller.Controller controller;
        private IPositionProvider globalMousePositionObserver;
        private Weapon weapon;

        public DaggerViewFactory(ItemViewCreationParams viewCreationParams)
        {
            this.imageProvider = viewCreationParams.ImageProvider;
            this.controller = viewCreationParams.Controller;
            this.globalMousePositionObserver = viewCreationParams.GlobalMousePositionObserver;
            this.weapon = viewCreationParams.Weapon;
        }
        public Dictionary<string, IDrawable> Create()
        {
            var image = @"Assets\image\weapons\dagger-outlined.png";
            var creationParams = new WeaponViewCreationParams
            {
                Image = new(image, imageProvider),
                Weapon = weapon,
            };
            var builder = new WeaponViewBuilder(creationParams);
            var factory = new NormalDaggerAnimationFactory(imageProvider, controller, builder.Weapon, globalMousePositionObserver);

            var attackShapeBuilder = new AttackShapeBuilder()
                    .PolygonShape(Polygon.RectangleVertices(new(0, 0), 10 * App.IMAGE_SCALE, 18 * App.IMAGE_SCALE));
            return builder
                .AttackCreatedCallback((s, v) => controller.AddVisible(new(s, v)))
                .LightAttackViewFactory(new DaggerAttackViewFactory(imageProvider))
                .LightAttackShapeFactory(new DaggerAttackShapeFactory(weapon))
                .LightAttackAnimationFactory(factory)
                .CreateWeapon();
        }
    }
}
