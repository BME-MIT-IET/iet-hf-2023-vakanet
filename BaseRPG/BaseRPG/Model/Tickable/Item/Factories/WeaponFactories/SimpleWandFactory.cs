using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Factories.Item;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Attacks.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Factories.WeaponFactories
{
    public class SimpleWandFactory : IItemFactory
    {
        private IAttackFactory attackFactory = new SimpleWandAttackFactory();

        //public SimpleWandFactory(IAttackFactory attackFactory)
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
        private AttackBuilder createLightAttackBuilder()
        {
            return new AttackBuilder(new DamagingAttackStrategy(20))
                .AttackLifetimeOverStrategy(new DestroyAfterAllTargetsHitStrategy())
                .CanHitSameTarget(false)
                .NumberOfMaxTargets(10)
                .LifeTimeInSeconds(3);
        }
        private AttackBuilder createHeavyAttackBuilder()
        {
            return new AttackBuilder(new DamagePerSecondAttackStrategy(40))
                .AttackLifetimeOverStrategy(new LifetimeOverStrategy())
                .CanHitSameTarget(true)
                .LifeTimeInSeconds(2);
        }
    }
}
