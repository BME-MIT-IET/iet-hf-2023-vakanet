using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class Binding
    {
        private string input;
        private string action;
        public event Action<string, string> InputChanged;
        public event Action<string, string> ActionChanged;
        public Binding(string input, string action)
        {
            Input = input;
            Action = action;
        }

        public string Input { 
            get => input;
            set
            {
                InputChanged?.Invoke(input, value);
                input = value;
            }
        }
        public string Action { 
            get => action;
            set
            {
                ActionChanged?.Invoke(action, value);
                action = value;
            }
        }
    }
}
