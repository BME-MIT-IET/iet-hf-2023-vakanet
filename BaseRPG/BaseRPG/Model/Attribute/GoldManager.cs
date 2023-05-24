using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{
    public class GoldManager
    {
        private int gold = 10;

        public int Gold => gold;
        public event Action<int> GoldChanged;
        public bool SpendGold(int amount) {
            if (gold - amount < 0) return false;
            gold -= amount;
            GoldChanged?.Invoke(gold);
            return true;
        }

        public void GainGold(int goldValue)
        {
            if (goldValue < 0) throw new ArgumentException("goldValue should be positive! use GoldManager.SpendGold for spending gold!");
            gold += goldValue;
            GoldChanged?.Invoke(gold);
        }
    }
}
