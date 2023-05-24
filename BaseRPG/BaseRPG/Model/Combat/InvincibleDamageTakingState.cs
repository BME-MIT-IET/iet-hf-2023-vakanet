using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.State;
using BaseRPG.Model.Tickable.FightingEntity;

namespace BaseRPG.Model.Combat
{
    public class InvincibleDamageTakingState : IDamageTakingState
    {

        public double CalculateDamage( double originalDamage)
        {
            return 0;
        }

        public bool CanBeActivated(IDamageTakingState currentState)
        {
            return currentState.CanSwitchToInvincible(this);
        }

        public bool CanSwitchToInvincible(InvincibleDamageTakingState invincibleDamageTakingState)
        {
            return false;
        }

        public bool CanSwitchToNormal(NormalDamageTakingState normalDamageTakingState)
        {
            return false;
        }

        public bool CanSwitchToVulnerable(VulnerableDamageTakingState vulnerableDamageTakingState) {
            return false;
        }
    }
}
