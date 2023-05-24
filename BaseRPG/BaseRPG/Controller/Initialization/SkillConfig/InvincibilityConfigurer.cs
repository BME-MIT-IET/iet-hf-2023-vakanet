using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Effects;
using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Effects.Invincibility;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Skills;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Effects.Factory;
using BaseRPG.View.Interfaces.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization.SkillConfig
{
    internal class InvincibilityConfigurer : ISkillManagerConfigurer
    {
        private IMovementManager movementManager;
        private Controller controller;

        public InvincibilityConfigurer(IMovementManager movementManager, Controller controller)
        {
            this.movementManager = movementManager;
            this.controller = controller;
        }

        public void Configure(SkillManager.Builder builder, GameConfiguration config)
        {
            var durationInSeconds = 5;
            var factory = new InvincibilityEffectFactory(durationInSeconds, e => createView(e, config));
            var skill = new EffectCreatingSkill<TargetedEffectParams>("invincibility", factory);
            builder.WithSkill(skill);
                
        }
        private void createView(Effect effect, GameConfiguration config) {

            var layer = 200;
            var param = new EffectViewCreationParams
            {
                Effect = effect,
                MovementManager = movementManager,
                ImageProvider = config.ImageProvider,
                AnimationProvider = config.AnimationProvider,
            };
            var factory = new InvincibilityEffectViewFactory(param);
            controller.AddView(factory.Create(), layer);
        }
    }
}
