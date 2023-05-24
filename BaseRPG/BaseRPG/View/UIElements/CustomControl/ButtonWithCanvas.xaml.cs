using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.UIElements.DrawingArgsFactory;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements.CustomControl
{
    public sealed partial class ButtonWithCanvas : UserControl
    {
        public event Action<object, RoutedEventArgs> ButtonClick;
        public event Action<object, RoutedEventArgs> ButtonRightClick;
        public event Action<CanvasControl, CanvasDrawEventArgs> CanvasDraw;
        public IDrawingArgsFactory DrawingArgsFactory { get; set; }
        public CanvasControl Canvas => canvas;
        public Button Button => button;
        public IDrawable Drawable { get; 
            set; 
        }
        public object EquippedArmor { get; internal set; }

        public void SetImageAsDrawable(DrawingImage image) {
            Drawable = image;
            DrawingArgsFactory = new ImageButtonDrawingArgsFactory(canvas.Size);
        }
        public ButtonWithCanvas()
        {
            this.InitializeComponent();
            button.PointerReleased += OnButtonPointerReleased;
        }

        private void OnButtonPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if(!e.GetCurrentPoint(sender as UIElement).Properties.IsRightButtonPressed)
                ButtonRightClick?.Invoke(sender, e);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            
            ButtonClick?.Invoke(sender,e);
        }

        private void OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            CanvasDraw?.Invoke(sender, args);
            if (DrawingArgsFactory == null) {
                return;
                //throw new NullReferenceException("DrawingArgsFactory");
            }
            Drawable?.OnRender(DrawingArgsFactory.Create(sender, args));
        }
    }
}
