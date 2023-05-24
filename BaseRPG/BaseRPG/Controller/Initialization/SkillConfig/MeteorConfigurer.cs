using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Collision.Attacks;
using BaseRPG.View.EntityView;
using BaseRPG.View.EntityView.Factory;
using BaseRPG.View.EntityView.Factory.AttackViewFactory;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization.SkillConfig
{
    public class MeteorConfigurer:ISkillManagerConfigurer
    {
        IMovementManager movementManager;
        private readonly Controller controller;

        public MeteorConfigurer(IMovementManager movementManager, Controller controller)
        {
            this.movementManager = movementManager;
            this.controller = controller;
        }

        public void Configure(SkillManager.Builder builder, GameConfiguration config)
        {
            var damagePerSecond = 100;
            var lifetime = 0.5;
            var attackablilityService =
                new AttackabilityService.Builder()
                .AllowIf((attacker, attacked) => attacked == attacker)
                .CreateByDefaultMapping();

            var strategy = new DamagePerSecondAttackStrategy(damagePerSecond);
            var attackBuilder = new AttackBuilder(strategy, lifetime, int.MaxValue)
                    .World(config.CurrentWorld)
                    .Attacker(config.Hero)
                    .CanHitSameTarget(true)
                    .AttackabilityService(attackablilityService);
            var skill = new AttackCreatingSkill(
                name:                   "meteor",
                attackBuilder:          attackBuilder,
                attackCreationCallback: a => addAttack(a, config.ImageProvider)
                );
            builder.WithSkill(skill);
        }
        private void addAttack(Attack a, IImageProvider imageProvider) {
            var view = new MeteorAttackViewFactory(a,imageProvider).Create();
            var shape = new AttackShapeBuilder(a)
                .PolygonShape(Polygon.CircleVertices(new(0, 0), 300, 50))
                .OwnerPosition(movementManager.Position)
                .Rotated(false)
                .Create();

            controller.AddVisibleInstantly(new(shape,view));
        }
    }
}
