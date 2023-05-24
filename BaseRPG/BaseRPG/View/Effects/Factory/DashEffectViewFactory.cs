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
    public class DashEffectViewFactory : IEffectViewFactory
    {
        private EffectViewCreationParams creationParams;
        private double time;

        public DashEffectViewFactory(EffectViewCreationParams creationParams, double time)
        {
            this.creationParams = creationParams;
            this.time = time;
        }

        public EffectView Create()
        {
            var distanceOffsetTowardsPointer = -25;
            var animationStrings = creationParams.AnimationProvider.Get("dash-effect");
            var imageSequenceAnimation = ImageSequenceAnimation
                .WithTimeFrameHoldLastItem(creationParams.ImageProvider, animationStrings, time);
            var lastMovementPositionProvider = PositionObserver
                .CreateForLastMovement(creationParams.MovementManager, GameConfigurer.VERY_LARGE_NUMBER);
            var tansformAnimation = new FacingPointOnCallbackAnimation(
                distanceOffsetTowardsPointer * App.IMAGE_SCALE,
                lastMovementPositionProvider
            );
            var builder = new EffectView.Builder(
                creationParams.Effect,
                creationParams.MovementManager,
                imageSequenceAnimation
            );
            return builder
                .DefaultTransformationAnimation(tansformAnimation)
                .Build();
        }
    }
}
