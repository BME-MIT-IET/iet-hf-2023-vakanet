using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Attacks.Factory
{
    internal class SimpleSwordAttackFactory : AttackFactory2D
    {
        public override AttackBuilder ConfigureHeavy(Vector2D movementDirection)
        {
            return new AttackBuilder(new DamagingAttackStrategy(100))
                .LifeTimeInSeconds(0.1)
                .AttackLifetimeOverStrategy(new LifetimeOverStrategy())
                .CanHitSameTarget(false);
        }

        public override AttackBuilder ConfigureLight(Vector2D movementDirection)
        {
            return new AttackBuilder(new DamagingAttackStrategy(40));
        }

    }
}
