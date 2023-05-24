using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.State;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Combat
{
    public class NormalDamageTakingState : IDamageTakingState
    {
        private readonly Unit owner;

        public NormalDamageTakingState(Unit owner) {
            this.owner = owner;
        }

        public double CalculateDamage(double originalDamage)
        {
            double damage = Math.Max(originalDamage - owner.Armor,0);
            return damage;
        }

        public bool CanBeActivated(IDamageTakingState currentState)
        {
            return currentState.CanSwitchToNormal(this);
        }

    }
}
