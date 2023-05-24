using BaseRPG.Controller.Utility;
using System.Text.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class BindingHandler
    {
        private string bindingFilePath;

        public BindingHandler(string bindingFilePath)
        {
            this.bindingFilePath = bindingFilePath;
        }
        private List<Binding> bindings = new();
        
        public List<string> PossibleActions { 
            get 
            { 
                return bindings.Select(b=>b.Action).ToList();
            } 
        }

        public List<Binding> Bindings => bindings;

        public bool HasInput(string rawInput) {
            return bindings.Select(b => b.Input).Contains(rawInput);
        }
        public IEnumerable<string> BindingOf(string input) {
            return bindings.FindAll(b=>b.Input == input).Select(b=>b.Action);
        }
        public void Add(string rawInput, string processedInput) {
            AddBinding(new(rawInput, processedInput));
        }
        public void AddBinding(Binding binding) {
            bindings.Add(binding);
        }
        public void Save() {
            MyJsonWriter.SaveAsList(bindingFilePath, bindings);
        }
        public void Load() {
            bindings = MyJsonReader.AsList<Binding>(bindingFilePath);
        }
        public static BindingHandler CreateAndLoad(string filePath) {
            var bindings = new BindingHandler(filePath);
            bindings.Load();
            return bindings;
        }
    }


}
