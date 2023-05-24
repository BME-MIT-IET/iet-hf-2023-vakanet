using BaseRPG.View.Animation;
using BaseRPG.View.EntityView.Health;
using Microsoft.Graphics.Canvas.UI.Xaml;
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
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements
{
    public sealed partial class HealthViewControl : UserControl
    {
        public HealthViewControl()
        {
            this.InitializeComponent();
        }
        public HealthViewControl(HealthView healthView)
        {
            this.InitializeComponent();
            this.HealthView = healthView;
            canvas.Invalidate();
        }
        private HealthView healthView;

        public HealthView HealthView { get => healthView; set => healthView = value; }
        
        private void canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (HealthView == null)
            {
                canvas.Invalidate();
                return;
            }
            HealthView.Render(
                        new DrawingArgs(
                            sender: sender,
                            delta: 1,
                            positionOnScreen: new(HealthView.BorderWidth / 2, HealthView.BorderWidth / 2),
                            mousePositionOnScreen: new(),
                            drawingSession: args.DrawingSession));
            canvas.Invalidate();
        }
    }
}
