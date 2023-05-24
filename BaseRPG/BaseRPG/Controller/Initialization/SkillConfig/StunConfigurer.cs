using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Effects.DamagingStun;
using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization.SkillConfig
{
    public class StunConfigurer : ISkillManagerConfigurer
    {
        public void Configure(SkillManager.Builder builder, GameConfiguration config)
        {
            builder.WithSkill(new EffectCreatingSkill<TargetedEffectParams>("stun", new DamagingStunEffectFactory(config.Hero, 5)));
        }
    }
}
