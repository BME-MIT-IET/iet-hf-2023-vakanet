using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces.Providers;
using BaseRPG.View.ItemView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces.Factory
{
    public class ItemViewCreationParams
    {
        private IImageProvider imageProvider;
        private Controller.Controller controller;
        private IPositionProvider globalMousePositionObserver;
        private Weapon weapon;

        public Controller.Controller Controller { get => controller; init => controller = value; }
        public IPositionProvider GlobalMousePositionObserver { get => globalMousePositionObserver; init => globalMousePositionObserver = value; }
        public IImageProvider ImageProvider { get => imageProvider; init => imageProvider = value; }
        public Weapon Weapon { get => weapon; init => weapon = value; }
    }
    public interface IItemViewFactory
    {
        Dictionary<string, IDrawable> Create();
    }
}
