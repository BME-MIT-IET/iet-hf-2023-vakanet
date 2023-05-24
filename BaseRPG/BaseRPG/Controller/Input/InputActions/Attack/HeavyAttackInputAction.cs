using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions.Attack
{
    public class HeavyAttackInputAction : IInputAction
    {
        private readonly PlayerControl playerControl;

        public HeavyAttackInputAction(PlayerControl playerControl)
        {
            this.playerControl = playerControl;
        }
        public void OnPressed()
        {
            playerControl.HeavyAttackChargeStart();
        }
        public void OnReleased()
        {
            playerControl.HeavyAttackRelease();
        }

        public void OnHold(double delta) {
            playerControl.HeavyAttackChargeHold(delta);
        }

    }
}
