using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.EntityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Interfaces
{
    public interface IItemCollectionControl
    {
        int Capacity { get; }
        event Action OnChanged;

        Item GetItemAt(int i);
        void OnItemLeftClicked(int index);
        void OnItemRightClicked(int index);
    }
}
