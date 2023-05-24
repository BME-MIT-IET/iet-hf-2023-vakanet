using BaseRPG.Controller.Input;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Controller.Window;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Camera;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;

namespace BaseRPG.Controller.Initialization.GameConfiguring
{

    public class GameConfiguration:IReadOnlyGameConfiguration
    {
        public class DrawableData
        {
            public IDrawable Drawable { get; set; }
            public int Layer { get; set; }

            public DrawableData(IDrawable drawable, int layer)
            {
                Drawable = drawable;
                Layer = layer;
            }
        }
        public PositionObserver GlobalMousePositionObserver { get; set; }
        public IDrawableProvider DrawableProvider { get; set; }
        public IImageProvider ImageProvider { get; set; }
        public BindingHandler Binding { get; set; }
        public InputHandler InputHandler { get; internal set; }
        public Game Game { get; internal set; }
        public World CurrentWorld => Game.CurrentWorld;
        public FollowingCamera2D FollowingCamera { get; internal set; }
        public CollisionNotifier2D CollisionNotifier2D { get; internal set; }
        public AnimationProvider AnimationProvider { get; internal set; }
        public List<DrawableData> Drawables { get; } = new();
        public List<GameObject> GameObjects { get; } = new();
        public List<IShape2D> Shapes { get; } = new();
        public IPhysicsFactory PhysicsFactory => Game.PhysicsFactory;
        public Hero Hero { get; internal set; }
        public ShopControl ShopControl { get; internal set; }
        public PlayerControl PlayerControl { get; internal set; }
        public InventoryControl InventoryControl { get; internal set; }
        public SpellControl SpellControl { get; internal set; }

        public void AddDrawable(IDrawable drawable, int layer = 0)
        {
            Drawables.Add(new DrawableData(drawable, layer));
        }
        public void AddGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }
        public void AddShape(IShape2D shape)
        {
            Shapes.Add(shape);
        }

        public GameConfiguration() { }

        private void AddDrawables(Controller controller)
        {
            Drawables.ForEach(d => controller.AddView(d.Drawable, d.Layer));
        }
        private void AddShapes(Controller controller)
        {
            Shapes.ForEach(s => controller.AddShape(s));
        }
        public void AddAll(Controller controller)
        {
            AddDrawables(controller);
            AddShapes(controller);
        }
    }

}
