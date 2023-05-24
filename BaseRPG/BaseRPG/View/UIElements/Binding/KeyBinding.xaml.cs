using BaseRPG.Controller.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements
{
    public sealed partial class KeyBinding : UserControl
    {
        public KeyBinding(Binding binding, BindingHandler bindingHandler)
        {
            this.InitializeComponent();
            this.input.InputText = binding.Input;
            this.input.InputTextChanged +=
                (o, e) =>
                {
                    binding.Input = input.InputText;
                };
            this.action.ActionCombobox.SelectionChanged += (o, e) => binding.Action = this.action.ActionCombobox.SelectedItem.ToString();
            LoadBinding(bindingHandler, binding.Action);
        }

        public void LoadBinding(BindingHandler bindingHandler, string selectedAction)
        {
            var items = action.ActionCombobox.Items;
            if(items.Count > 0)
                items.Clear();
            foreach (var possibleAction in bindingHandler.PossibleActions)
            {
                items.Add(possibleAction);
            }
            int index = items.IndexOf(selectedAction);
            action.ActionCombobox.SelectedItem = action.ActionCombobox.Items[index];
        }
    }
}
