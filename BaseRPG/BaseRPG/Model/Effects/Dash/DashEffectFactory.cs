using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Interfaces.Effect;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects.Dash
{
    public class DashEffectFactory : IEffectFactory<DashEffectCreationParams>
    {
        private double timeToComplete;
        private double distance;

        public DashEffectFactory(double timeToComplete, double distance, Action<Effect> onCreated = null)
        {
            this.timeToComplete = timeToComplete;
            this.distance = distance; 
            if (onCreated != null)
                EffectCreated += onCreated;
        }

        public event Action<Effect> EffectCreated;

        public Effect CreateEffect(DashEffectCreationParams skillCastParams)
        {
            var movement = skillCastParams.Direction.WithLength(distance);
            DashEffect dashEffect = new DashEffect(skillCastParams.Target, movement, timeToComplete);
            EffectCreated?.Invoke(dashEffect);
            return dashEffect;
        }
    }
}
