using BaseRPG.Model.Combat;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Attacks.Factory
{
    internal class SimpleDaggerAttackFactory : AttackFactory2D
    {
        public override AttackBuilder ConfigureHeavy(Vector2D movementDirection)
        {
            return new AttackBuilder(new DamagingAttackStrategy(80));
        }

        public override AttackBuilder ConfigureLight(Vector2D movementDirection)
        {
            return new AttackBuilder(new DamagingAttackStrategy(70));
        }
    }
}
