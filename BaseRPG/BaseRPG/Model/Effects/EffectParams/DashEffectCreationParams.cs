using BaseRPG.Model.Interfaces.Effect;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects.EffectParams
{
    public class DashEffectCreationParams:TargetedEffectParams
    {
        public IMovementUnit Direction { get; init; }
        
        public DashEffectCreationParams(IMovementUnit direction,Unit target) :base(target)
        {
            Direction = direction;
        }
    }
}
