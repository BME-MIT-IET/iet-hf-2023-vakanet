using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions.Movement
{
    public class MovementInputAction : IInputAction
    {
        private MoveDirection moveDirection;
        private readonly PlayerControl playerControl;

        public MovementInputAction(MoveDirection moveDirection, PlayerControl playerControl)
        {
            this.moveDirection = moveDirection;
            this.playerControl = playerControl;
        }

        public void OnHold(double delta)
        {
            playerControl.OnMove(moveDirection);
        }


    }
}
