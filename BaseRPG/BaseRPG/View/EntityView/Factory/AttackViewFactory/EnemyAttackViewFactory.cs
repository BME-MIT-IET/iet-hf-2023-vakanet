using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.View.Interfaces.Factory;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView.Factory.AttackViewFactory
{
    internal class EnemyAttackViewFactory : IAttackViewFactory
    {
        private readonly Attack attack;
        private readonly IImageProvider imageProvider;
        private readonly Enemy enemy;

        public EnemyAttackViewFactory(Attack attack, IImageProvider imageProvider, Enemy enemy)
        {
            this.attack = attack;
            this.imageProvider = imageProvider;
            this.enemy = enemy;
        }

        public AttackView Create()
        {
            return new AttackView.Builder(@"Assets\image\attacks\enemy-attack.png", attack)
                .ImageProvider(imageProvider)
                .Rotated(true)
                .OwnerPosition(enemy.Position)
                .Create();
        }
    }
}
