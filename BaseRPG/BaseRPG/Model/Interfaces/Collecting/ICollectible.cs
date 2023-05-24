using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds.InteractionPoints;
using System.Collections.Generic;

namespace BaseRPG.Model.Interfaces.Collecting
{
    public interface ICollectible
    {
        void OnCollectedByHero(Hero hero);
        void OnCollectedByShop(Shop shop);
    }
}
