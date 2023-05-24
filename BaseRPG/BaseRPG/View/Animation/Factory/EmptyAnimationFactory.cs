using BaseRPG.Model.Exceptions;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Factory
{
    internal class EmptyAnimationFactory: IAttackAnimationFactory
    {
        public ImageSequenceAnimation CreateImageSequence(IAttackFactory attackFactory)
        {
            throw new NoSuchAttackBuilderException();
        }

        public TransformationAnimation2D CreateTransformation(IAttackFactory attackFactory)
        {
            throw new NoSuchAttackBuilderException();
        }
    }
}
