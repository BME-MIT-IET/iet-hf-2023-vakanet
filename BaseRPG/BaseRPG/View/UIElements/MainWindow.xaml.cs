using BaseRPG.Controller.Utility;
using BaseRPG.Controller.Window;
using BaseRPG.Model.Game;
using BaseRPG.View;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView.Health;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using BaseRPG.View.UIElements.ItemCollectionUI;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window, ICanvasProvider
    {

        public event Action OnResourcesReady;
        private Controller.Controller controller;
        private IViewManager viewManager;
        public IViewManager ViewManager { get { return viewManager; } set { viewManager = value; } }
        public Vector2D CameraPosition => viewManager.CameraPosition;
        private DeltaLoopHandler drawLoopHandler;
        private WindowControl windowControl;
        public CanvasVirtualControl Canvas => canvas;
        public Canvas MainCanvas => mainCanvas;
        public Controller.Controller Controller { get => controller; set => controller = value; }
        public WindowControl WindowControl { get => windowControl; set => windowControl = value; }

        public event Action<object, PointerRoutedEventArgs> OnPointerPressed;
        public event Action<object, PointerRoutedEventArgs> OnPointerReleased;
        public event Action<object, PointerRoutedEventArgs> OnPointerMoved;
        public event Action<object, KeyRoutedEventArgs> OnKeyDown;
        public event Action<object, KeyRoutedEventArgs> OnKeyUp;

        public MainWindow()
        {
            this.InitializeComponent();

            this.SizeChanged += (s, e) =>
            {
                canvas.Width = e.Size.Width;
                canvas.Height = e.Size.Height;
            };

            drawLoopHandler = new DeltaLoopHandler();
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (a, b) => Console.WriteLine("Draw fps: " + drawLoopHandler.Fps);
            timer.Start();
        }
        public Vector2D MiddleOfScreen => new(canvas.Width / 2, canvas.Height / 2);

        public IImageProvider ImageProvider => controller.ImageProvider;

        public async Task CreateResourceAsync(CanvasVirtualControl sender)
        {
            await RawImageProvider.LoadImages(sender);
        }
        public void canvas_CreateResources(CanvasVirtualControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourceAsync(sender).AsAsyncAction());
            sender.Invalidate();
            drawLoopHandler.FirsTickEvent += () => OnResourcesReady();
            drawLoopHandler.FirsTickEvent += ResourcesReady;
        }
        public void ResourcesReady()
        {

            hud.Init(controller.ImageProvider, controller.Game.Hero);
        }
        public void canvas_Draw(CanvasVirtualControl sender, CanvasRegionsInvalidatedEventArgs args)
        {

            foreach (var region in args.InvalidatedRegions)
            {
                using (var ds = sender.CreateDrawingSession(region))
                {
                    var delta = drawLoopHandler.Tick();
                    DrawingArgs drawingArgs = new DrawingArgs(sender, delta, controller.InputHandler.MousePosition, ds);
                    viewManager.Draw(drawingArgs);

                }
            }
            canvas.Invalidate();
        }
        //public void canvas2_Draw(CanvasVirtualControl sender, CanvasRegionsInvalidatedEventArgs args)
        //{

        //    foreach (var region in args.InvalidatedRegions)
        //    {
        //        using (var ds = sender.CreateDrawingSession(region))
        //        {
        //            ds.FillRectangle(0, 0, (float)canvas2.Width, (float)canvas2.Height,Color.FromArgb(255,0,0,255));
        //        }
        //    }
        //    canvas2.Invalidate();
        //}
        private void PointerMoved(object sender, PointerRoutedEventArgs e)
        {

            OnPointerMoved?.Invoke(sender, e);
        }

        private void PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            OnPointerPressed?.Invoke(sender, e);
        }

        private void PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            OnPointerReleased?.Invoke(sender, e);
        }

        private void KeyDown(object sender, KeyRoutedEventArgs e)
        {
            OnKeyDown?.Invoke(sender, e);
        }
        private void KeyUp(object sender, KeyRoutedEventArgs e)
        {
            OnKeyUp?.Invoke(sender, e);
        }

        private void hud_WindowButtonClicked(string windowName)
        {
            WindowControl.OpenOrClose(windowName);
        }
    }
}
