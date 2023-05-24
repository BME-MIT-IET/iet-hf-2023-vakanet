using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
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
    public sealed partial class LabelAndTextBox : UserControl
    {
        public string LabelText { get => label.Text; set => label.Text = value; }
        public string InputText { get => input.Text; set => input.Text = value; }
        public event Action<object,TextChangedEventArgs> InputTextChanged;
        public LabelAndTextBox()
        {
            this.InitializeComponent();
        }

        private void input_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputTextChanged?.Invoke(sender, e);
        }
    }
}
