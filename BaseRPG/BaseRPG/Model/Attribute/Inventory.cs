using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.DefensiveItem;
using BaseRPG.Model.Tickable.Item.Weapon;
using System;
using System.Collections.Generic;

namespace BaseRPG.Model.Attribute
{
    public delegate void ItemEquipped(Item previousItem,Item newItem);
    public class Inventory
    {
        private List<Item> items = new();
        public event ItemEquipped WeaponEquipped;
        public event Action<DefensiveItem> DefensiveItemEquipped;
        public event Action<Item> ItemCollected;
        private DefensiveItem equippedArmor;
        private DefensiveItem equippedShoe;
        private Weapon equippedWeapon;
        public int Capacity { get; set; }

        public Inventory(int capacity)
        {
            Capacity = capacity;
        }

        public List<Item> UnequippedItems => items;
        public List<Item> AllItems 
        { 
            get 
            {
                List<Item> result = new List<Item>();
                result.AddRange(items);
                result.Add(EquippedArmor);
                result.Add(EquippedShoe);
                result.Add(EquippedWeapon);
                return result;
            } 
        }
        public DefensiveItem EquippedArmor { get => equippedArmor; set {
                items.Remove(value);
                equippedArmor = value;
                DefensiveItemEquipped?.Invoke(equippedArmor);
            } }
        public DefensiveItem EquippedShoe { get => equippedShoe; set {
                items.Remove(value);
                equippedShoe = value;
                DefensiveItemEquipped?.Invoke(equippedShoe);
            } }

        internal void Equip(Item item)
        {
            item.EquippedBy(this);
        }

        internal void Equip(int itemIndex)
        {
            if (itemIndex >= items.Count) return;
            if (items[itemIndex] == null) return;
            Equip(items[itemIndex]);
        }
        internal void UnEquip(Item item)
        {
            
            if (equippedArmor == item) equippedArmor = null;
            else if (equippedShoe == item) equippedShoe = null;
            else if (equippedWeapon == item) equippedWeapon = null;
            else return;
            Collect(item);
        }

        public Weapon EquippedWeapon { 
            get => equippedWeapon; 
            set 
            {
                if (value == null)
                {
                    equippedWeapon = value;
                    return;
                }
                if (equippedWeapon != null) { 
                    UnEquip(equippedWeapon);
                }
                
                items.Remove(value);
                var temp = equippedWeapon;
                equippedWeapon = value;
                WeaponEquipped?.Invoke(temp, value);
            }
        }

        public void Drop(Item item) {
             items.Remove(item);
        }

        public void Collect(Item collectible)
        {
            if (collectible == null) return; 
            //collectible.OnCollect(this);
            items.Add(collectible);
            ItemCollected?.Invoke(collectible);
        }

    }
}
