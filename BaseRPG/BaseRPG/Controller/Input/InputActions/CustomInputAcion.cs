using BaseRPG.Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions
{
    public class CustomInputAcion:IInputAction
    {
        private Action actionOnHold;
        private Action actionOnPressed;
        private Action actionOnReleased;

        public CustomInputAcion(Action actionOnHold = null, Action actionOnPressed = null, Action actionOnReleased = null)
        {
            this.actionOnHold = actionOnHold;
            this.actionOnPressed = actionOnPressed;
            this.actionOnReleased = actionOnReleased;
        }

        public void OnPressed() { actionOnPressed?.Invoke(); }
        public void OnReleased() { actionOnReleased?.Invoke();  }
        public void OnHold() { actionOnHold?.Invoke(); }
    }
}
