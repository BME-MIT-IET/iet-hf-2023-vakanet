using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Interfaces.Effect;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects.Invincibility
{
    internal class InvincibilityEffectFactory : IEffectFactory<TargetedEffectParams>
    {
        private double secondsDuration;

        public InvincibilityEffectFactory(double secondsDuration, Action<Effect> onCreatedCallback = null)
        {
            this.secondsDuration = secondsDuration;
            if(onCreatedCallback != null)
                EffectCreated += onCreatedCallback;
        }

        public event Action<Effect> EffectCreated;

        public Effect CreateEffect(TargetedEffectParams skillCastParams)
        {
            InvincibilityEffect invincibilityEffect = new InvincibilityEffect(skillCastParams.Target, secondsDuration);
            EffectCreated?.Invoke(invincibilityEffect);
            return invincibilityEffect;
        }
    }
}
