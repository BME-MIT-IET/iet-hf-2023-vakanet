using BaseRPG.Controller.Initialization;
using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Controller.Input;
using BaseRPG.Controller.Input.InputActions;
using BaseRPG.Controller.Input.InputActions.Attack;
using BaseRPG.Controller.Input.InputActions.Effect;
using BaseRPG.Controller.Input.InputActions.Interaction;
using BaseRPG.Controller.Input.InputActions.Movement;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Controller.Utility;
using BaseRPG.Controller.Window;
using BaseRPG.Model.Data;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.Utility;
using BaseRPG.Model.Tickable.Item.Factories;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using BaseRPG.Model.Worlds.Blocks;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Camera;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using BaseRPG.View.UIElements;
using BaseRPG.View.UIElements.ItemCollectionUI;
using BaseRPG.View.UIElements.Spell;
using BaseRPG.View.WorldView;
using Microsoft.Graphics.Canvas.UI.Xaml;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace BaseRPG.Controller
{
    public class Controller:ICanQueueAction,ICanQueueFunc<bool>
    {
        #region private fields
        private PlayerControl playerControl;
        private InputHandler inputHandler;
        private ViewManager viewManager;
        private Game game;
        private bool running = true;
        private CallbackQueue callbackQueue = new();
        private BoolCallbackQueue boolCallbackQueue = new();
        private IImageProvider imageProvider;
        #endregion

        #region properties
        public Game Game => game;
        public InputHandler InputHandler { get { return inputHandler; } }
        public bool Running { get => running; }
        public PlayerControl PlayerControl { set { playerControl = value; } get => playerControl; }
        public IDrawableProvider DrawableProvider { get; internal set; }
        public IImageProvider ImageProvider => imageProvider;

        public BoolCallbackQueue BoolCallbackQueue { get => boolCallbackQueue; }
        public CollisionNotifier2D CollisionNotifier { get; set; }
        public ViewManager ViewManager => viewManager;
//        public ViewManager ViewManager { get => viewManager; set => viewManager = value; }
        #endregion

        public Controller(IViewManager view)
        {
            this.viewManager = view as ViewManager;
        }
        public void QueueAction(Action action) {
            callbackQueue.QueueAction(action);
        }
        public void Tick(double delta) {
            boolCallbackQueue.Tick();
            callbackQueue.ExecuteAll();
            inputHandler.OnTick(delta);
            game.OnTick(delta);
            playerControl.OnTick(delta);
        }
        public void Initialize(
            IReadOnlyGameConfiguration config,
            MainWindow window) {

            configure(config);
            config.AddAll(this);
            configureWindows(window, config);
        }
        private void configure(IReadOnlyGameConfiguration config) {
            imageProvider = config.ImageProvider;
            inputHandler = config.InputHandler;
            CollisionNotifier = config.CollisionNotifier2D;
            DrawableProvider = config.DrawableProvider;
            playerControl = config.PlayerControl;
            viewManager.GlobalMousePositionProvider = config.GlobalMousePositionObserver;
            game = config.Game;
            game.Hero = config.Hero;
        }
        private void configureWindows(MainWindow window, IReadOnlyGameConfiguration config){
            var initializer = new DefaultWindowinitializer(
                config.Binding,
                game.Hero.Inventory,
                config.InventoryControl,
                config.SpellControl
            );
            window.WindowControl = initializer.Initialize(window, config.ShopControl);
            config.InputHandler.ProcessedInputActionMapper = ProcessedInputActionMapper.Default(config, window);
            window.OnKeyDown += inputHandler.KeyDown;
            window.OnKeyUp += inputHandler.KeyUp;
            window.OnPointerPressed += inputHandler.MouseDown;
            window.OnPointerReleased += inputHandler.MouseUp;
            window.OnPointerMoved += inputHandler.MouseMoved;
        }
        public void AddView(IDrawable drawable, int layer = 0) {
            viewManager.CurrentWorldView.AddView(drawable, layer);
        }

        public void AddShape(IShape2D shape)
        {
            
            if (System.Diagnostics.Debugger.IsAttached)
                viewManager.CurrentWorldView.AddView(
                    new ShapeView(shape, new PositionObserver(() => 
                    PositionUnit2D.ToVector2D(shape.MovementManager.Position)
                    )),
                    int.MaxValue);
            CollisionNotifier.AddToObservedShapes(shape);
        }
        public void AddVisible(ShapeViewPair fullGameObject, int layer = 0) {
            QueueAction(
                () => 
                AddVisibleInstantly(fullGameObject, layer));
        }
        public void AddVisibleInstantly(ShapeViewPair fullGameObject, int layer = 0)
        {
            if (fullGameObject.View != null)
                AddView(fullGameObject.View, layer);
            if (fullGameObject.Shape != null)
                AddShape(fullGameObject.Shape);
        }

        internal void RemoveView(IDrawable drawable)
        {
            viewManager.CurrentWorldView.RemoveView(drawable);
        }

        public void QueueWithResult(Func<bool> func, Action<bool> onResult)
        {
            throw new NotImplementedException();
        }
    }
}
