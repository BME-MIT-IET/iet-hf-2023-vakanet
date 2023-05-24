using BaseRPG.Model.Combat;
using BaseRPG.Model.Tickable.FightingEntity;

namespace BaseRPG.Model.Interfaces.State
{
    /// <summary>
    /// A State that represents how much damage its owner should take based on the original damage. For example
    /// </summary>
    public interface IDamageTakingState : IState<IDamageTakingState>
    {
        double CalculateDamage(double originalDamage);
        
        bool CanSwitchToInvincible(InvincibleDamageTakingState invincibleDamageTakingState) { return true; }
        bool CanSwitchToNormal(NormalDamageTakingState normalDamageTakingState) { return true; }
        bool CanSwitchToVulnerable(VulnerableDamageTakingState vulnerableDamageTakingState) { return true; }
    }
}