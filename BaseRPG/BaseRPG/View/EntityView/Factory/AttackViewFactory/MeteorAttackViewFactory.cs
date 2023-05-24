using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.View.Interfaces.Factory;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView.Factory.AttackViewFactory
{
    public class MeteorAttackViewFactory : IAttackViewFactory
    {
        private readonly Attack attack;
        private readonly IImageProvider imageProvider;

        public MeteorAttackViewFactory(Attack attack, IImageProvider imageProvider)
        {
            this.attack = attack;
            this.imageProvider = imageProvider;
        }

        public AttackView Create()
        {
            return new AttackView.Builder(@"Assets\image\effects\meteor\lava-ground-circle.png", attack)
                .ImageProvider(imageProvider)
                .Rotated(false)
                .SecondsVisibleAfterDestroyed(0)
                .Create();
        }
    }
}
