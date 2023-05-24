using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects.EffectParams
{
    public class TargetedEffectParams
    {
        public Unit Target { get; }

        public TargetedEffectParams(Unit target)
        {
            Target = target;
        }
    }

}
