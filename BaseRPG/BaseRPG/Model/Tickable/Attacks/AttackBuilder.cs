using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Worlds;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Attacks
{
    public class AttackBuilder
    {
        
        private IAttacking attacker;
        private IAttackStrategy attackStrategy;
        private World world;
        private IPositionUnit initialPosition;
        private double lifeTimeInSeconds;
        private int numberOfMaxTargetsInOneStep;
        private int numberOfMaxTargets = int.MaxValue;
        private  AttackabilityService attackabilityService;
        private IMovementStrategy movementStrategy = new EmptyMovementStrategy();
        private IAttackLifetimeOverStrategy attackLifetimeOverStrategy = new DestroyAfterAllTargetsHitStrategy();
        private bool canHitSameTarget;
        private double secondsBetween2Hits;

        public event Action<Attack> CreatedEvent;
        public AttackBuilder(IAttackStrategy attackStrategy, double lifeTime = 0, int numberOfMaxTargetsInOneStep = 1)
        {
            this.attackStrategy = attackStrategy;
            this.lifeTimeInSeconds = lifeTime;
            this.numberOfMaxTargetsInOneStep = numberOfMaxTargetsInOneStep;
            attackabilityService = new AttackabilityService.Builder().CreateByDefaultMapping();
        }
        public AttackBuilder World(World world)
        {
            this.world = world;
            return this;
        }
        public AttackBuilder NumberOfMaxTargets(int value)
        {
            numberOfMaxTargets = value;
            return this;
        }
        public AttackBuilder Attacker(IAttacking value)
        {
            attacker = value;
            return this;
        }
        public AttackBuilder LifeTimeInSeconds(double lifeTimeInSeconds) {
            this.lifeTimeInSeconds = lifeTimeInSeconds;
            return this;
        }
        public AttackBuilder MovementStrategy(IMovementStrategy value)
        {
            movementStrategy = value;
            return this;
        }
        public AttackBuilder AttackLifetimeOverStrategy(IAttackLifetimeOverStrategy value)
        {
            attackLifetimeOverStrategy = value;
            return this;
        }
        public AttackBuilder AttackLifetimeOverStrategy(Func<Attack,bool> value)
        {
            attackLifetimeOverStrategy = new DelegateLifetimeOverStrategy(value);
            return this;
        }
        public AttackBuilder AttackabilityService(AttackabilityService attackabilityService) {
            this.attackabilityService = attackabilityService;
            return this;
        }
        public AttackBuilder CanHitSameTarget(bool value) {
            this.canHitSameTarget = value;
            return this;
        }
        public AttackBuilder HitTicksPerSecond(double ticksPerSecond)
        {
            this.secondsBetween2Hits = 1/ticksPerSecond;
            return this;
        }
        private Attack CreateAttack(IAttacking attacker, IPositionUnit position)
        {
            var creationParams = new AttackCreationParams
            {
                Attacker = attacker,
                InitialPosition = position,
                AttackStrategy = attackStrategy,
                MovementStrategy = movementStrategy,
                AttackLifetimeOverStrategy = attackLifetimeOverStrategy,
                World = world,
                AttackabilityService = attackabilityService,
                LifeTimeInSeconds = lifeTimeInSeconds,
                NumberOfMaxTargetsInOneStep = numberOfMaxTargetsInOneStep,
                NumberOfMaxTargets = numberOfMaxTargets,
                SecondsBetween2Hits = secondsBetween2Hits,
                CanHitSameTarget = canHitSameTarget
            };
            Attack attack = new Attack(creationParams);
            CreatedEvent?.Invoke(attack);
            return attack;

        }
        public Attack CreateAttack(IPositionUnit position)
        {
            if (attacker == null) throw new RequiredParameterNull("attacker was null");
            return CreateAttack(attacker, position);
        }
        public Attack CreateInFrontOf(Unit unit, double distanceFromUnit)
        {
            attacker = unit;
            var temp = unit.Position.Copy();
            temp.MoveBy(unit.LastMovement.WithLength(distanceFromUnit));
            initialPosition = temp;
            return CreateAttack(attacker, initialPosition);
        }
        public Attack CreateTargeted(Unit target)
        {
            return CreateAttack(attacker, target.Position);
        }
    }
}
