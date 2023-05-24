using BaseRPG.Controller.Input;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Controller.Window;
using BaseRPG.Model.Attribute;
using BaseRPG.View.UIElements;
using BaseRPG.View.UIElements.ItemCollectionUI;
using BaseRPG.View.UIElements.Spell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization
{
    public class DefaultWindowinitializer : IWindowsInitializer
    {
        private BindingHandler bindingHandler;
        private readonly Inventory inventory;
        private readonly InventoryControl inventoryControl;
        private readonly SpellControl spellControl;

        public DefaultWindowinitializer(BindingHandler bindingHandler, Inventory inventory, InventoryControl inventoryControl, SpellControl spellControl)
        {
            this.bindingHandler = bindingHandler;
            this.inventory = inventory;
            this.inventoryControl = inventoryControl;
            this.spellControl = spellControl;
        }

        public WindowControl Initialize(MainWindow window, ShopControl shopControl)
        {
            var result = new WindowControl.Builder(window.MainCanvas)
                .SettingsWindowAs(new SettingsWindow(exitCallback: window.Close))
                .Window(InventoryWindow.WindowName, new InventoryWindow(inventory,window.Controller.DrawableProvider, inventoryControl))
                .Window(KeyBindingsWindow.WindowName,new KeyBindingsWindow(bindingHandler, 2))
                .Window(SpellsWindow.WindowName,new SpellsWindow(window.ImageProvider, spellControl))
                .Window(ShopWindow.WindowName,new ShopWindow(window.Controller.DrawableProvider, shopControl))
                .Build();
            shopControl.WindowControl = result;
            return result;
        }
    }
}
