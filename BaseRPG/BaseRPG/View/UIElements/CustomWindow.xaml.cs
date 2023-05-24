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
    public partial class CustomWindow : UserControl
    {
        protected class DragHandler{
            private bool enabled = false;
            private Vector2D? positionOnDragStart;
            public void Start(Vector2D position) {
                enabled = true;
                positionOnDragStart = position;
            }
            public bool Enabled => enabled;
            public Vector2D PositionOnDragStart 
            { 
                get 
                {
                    if (positionOnDragStart.HasValue)
                        return positionOnDragStart.Value;
                    throw new Exception("Don't try to get the value of the position if the drag hasn't started!");
                } 
            }
            public void Stop()
            {
                enabled = false;
                positionOnDragStart = null;
            }

        }

        public virtual void  OnClosed()
        {
        }

        public event Action<CustomWindow> XButtonClicked;
        private DragHandler dragHandler;
        public CustomWindow()
        {
            this.InitializeComponent();
            dragHandler = new();
        }
        public void XButton_Click(object sender, RoutedEventArgs e)
        {
            XButtonClicked?.Invoke(this);
        }
        public virtual void OnOpened() { }

        private void StartDrag(Point point) {
            dragHandler.Start(new(point.X, point.Y));
        }
        private void StopDrag()
        {
            dragHandler.Stop();
        }

        private void OnDrag(Point mousePos) {
            if (dragHandler.Enabled)
            {
                Canvas.SetLeft(this, -dragHandler.PositionOnDragStart.X + mousePos.X);
                Canvas.SetTop(this, -dragHandler.PositionOnDragStart.Y + mousePos.Y);
            }
        }

        protected void UserControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            OnDrag(e.GetCurrentPoint(Parent as UIElement).Position);
        }

        protected void UserControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            StartDrag(e.GetCurrentPoint(this).Position);
        }

        protected void UserControl_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            StopDrag();
        }
    }
}
