using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.Window;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Worlds.InteractionPoints;
using BaseRPG.View.EntityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl.ItemCollection
{
    public class ShopControl : IItemCollectionControl
    {
        private Shop currentShop;
        private readonly PlayerControl playerControl;
        private WindowControl windowControl;

        public event Action OnChanged;

        public ShopControl(PlayerControl playerControl )
        {
            this.playerControl = playerControl;
        }

        public int Capacity => Shop.Capacity;

        public Shop CurrentShop { get => currentShop; set => currentShop = value; }

        public WindowControl WindowControl { get => windowControl; set => windowControl = value; }

        public Item GetItemAt(int i)
        {
            if (currentShop == null) return null;
            if (i >= currentShop.ItemCount)
                return null;
            return currentShop.GetItemAt(i);
        }

        public void OnItemLeftClicked(int index)
        {
            
            playerControl.BuyFromShop(currentShop, index);
        }

        public void OnItemRightClicked(int index)
        {
            throw new NotImplementedException();
        }

        internal void ClickedOnShop(Shop shop)
        {
            
            if (currentShop == null || shop == currentShop) {
                currentShop = shop;
                windowControl.OpenOrClose("shop");
                return;
            }
            //if clicked on a new shop, the old one should be closed and the new one should be opened
            windowControl.Close("shop");
            currentShop = shop;
            windowControl.Open("shop");
        }

        internal void AddItemToShop(Item item)
        {
            currentShop.Collect(item);
        }
    }
}
