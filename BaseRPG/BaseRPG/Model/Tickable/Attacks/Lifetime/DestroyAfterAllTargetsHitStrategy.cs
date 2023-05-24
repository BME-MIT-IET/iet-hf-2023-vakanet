using BaseRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Attacks.Lifetime
{
    internal class DestroyAfterAllTargetsHitStrategy : IAttackLifetimeOverStrategy
    {
        public bool IsOver(Attack attack)
        {
            return attack.LifeTime <= 0 || attack.NumberOfRemainingMaxTargets <= 0;
        }
    }
}
