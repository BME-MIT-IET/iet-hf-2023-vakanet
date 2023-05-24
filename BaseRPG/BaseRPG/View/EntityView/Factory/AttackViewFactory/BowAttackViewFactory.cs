using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView.Factory.AttackViewFactory
{

    public class BowAttackViewFactory : WeaponAttackViewFactory
    {
        public BowAttackViewFactory(IImageProvider imageProvider) : base(imageProvider)
        {
        }

        protected override AttackView Create(WeaponAttackCreationParams creationParams, IImageProvider imageProvider)
        {
            return new AttackView.Builder(@"Assets\image\attacks\arrow-outlined.png")
                .ImageProvider(imageProvider)
                .Attack(creationParams.Attack)
                .OwnerPosition(creationParams.OwnerPosition)
                .Rotated(true)
                .Create();
        }
    }
}
