using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Combat
{
    public class DamagingAttackStrategy : IAttackStrategy
    {
        private double damage;


        public DamagingAttackStrategy(double damage)
        {
            this.damage = damage;
        }

        public void OnAttackHit(IAttacking attacker, IAttackable attacked, double delta)
        {
            attacked.TakeDamage(damage, attacker);
        }
    }
}
