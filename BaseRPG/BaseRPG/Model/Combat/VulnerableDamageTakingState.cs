using BaseRPG.Model.Interfaces.State;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Combat
{
    public class VulnerableDamageTakingState : IDamageTakingState
    {

        public double CalculateDamage(double originalDamage)
        {
            throw new NotImplementedException();
        }

        public bool CanBeActivated(IDamageTakingState currentState)
        {
            return currentState.CanSwitchToVulnerable(this);
        }

        public bool CanSwitchToInvincible(InvincibleDamageTakingState invincibleDamageTakingState)
        {
            return true;
        }

        public bool CanSwitchToNormal(NormalDamageTakingState normalDamageTakingState)
        {
            return false;
        }
    }
}
