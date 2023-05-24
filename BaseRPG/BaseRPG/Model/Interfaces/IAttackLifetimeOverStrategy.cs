using BaseRPG.Model.Tickable.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces
{
    public interface IAttackLifetimeOverStrategy
    {
        bool IsOver(Attack attack);
    }
}
