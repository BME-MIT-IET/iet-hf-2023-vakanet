using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.State;
using BaseRPG.Model.State.CrowdControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.State.Movement
{
    public class BlockingMovementState : IMovementState
    {
        public IMovementUnit CaluculateMovement(IMovementUnit originalMovement)
        {
            return originalMovement.Scaled(0);
        }

        public bool CanBeActivated(IMovementState newState)
        {
            return newState.CanSwitchToBlocking(this);
        }

        public bool CanSwitchToNormal(NormalMovementState normalMovementState) {
            return false;
        }
    }
}
