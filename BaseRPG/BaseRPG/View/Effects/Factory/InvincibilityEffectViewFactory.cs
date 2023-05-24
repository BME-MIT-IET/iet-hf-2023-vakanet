using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Model.Effects;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Effects.Factory
{
    public class InvincibilityEffectViewFactory : IEffectViewFactory
    {
        private EffectViewCreationParams creationParams;

        public InvincibilityEffectViewFactory(EffectViewCreationParams creationParams)
        {
            this.creationParams = creationParams;
        }

        public EffectView Create()
        {
            var timeBetweenFrames = 0.5;
            var distanceOffsetTowardsPointer = 0;
            return new EffectView.Builder(creationParams.Effect, creationParams.MovementManager,
                ImageSequenceAnimation.LoopingAnimation(creationParams.ImageProvider, creationParams.AnimationProvider.Get("invincibility-effect"), timeBetweenFrames))
                .DefaultTransformationAnimation(
                    new FacingPointOnCallbackAnimation(distanceOffsetTowardsPointer,
                        PositionObserver.CreateForLastMovement(creationParams.MovementManager, GameConfigurer.VERY_LARGE_NUMBER)
                        )
                    )
                .Build();
        }
    }
}
