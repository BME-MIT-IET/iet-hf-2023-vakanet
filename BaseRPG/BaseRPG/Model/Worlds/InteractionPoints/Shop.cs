using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Physics.TwoDimensional.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Worlds.InteractionPoints
{
    public class Shop : GameObject, ICollisionDetector,ICollector
    {
        public static int Capacity => 20;
        private class ShopItem{
            public ShopItem(Item item, int cost)
            {
                Item = item;
                Cost = cost;
            }

            public Item Item { get; set; }
            public int Cost { get; set; }
        }
        private List<ShopItem> items = new();
        private IPositionUnit position;
        private World currentWorld;

        public Shop(IPositionUnit position, World currentWorld, bool addToWorldInstantly = true):base(currentWorld, addToWorldInstantly)
        {
            this.position = position;
            this.currentWorld = currentWorld;
        }

        internal Item GetItemAt(int i)
        {
            return items[i].Item;
            //throw new NotImplementedException();
        }

        internal void AddItem(Item item,int cost)
        {
            items.Add(new(item,cost));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="itemIndex"></param>
        /// <returns> if the purchase was successful</returns>
        public bool Buy(Hero hero,int itemIndex) {
            if (itemIndex >= items.Count) return false;
            var success = hero.SpendGold(items[itemIndex].Cost);
            if (success) { 
                hero.Collect(GetItemAt(itemIndex));
            }
            return success;
            
        }
        public override bool Exists => true;

        

        public IPositionUnit Position => position;

        public int ItemCount => items.Count;

        public override event Action OnCeaseToExist;
        public override event Action<Hero,GameObject> InteractionStarted;


        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }

        public override void Step(double delta)
        {
            //throw new NotImplementedException();
        }

        public void OnCollision(ICollisionDetector other, double delta)
        {
            //throw new NotImplementedException();
        }

        public bool CanCollide(ICollisionDetector other)
        {
            return true;
            //throw new NotImplementedException();
        }
        public void SeletByInteractionTargetability(LinkedList<GameObject> targetableGameObjects, LinkedList<ICollisionDetector> targetableOther) {

            targetableGameObjects.AddLast(this);
        }
        public override void InteractWith(Hero interactionStarter)
        {
            InteractionStarted?.Invoke(interactionStarter, this);
        }

        public void Collect(ICollectible collectible)
        {
            collectible.OnCollectedByShop(this);
        }
    }
}
