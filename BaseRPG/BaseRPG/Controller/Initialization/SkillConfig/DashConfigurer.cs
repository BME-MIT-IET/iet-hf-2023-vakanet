using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Effects;
using BaseRPG.Model.Effects.Dash;
using BaseRPG.Model.Effects.EffectParams;
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
    internal class DashConfigurer : ISkillManagerConfigurer
    {
        IMovementManager movementManager;
        private double time = 0.2;
        private readonly Controller controller;

        public DashConfigurer(IMovementManager movementManager, Controller controller)
        {
            this.movementManager = movementManager;
            this.controller = controller;
        }

        public void Configure(SkillManager.Builder builder, GameConfiguration config)
        {
         
            var distance = 250;
            
            builder.WithSkill(
                new EffectCreatingSkill<DashEffectCreationParams>(
                    "dash", new DashEffectFactory(time, distance, e => createView(e,config)))
                );
        }
        private void createView(Effect effect ,GameConfiguration config) {
            var layer = -100;
            var param = new EffectViewCreationParams
            {
                Effect = effect,
                MovementManager = movementManager,
                ImageProvider = config.ImageProvider,
                AnimationProvider = config.AnimationProvider,
            };
            controller.AddView(new DashEffectViewFactory(param,time).Create(), layer);
        }
    }
}
