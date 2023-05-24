using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Effect;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects.DamagingStun
{
    internal class DamagingStunEffectFactory : IEffectFactory<TargetedEffectParams>
    {
        private Unit caster;
        private readonly double durationSeconds;

        public DamagingStunEffectFactory(Unit caster, double durationSeconds)
        {
            this.caster = caster;
            this.durationSeconds = durationSeconds;
        }

        public event Action<Effect> EffectCreated;

        public Effect CreateEffect(TargetedEffectParams effectCreationParams)
        {
            
            var result = new DamagingStunEffect(caster, effectCreationParams.Target, durationSeconds);
            EffectCreated?.Invoke(result);
            return result;
        }
    }
}
