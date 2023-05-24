using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Combat.Attack
{
    public interface IAttackFactory
    {
        public IAttacking Owner { get; set; }
        public World World { get; set; }
        event Action<Tickable.Attacks.Attack> LightAttackCreated;
        event Action<Tickable.Attacks.Attack> HeavyAttackCreated;

        public Tickable.Attacks.Attack CreateLight(IPositionUnit position, IMovementUnit movementDirection);
        public Tickable.Attacks.Attack CreateHeavy(IPositionUnit position, IMovementUnit movementDirection);
    }
}
