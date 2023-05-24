using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.State.Movement
{
    public class MovementStateHandler : StateHandler<IMovementState>
    {
        public MovementStateHandler(IMovementState baseDamageTakingState) : base(baseDamageTakingState)
        {
        }

        public IMovementUnit CalculateMovement(IMovementUnit movementUnit)
        {
            return CurrentState.CaluculateMovement(movementUnit);
        }
    }
}
