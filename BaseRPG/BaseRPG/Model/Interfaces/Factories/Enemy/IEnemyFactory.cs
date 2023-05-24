using BaseRPG.Model.Interfaces.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Factories.Enemy
{
    public interface IEnemyFactory
    {
        Tickable.FightingEntity.Enemy.Enemy Create(IPositionUnit positionUnit);
    }
}
