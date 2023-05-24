using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.State.CrowdControl
{
    public class NormalMovementState : IMovementState
    {
        public IMovementUnit CaluculateMovement(IMovementUnit originalMovement)
        {
            return originalMovement;
        }

        public bool CanBeActivated(IMovementState newState)
        {
            return newState.CanSwitchToNormal(this);
        }
    }
}
