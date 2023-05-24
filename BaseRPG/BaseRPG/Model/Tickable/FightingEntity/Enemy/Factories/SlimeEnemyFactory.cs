using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces.Factories.Enemy;
using BaseRPG.Model.Interfaces.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity.Enemy.Factories
{
    public class SlimeEnemyFactory : IEnemyFactory
    {
        private IPhysicsFactory physicsFactory;
        private Hero.Hero hero;
        private double speed;
        private readonly int hp;

        public SlimeEnemyFactory(IPhysicsFactory physicsFactory, Hero.Hero hero, double speed, int hp)
        {
            this.physicsFactory = physicsFactory;
            this.hero = hero;
            this.speed = speed;
            this.hp = hp;
        }

        public Enemy Create(IPositionUnit position)
        {
            return new Enemy.EnemyBuilder(
                hp, physicsFactory.CreateMovementManager(position),
                hero, hero.CurrentWorld).
                Attack("normal", new DamagingAttackStrategy(1))
                .Speed(speed)
                .Build() as Enemy;
        }
    }
}
