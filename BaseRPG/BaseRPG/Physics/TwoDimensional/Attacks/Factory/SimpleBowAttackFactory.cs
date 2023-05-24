using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
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
    public class SimpleBowAttackFactory : AttackFactory2D
    {
        public override AttackBuilder ConfigureHeavy(Vector2D movementDirection)
        {
            return new AttackBuilder(new DamagingAttackStrategy(50))
                .AttackLifetimeOverStrategy(new DestroyAfterAllTargetsHitStrategy())
                .NumberOfMaxTargets(10)
                .CanHitSameTarget(false)
                .LifeTimeInSeconds(1)
                .MovementStrategy(new StraightMovementStrategy2D(movementDirection * 40 * 100));
        }

        public override AttackBuilder ConfigureLight(Vector2D movementDirection)
        {
            return new AttackBuilder(new DamagingAttackStrategy(20))
                .AttackLifetimeOverStrategy(new DestroyAfterAllTargetsHitStrategy())
                .NumberOfMaxTargets(1)
                .LifeTimeInSeconds(1)
                .MovementStrategy(new StraightMovementStrategy2D(movementDirection * 10 * 100));
        }
    }
}
