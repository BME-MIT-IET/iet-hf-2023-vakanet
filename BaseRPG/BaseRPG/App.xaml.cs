using BaseRPG.Controller;
using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Controller.Input;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.View;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using Microsoft.UI.Xaml;
using SimpleInjector;
using System.Runtime.InteropServices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {

        public static readonly int IMAGE_SCALE = 4;
        private Container container;
        private MainWindow window;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            AllocConsole();
            this.InitializeComponent();
            
        }
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Container container = registerDependecies();
            
            window = new MainWindow();
            window.OnResourcesReady += StartGame;
            window.Activate();
        }
        private Container registerDependecies() {
            container = new Container();

            container.RegisterSingleton<IWorldNameImageMapper, DefaultWorldNameImageMapper>();
            container.RegisterSingleton<IViewManager, ViewManager>();
            container.RegisterSingleton<Controller.Controller>();
            container.Register<IPhysicsFactory,PhysicsFactory2D>();

            container.RegisterSingleton<ICanvasProvider>(() => window);
            container.RegisterSingleton<MainWindow>(() => window);
            container.Register<IGameConfigurer, GameConfigurer>();

            container.Register<IDrawableProvider, DrawableProvider>();
            container.Register<IImageProvider>(() => 
                new CenteredImageProvider(new ScalingImageProvider(IMAGE_SCALE, new RawImageProvider())));
            container.Register<IMovementManager>(() => {
                var physicsFactory = container.GetInstance<IPhysicsFactory>();
                return physicsFactory.CreateMovementManager();
            });
            container.Register<BindingHandler>(()=>
                BindingHandler.CreateAndLoad(@"Assets\config\input-binding.json"));
            container.Register<IRawInputProcessedInputMapper, RawInputProcessedInputMapper>();

            return container;
        }
        private void StartGame() {
            container.Register<InputHandler>();
            container.RegisterSingleton<IReadOnlyGameConfiguration>(() =>
                container
                .GetInstance<IGameConfigurer>()
                .Configure(container)
            );
            container.Register<IProcessedInputActionMapper>(() => {
                var config = container.GetInstance<IReadOnlyGameConfiguration>();
                return ProcessedInputActionMapper.Default(config, window);
            });
            container.Verify();
            IPhysicsFactory.Instance = container.GetInstance<IPhysicsFactory>();
            window.ViewManager = container.GetInstance<IViewManager>();
            window.Controller = container.GetInstance<Controller.Controller>();
            var config = container.GetInstance<IReadOnlyGameConfiguration>();
            var controller = container.GetInstance<Controller.Controller>();

            controller.Initialize(config,window);
            new MainLoop(controller).Start();
        }
    }
}
