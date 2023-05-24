using BaseRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Attacks.Lifetime
{
    
    public class DelegateLifetimeOverStrategy : IAttackLifetimeOverStrategy
    {
        private Func<Attack, bool> isOver;

        public DelegateLifetimeOverStrategy(Func<Attack, bool> isOver)
        {
            this.isOver = isOver;
        }

        public bool IsOver(Attack attack)
        {
            return isOver(attack);
        }
    }
}
