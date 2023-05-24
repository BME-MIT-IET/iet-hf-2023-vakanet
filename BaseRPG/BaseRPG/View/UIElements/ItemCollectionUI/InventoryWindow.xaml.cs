using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces.Providers;
using System.Timers;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements.ItemCollectionUI
{
    public sealed partial class InventoryWindow : CustomWindow
    {
        public static string WindowName => windowName;
        private static readonly string windowName = "inventory";
        private InventoryControl inventoryControl;
        private IDrawableProvider drawableProvider;

        public InventoryWindow(Model.Attribute.Inventory inventory, IDrawableProvider drawableProvider,InventoryControl inventoryControl)
        {
            this.InitializeComponent();
            Inventory = inventory;
            equippedItemUI.Init(inventoryControl, drawableProvider);
            itemsGrid.Init(inventoryControl,drawableProvider);
        }
        public EquippedItemUI EquippedItemsUI => equippedItemUI;
        public Model.Attribute.Inventory Inventory { get; }
        public override void OnOpened()
        {
            base.OnOpened();
            equippedItemUI.EquippedWeaponCanvas.Invalidate();
        }
        public override void OnClosed()
        {
            base.OnClosed();
        }
    }
}
