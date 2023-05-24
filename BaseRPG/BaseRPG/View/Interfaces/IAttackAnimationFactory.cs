using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation.ImageSequence;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces
{
    public interface IAttackAnimationFactory
    {
        Vector2D MousePositionOnScreen { set { } }
        TransformationAnimation2D CreateTransformation(IAttackFactory attackFactory);
        ImageSequenceAnimation CreateImageSequence(IAttackFactory attackFactory);
    }
}
