using BaseRPG.Model.Attribute;
using BaseRPG.Model.Exceptions;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Weapon
{
    public class Weapon:Item
    {
        private Default<double> chargeUpTime;
        private Unit owner;
        public event Action<Attack> HeavyAttackCreatedEvent;
        public event Action<Attack> LightAttackCreatedEvent;
        //private readonly AttackBuilder lightAttackBuilder;
        //private readonly AttackBuilder heavyAttackBuilder;
        private readonly IAttackFactory attackFactory;
        public Unit Owner { 
            get => owner;
            set
            {
                owner = value;
                //heavyAttackBuilder?.Attacker(owner);
                //lightAttackBuilder?.Attacker(owner);
                attackFactory.Owner = owner;
            } 
        }
        //public AttackBuilder LightAttackBuilder => lightAttackBuilder;
        //public AttackBuilder HeavyAttackBuilder => heavyAttackBuilder;

        public IAttackFactory AttackFactory => attackFactory;

        public Weapon(IAttackFactory attackFactory, World world, Unit owner, int basePrice, double chargeUpTime) : base(world, basePrice)
        {
            //this.heavyAttackBuilder = heavyAttackFactory.World(world);
            //this.lightAttackBuilder = lightAttackFactory.World(world);
            
            this.attackFactory = attackFactory;
            attackFactory.World = world;
            this.Owner = owner;
            this.chargeUpTime = chargeUpTime;
        }
        public override void OnCollectedByHero(Hero hero)
        {
            Owner = hero;
            base.OnCollectedByHero(hero);
        }
        public override void EquippedBy(Inventory inventory)
        {
            inventory.EquippedWeapon = this;
        }
        public IAttackFactory GetHeavyAttackIfPossible() {
            if (chargeUpTime.CurrentValue <= 0)
            {
                chargeUpTime.Reset();
                return attackFactory;
            }
            throw new AttackNotFullyChargedException();
        }

        public override object Clone(int basePrice)
        {
            return new Weapon(attackFactory, CurrentWorld,Owner, basePrice,chargeUpTime.DefaultValue);
        }
        internal void OnChargeHold(double delta)
        {
            chargeUpTime.CurrentValue -= delta;
        }
        public void OnLightAttack() 
        { 
        
        }
        public void OnHeavyAttack() 
        { 
        
        }

    }
}
