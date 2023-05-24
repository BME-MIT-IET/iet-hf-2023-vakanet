using System.Collections.Generic;
using BaseRPG.Controller.Input;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.Camera;
using BaseRPG.View.EntityView;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces.Providers;

namespace BaseRPG.Controller.Interfaces
{
    public interface IReadOnlyGameConfiguration
    {
        public PositionObserver GlobalMousePositionObserver { get; }
        public IDrawableProvider DrawableProvider { get; }
        public IImageProvider ImageProvider { get; }
        public BindingHandler Binding { get; }
        public InputHandler InputHandler { get; }
        public Game Game { get; }
        public World CurrentWorld { get; }
        public FollowingCamera2D FollowingCamera { get; }
        public CollisionNotifier2D CollisionNotifier2D { get; }
        public AnimationProvider AnimationProvider { get; }
        public IPhysicsFactory PhysicsFactory { get; }
        public Hero Hero { get; }
        public ShopControl ShopControl { get; }
        public PlayerControl PlayerControl { get; }
        public InventoryControl InventoryControl { get; }
        public SpellControl SpellControl { get; }

        void AddAll(Controller controller);
    }
}
