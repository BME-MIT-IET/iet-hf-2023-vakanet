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
    public sealed partial class WindowHeader : UserControl
    {
        public double LineWidth { get => rect.Height; set { rect.Height = value; } }
        public event Action<object, RoutedEventArgs> XButtonClicked;
        public string TitleText { get {
                return title.Text;
            } set {
                title.Text = value;
            } }
        public WindowHeader()
        {
            this.InitializeComponent();
            
            
        }
        private void xButton_Click(object sender, RoutedEventArgs e)
        {
            XButtonClicked?.Invoke(sender,e);
        }
    }
}
