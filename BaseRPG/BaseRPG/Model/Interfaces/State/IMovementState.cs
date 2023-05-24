using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.State.CrowdControl;
using BaseRPG.Model.State.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.State
{
    public interface IMovementState:IState<IMovementState>
    {
        IMovementUnit CaluculateMovement(IMovementUnit originalMovement);


        bool CanSwitchToNormal(NormalMovementState normalMovementState) { return true; }
        bool CanSwitchToBlocking(BlockingMovementState blockingMovementState) { return true; }
    }
}
