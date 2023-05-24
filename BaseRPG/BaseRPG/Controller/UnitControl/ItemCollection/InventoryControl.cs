using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Attribute;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl.ItemCollection
{
    public class InventoryControl : IItemCollectionControl
    {
        private Inventory inventory;
        private readonly IDrawableProvider drawableProvider;
        private readonly Controller controller;
        //public event Action<Item, DrawableProvider> OnUnequipped;
        //public event Action<Item, DrawableProvider> OnEquipped;
        //public event Action<Item, DrawableProvider> OnCollected;
        public event Action OnChanged;

        public InventoryControl(Inventory inventory, IDrawableProvider drawableProvider, Controller controller)
        {
            this.inventory = inventory;
            this.drawableProvider = drawableProvider;
            this.controller = controller;
            inventory.ItemCollected += item => OnChanged?.Invoke();
        }

        public Item GetItemAt(int index)
        {
            if (index >= inventory.UnequippedItems.Count)
                return null;
            return inventory.UnequippedItems[index];
        }
        public Inventory Inventory { get { return inventory; } }

        public Item EquippedWeapon => inventory.EquippedWeapon;
        public Item EquippedArmor => inventory.EquippedArmor;
        public Item EquippedShoe => inventory.EquippedShoe;

        public IDrawableProvider DrawableProvider => drawableProvider;

        public int Capacity => inventory.Capacity;

        public void EquipItem(Item item)
        {
            controller.QueueAction(() =>
            {
                UnEquipItem(inventory.EquippedWeapon,false);
                inventory.Equip(item);
                controller.AddView(drawableProvider.GetDrawable(inventory.EquippedWeapon, "equipped"), 100);
                OnChanged?.Invoke();
            });
            

        }
        public void EquipItem(int index)
        {
            if (inventory.UnequippedItems.Count <= index)
            {
                return;
            }
            EquipItem(inventory.UnequippedItems[index]);
        }
        public void UnEquipItem(Item item,bool shouldQueue = true)
        {
            var func = () =>
            {
                inventory.UnEquip(item);
                var views = drawableProvider.GetDrawablesOf(item);
                controller.RemoveView(drawableProvider.GetDrawable(item, "equipped"));
                OnChanged?.Invoke();
            };
            if (shouldQueue)
                controller.QueueAction(func);
            else func();
        }
        public void CollectItem(Item item)
        {
            inventory.Collect(item);
        }

        public void DropItem(int index)
        {
            controller.QueueAction(() =>
            {
                if (inventory.UnequippedItems.Count <= index)
                {
                    return;
                }
                inventory.Drop(inventory.UnequippedItems[index]);
                OnChanged?.Invoke();
            });
            
        }

        public void OnItemLeftClicked(int index)
        {
            EquipItem(index);
        }
        public void OnItemRightClicked(int index)
        {
            DropItem(index);
        }
    }
}
