using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView.Factory.AttackViewFactory
{
    internal class DaggerAttackViewFactory : WeaponAttackViewFactory
    {
        public DaggerAttackViewFactory(IImageProvider imageProvider) : base(imageProvider)
        {
        }

        protected override AttackView Create(WeaponAttackCreationParams creationParams, IImageProvider imageProvider)
        {
            return new AttackView.Builder(@"Assets\image\attacks\dagger-attack-effect.png")
                .ImageProvider(imageProvider)
                .Attack(creationParams.Attack)
                .OwnerPosition(creationParams.OwnerPosition)
                .Rotated(true)
                .SecondsVisibleAfterDestroyed(0.2)
                .Create();
        }
    }
}
