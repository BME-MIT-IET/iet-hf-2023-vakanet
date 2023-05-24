using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Factories.Item;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Attacks.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Factories.WeaponFactories
{
    public class SimpleSwordFactory : IItemFactory
    {
        private IAttackFactory attackFactory = new SimpleSwordAttackFactory();

        //public SimpleSwordFactory(IAttackFactory attackFactory)
        //{
        //    this.attackFactory = attackFactory;
        //}

        public static event Action<Item> OnItemCreatedStatic;

        public Item Create(World world)
        {
            var result = new Weapon.Weapon(
                attackFactory,
                world, null, 1, 1);
            OnItemCreatedStatic?.Invoke(result);
            return result;
        }
        private AttackBuilder createLightAttack() {
            return new AttackBuilder(new DamagingAttackStrategy(40));
        }
        private AttackBuilder createHeavyAttack()
        {
            return new AttackBuilder(new DamagingAttackStrategy(100))
                .LifeTimeInSeconds(0.1)
                .AttackLifetimeOverStrategy(new LifetimeOverStrategy())
                .CanHitSameTarget(false);
        }
    }
}
