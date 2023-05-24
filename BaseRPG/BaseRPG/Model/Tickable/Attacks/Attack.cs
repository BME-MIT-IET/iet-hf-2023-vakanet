using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;

namespace BaseRPG.Model.Tickable.Attacks
{
    public class AttackCreationParams
    { 
        private IAttacking attacker;
        private IPositionUnit initialPosition;
        private IAttackStrategy attackStrategy;
        private IMovementStrategy movementStrategy;
        private IAttackLifetimeOverStrategy attackLifetimeOverStrategy;
        private World world;
        private AttackabilityService attackabilityService;
        private double lifeTimeInSeconds;
        private int numberOfMaxTargetsInOneStep;
        private int numberOfMaxTargets;
        private double secondsBetween2Hits;
        private bool canHitSameTarget;

        public IAttacking Attacker { get => attacker; init => attacker = value; }
        public IPositionUnit InitialPosition { get => initialPosition; init => initialPosition = value; }
        public IAttackStrategy AttackStrategy { get => attackStrategy; init => attackStrategy = value; }
        public IMovementStrategy MovementStrategy { get => movementStrategy; init => movementStrategy = value; }
        public IAttackLifetimeOverStrategy AttackLifetimeOverStrategy { get => attackLifetimeOverStrategy; init => attackLifetimeOverStrategy = value; }
        public World World { get => world; init => world = value; }
        public AttackabilityService AttackabilityService { get => attackabilityService; init => attackabilityService = value; }
        public double LifeTimeInSeconds { get => lifeTimeInSeconds; init => lifeTimeInSeconds = value; }
        public int NumberOfMaxTargetsInOneStep { get => numberOfMaxTargetsInOneStep; init => numberOfMaxTargetsInOneStep = value; }
        public int NumberOfMaxTargets { get => numberOfMaxTargets; init => numberOfMaxTargets = value; }
        public double SecondsBetween2Hits { get => secondsBetween2Hits; init => secondsBetween2Hits = value; }
        public bool CanHitSameTarget { get => canHitSameTarget; init => canHitSameTarget = value; }
    }
    public class Attack : GameObject, ICollisionDetector
    {
        private IAttacking attacker;
        private IMovementManager movementManager;
        private IAttackStrategy attackStrategy;
        private readonly IMovementStrategy movementStrategy;
        private readonly IAttackLifetimeOverStrategy attackLifetimeOverStrategy;
        private readonly AttackabilityService attackabilityService;
        private double lifeTimeInSeconds;
        private readonly Default<int> numberOfMaxTargetsInOneStep = new(0);
        private int numberOfMaxTargets = 0;
        private bool canHitSameTarget;
        private DefaultComparable<double> secondsBetween2Hits;
        private HashSet<object> targetsHit = new();
        public Attack(AttackCreationParams creationParams) : base(creationParams.World)
        {
            this.attacker = creationParams.Attacker;
            movementManager = IPhysicsFactory.Instance
                .CreateMovementManager(creationParams.InitialPosition);
            this.attackStrategy = creationParams.AttackStrategy;
            this.movementStrategy = creationParams.MovementStrategy;
            this.attackLifetimeOverStrategy = creationParams.AttackLifetimeOverStrategy;
            this.attackabilityService = creationParams.AttackabilityService;
            this.lifeTimeInSeconds = creationParams.LifeTimeInSeconds;
            this.numberOfMaxTargetsInOneStep = new(creationParams.NumberOfMaxTargetsInOneStep);
            this.numberOfMaxTargets = creationParams.NumberOfMaxTargets;
            this.canHitSameTarget = creationParams.CanHitSameTarget;
            this.secondsBetween2Hits = new(creationParams.SecondsBetween2Hits, 0);
        }

        public IPositionUnit Position { get => movementManager.Position; }

        public override bool Exists { get =>
                !attackLifetimeOverStrategy.IsOver(this);
        }

        public IMovementManager MovementManager { get { return movementManager; } }

        public double LifeTime { get => lifeTimeInSeconds; }
        public int NumberOfRemainingMaxTargets { get => numberOfMaxTargets; }

        public override event Action OnCeaseToExist;

        public bool OnAttackHit(IAttackable attackable, double delta)
        {
            if (attacker == null) return false;
            if (!attackabilityService.CanAttack(attacker, attackable)) return false;
            attackStrategy.OnAttackHit(attacker, attackable, delta);
            return true;
        }

        public void OnCollision(ICollisionDetector other, double delta)
        {
           
            if (shouldReturnInstantly(other)) return;
            
            var success = OnAttackHit(other as IAttackable, delta);
            if (success) onSuccess(other,delta);
                
            
        }

        public override void Step(double delta)
        {
            lifeTimeInSeconds -= delta;
            secondsBetween2Hits.CurrentValue -= delta;
            numberOfMaxTargetsInOneStep.Reset();
            IMovementUnit movementUnit = movementStrategy.CalculateNextMovement(movementManager, 1)?.Scaled(delta);
            movementManager.Move(movementUnit);
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }

        public bool CanCollide(ICollisionDetector other)
        {
            return true;
        }

        private bool shouldReturnInstantly(ICollisionDetector other) {
            if (numberOfMaxTargetsInOneStep.CurrentValue <= 0) return true;
            if (numberOfMaxTargets <= 0) return true;
            if (!(other is IAttackable)) return true;
            if (!(secondsBetween2Hits.CurrentValue <= 0)) return true;
            if(!canHitSameTarget)
                if(targetsHit.Contains(other))
                    return true;
            return false;
        }
        private void onSuccess(object other, double delta)
        {
            targetsHit.Add(other);
            --numberOfMaxTargetsInOneStep.CurrentValue;
            --numberOfMaxTargets;
            secondsBetween2Hits.Reset();
        }
    }
}
