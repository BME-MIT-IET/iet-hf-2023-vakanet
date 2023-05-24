using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.View.EntityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces.Factory
{
    public interface IAttackViewFactory
    {
        AttackView Create();
    }
}
