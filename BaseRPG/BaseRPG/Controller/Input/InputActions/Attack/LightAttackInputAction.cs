using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions.Attack
{
    public class LightAttackInputAction : IInputAction
    {
        private readonly PlayerControl playerControl;

        public LightAttackInputAction(PlayerControl playerControl)
        {
            this.playerControl = playerControl;
        }

        public void OnPressed()
        {
            playerControl.LightAttack();
        }

    }
}
