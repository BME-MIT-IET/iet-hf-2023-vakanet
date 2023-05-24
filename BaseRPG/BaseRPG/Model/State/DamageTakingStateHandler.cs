using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces.State;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.State
{
    public class DamageTakingStateHandler:StateHandler<IDamageTakingState>
    {
        public DamageTakingStateHandler(IDamageTakingState baseDamageTakingState):base(baseDamageTakingState) {
        }

        public double CalculateDamage( double originalDamage) { 
            return CurrentState.CalculateDamage( originalDamage);
        }
    }
}
