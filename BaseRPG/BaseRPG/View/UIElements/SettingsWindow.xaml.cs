using BaseRPG.Controller.Window;
using MathNet.Spatial.Euclidean;
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
    public sealed partial class SettingsWindow : CustomWindow
    {
        public static string WindowName => windowName;
        private static readonly string windowName = "settings";
        private Action exitCallback;
        private WindowControl windowControl;

        public WindowControl WindowControl { get => windowControl; set => windowControl = value; }

        public SettingsWindow(Action exitCallback)
        {
            this.InitializeComponent();
            this.exitCallback = exitCallback;
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            exitCallback?.Invoke();
        }
        private void bindingsButton_Click(object sender, RoutedEventArgs e)
        {
            WindowControl.OpenOrClose(KeyBindingsWindow.WindowName);
        }
    }
}
