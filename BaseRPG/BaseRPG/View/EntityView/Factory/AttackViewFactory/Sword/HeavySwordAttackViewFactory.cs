using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.Factory.HeavyAttackAnimation.Strike;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView.Factory.AttackViewFactory.Sword
{
    public class HeavySwordAttackViewFactory : WeaponAttackViewFactory
    {
        private AnimationProvider animationProvider;

        public HeavySwordAttackViewFactory(IImageProvider imageProvider) : base(imageProvider)
        {
            animationProvider = AnimationProvider.CreateDefault();
        }

        protected override AttackView Create(WeaponAttackCreationParams creationParams, IImageProvider imageProvider)
        {
            var time = SwordHeavyStrikeAnimationFactory.HEAVY_SWORD_SWING_SECONDS;
            var imageAnimation = ImageSequenceAnimation.WithTimeFrameHoldLastItem(imageProvider, animationProvider.Get("sword-heavy-attack"), time);
            return new AttackView.Builder()
                .ImageProvider(imageProvider)
                .Attack(creationParams.Attack)
                .OwnerPosition(creationParams.OwnerPosition)
                .Rotated(true)
                .WithImageSequenceAnimation(imageAnimation)
                .SecondsVisibleAfterDestroyed(0.5)
                .Create();
        }
    }
}
