using BaseRPG.Model.Effects;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces.Factory
{
    public class EffectViewCreationParams
    {
        private Effect effect;
        private IMovementManager movementManager;
        private IImageProvider imageProvider;
        private AnimationProvider animationProvider;

        public Effect Effect { get => effect; init => effect = value; }
        public IMovementManager MovementManager { get => movementManager; init => movementManager = value; }
        public IImageProvider ImageProvider { get => imageProvider; init => imageProvider = value; }
        public AnimationProvider AnimationProvider { get => animationProvider; init => animationProvider = value; }
    }
    public interface IEffectViewFactory
    {
        EffectView Create();
    }
}
