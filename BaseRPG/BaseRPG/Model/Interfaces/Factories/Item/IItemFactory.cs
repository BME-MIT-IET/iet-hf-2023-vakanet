using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Worlds;

namespace BaseRPG.Model.Interfaces.Factories.Item
{
    public interface IItemFactory
    {
        Tickable.Item.Item Create(World world);
    }
}
