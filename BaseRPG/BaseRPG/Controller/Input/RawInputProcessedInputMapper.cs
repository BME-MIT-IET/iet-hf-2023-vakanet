using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class RawInputProcessedInputMapper: IRawInputProcessedInputMapper
    {
        private BindingHandler binding;

        public RawInputProcessedInputMapper(BindingHandler binding)
        {
            this.binding = binding;
        }


        public IEnumerable<string> toProcessedInput(string input) {
            //if (!binding.HasInput(input)) { return ""; }
            return binding.BindingOf(input);
        }
        public static RawInputProcessedInputMapper FromBinding(BindingHandler bindingHandler)
        {
            RawInputProcessedInputMapper rawInputProcessedInputMapper = new RawInputProcessedInputMapper(bindingHandler);
            return rawInputProcessedInputMapper;
        }
    }
}
