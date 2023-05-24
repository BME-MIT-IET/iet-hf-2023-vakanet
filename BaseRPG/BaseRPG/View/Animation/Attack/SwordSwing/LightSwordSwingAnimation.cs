using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Attack.SwordSwing
{
    public class LightSwordSwingAnimation : SwordSwingAnimation
    {

        public LightSwordSwingAnimation(Angle startingAngle,IMovementAngleCalculationStrategy movementAngleCalculationStrategy, double lastFrameHoldDurationInSeconds)
        :base(startingAngle, movementAngleCalculationStrategy, lastFrameHoldDurationInSeconds)
        {
        }


        #region private functions
        protected override Vector2D calculateFirstPointOffset()
        {

            return Vector2D.FromPolar(
                (CalculatedMovementAngle + AngleRange.Radians / 2) * 30,
                StartingAngle);

        }
        protected override double calculateDistanceOffsetTowardsPointer()
        {

            return 32.5 * App.IMAGE_SCALE
            - (CalculatedMovementAngle + AngleRange.Radians / 2) * 5 * App.IMAGE_SCALE;
        }
        
        #endregion
    }
}
