using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Combat
{
    public class DamagePerSecondAttackStrategy : IAttackStrategy
    {
        private readonly double damagePerSecond;

        public DamagePerSecondAttackStrategy(double damagePerSecond)
        {
            this.damagePerSecond = damagePerSecond;
        }
        public void OnAttackHit(IAttacking attacker, IAttackable attacked, double delta)
        {
            attacked.TakeDamage(damagePerSecond*delta, attacker);
        }
    }
}
