using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity.Enemy
{
    public class Enemy : Unit
    {
        protected override string Type { get { return "Enemy"; } }
        private readonly Dictionary<string, AttackBuilder> attacks;
        private InRangeDetector inRangeDetector;
        private Unit target;
        
        public double XpValue { get; private set; }
        public int GoldValue { get; private set; }

        public event Action<IAttackable> AttackableInRange;

        public override AttackabilityService.Group OffensiveGroup => AttackabilityService.Group.Enemy;

        public override AttackabilityService.Group DefensiveGroup => OffensiveGroup;

        public Unit Target => target;

        public InRangeDetector InRangeDetector => inRangeDetector; 

        private Enemy(
            int maxHp,
            IMovementManager movementManager,
            IMovementStrategy movementStrategy,
            Unit target,
            Dictionary<string, AttackBuilder> attacks,
            InRangeDetector inRangeDetector,
            SkillManager skillManager,
            World world,
            double xpValue,
            int goldValue)
            : base(maxHp, movementManager, movementStrategy, skillManager, world)
        {
            this.target = target;
            this.attacks = attacks;
            this.inRangeDetector = inRangeDetector;
            inRangeDetector.SetExists(true);
            //inRangeDetector.OnInRange += OnInRange;
            inRangeDetector.OnExitedRange += g => StartMoving();
            XpValue = xpValue;
            GoldValue = goldValue;
        }
        public void OnTargetDead(){
            this.target = null;
            DefaultMovementStrategy = new EmptyMovementStrategy();
            MovementStrategy = DefaultMovementStrategy;
        }
        public override void OnCollision(ICollisionDetector gameObject,double delta) 
        {
            
        }
        public void OnInRange(ICollisionDetector gameObject) {
            if (gameObject == this) return;
            
        }
        public override void Step(double delta)
        {
            base.Step(delta);
            if (inRangeDetector.IsInRange(target)) 
            {
                if (target is IAttackable)
                {
                    var canAttack = new AttackabilityService.Builder().CreateByDefaultMapping().CanAttack(this, target as IAttackable);
                    if (canAttack)
                    {
                        StopMoving();
                        AttackableInRange?.Invoke(target as IAttackable);
                    }
                }
            }
        }
        protected override void OnExistsSet(bool value) { 
            inRangeDetector.SetExists(value);
        }
        public Attack Attack(string v) {
            if (Target == null) 
                return null;
            var attack = attacks[v].Attacker(this).CreateTargeted(Target);
            return attack;
        }
        public override IAttackFactory AttackFactory(string v)
        {
            throw new NotImplementedException();
            //return attacks[v];
        }

        public override void OnTargetKilled(IAttackable target)
        {
            target.OnKilledByEnemy(this);
            if (target == this.target) {
                OnTargetDead();
            }
        }

        public override void OnKilledByHero(Hero.Hero hero)
        {
            hero.OnEnemyKilled(this);
        }

        public override void OnKilledByEnemy(Enemy enemy)
        {
            throw new NotImplementedException();
        }

        public class EnemyBuilder:Builder {
            private readonly Dictionary<string, AttackBuilder> attacks = new();
            private InRangeDetector inRangeDetector;
            private Unit target;
            private double xpValue = 1;
            private int goldValue = 1;

            public EnemyBuilder(
                int maxHp, IMovementManager movementManager,
                Unit target,
                 World world):base(maxHp,movementManager, new FollowingMovementStrategy(target.MovementManager), world)
            {
                this.target = target;
                this.inRangeDetector = new InRangeDetector(world);
            }
            public Builder Attack(string name, AttackBuilder builder) { 
                attacks.Add(name, builder);
                return this;
            }
            public Builder Attack(string name, IAttackStrategy strategy)
            {
                attacks.Add(name, new AttackBuilder(strategy).World(this.World));
                return this;
            }
            public Builder XpValue(double value) { 
                xpValue = value;
                return this;
            }
            public Builder GoldValue(int value)
            {
                goldValue  = value;
                return this;
            }
            public override Enemy Build(int maxHp, IMovementManager movementManager, IMovementStrategy movementStrategy, SkillManager skillManager, World world)

            {
                
                Enemy enemy = new Enemy(maxHp,movementManager,movementStrategy,target,attacks,inRangeDetector,skillManager,world,xpValue,goldValue);
                inRangeDetector.SetExists(true);
                target.OnCeaseToExist += enemy.OnTargetDead;
                //inRangeDetector.OnInRange += OnInRange;
                inRangeDetector.OnExitedRange += g => enemy.StartMoving();
                return enemy;
            }

        }

    }
}
