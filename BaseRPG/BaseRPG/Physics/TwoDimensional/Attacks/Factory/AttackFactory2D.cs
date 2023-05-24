using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Worlds;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Attacks.Factory
{
    public abstract class AttackFactory2D : IAttackFactory
    {
        public IAttacking Owner { get ; set; }
        public World World { get ; set ; }

        public event Action<Attack> LightAttackCreated;
        public event Action<Attack> HeavyAttackCreated;


        public Attack CreateHeavy(IPositionUnit position, IMovementUnit movementDirection)
        {
            var result = configureAttack(movementDirection, ConfigureHeavy)
                .World(World)
                .Attacker(Owner)
                .CreateAttack(position);
            HeavyAttackCreated?.Invoke(result);
            return result;
        }

        public Attack CreateLight(IPositionUnit position, IMovementUnit movementDirection)
        {
            var result = configureAttack(movementDirection, ConfigureLight)
                .World(World)
                .Attacker(Owner)
                .CreateAttack(position);
            LightAttackCreated?.Invoke(result);
            return result;
        }
        public abstract AttackBuilder ConfigureHeavy(Vector2D movementDirection);
        public abstract AttackBuilder ConfigureLight(Vector2D movementDirection);

        private AttackBuilder configureAttack(IMovementUnit movementDirection,Func<Vector2D, AttackBuilder> creatorFunc) {
            //var position2D = Movement.PositionUnit2D.ToVector2D(position);
            var direction2D = Movement.MovementUnit2D.ToVector2D(movementDirection);
            return creatorFunc(direction2D.Normalize());
        }
    }
}
