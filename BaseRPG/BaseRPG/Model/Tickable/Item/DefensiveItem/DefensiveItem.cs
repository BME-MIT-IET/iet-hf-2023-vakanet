using BaseRPG.Model.Attribute;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.DefensiveItem
{
    public class DefensiveItem : Item
    {
        public DefensiveItem(World currentWorld, int basePrice) : base(currentWorld, basePrice)
        {
        }

        public override object Clone(int basePrice)
        {
            return new DefensiveItem(CurrentWorld,basePrice);
        }

        public override void EquippedBy(Inventory inventory)
        {
            inventory.EquippedArmor = this;
        }
    }
}
