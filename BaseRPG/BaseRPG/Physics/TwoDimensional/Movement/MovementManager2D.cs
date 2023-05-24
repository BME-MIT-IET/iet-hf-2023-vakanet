using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{

    public class MovementManager2D : IMovementManager
    {
        private PositionUnit2D position;
        private IMovementUnit lastMovement;
        private List<IMovementUnit> queuedMovements = new();
        private IMovementBlockingStrategy movementBlockingStrategy;
        public event Action<IMovementBlockingStrategy> Moved;
        public MovementManager2D(Vector2D vector, IMovementBlockingStrategy movementBlockingStrategy): this(new PositionUnit2D(vector), movementBlockingStrategy)
        {
        }
        public MovementManager2D(double x, double y, IMovementBlockingStrategy movementBlockingStrategy):this(new PositionUnit2D(x, y),movementBlockingStrategy)
        {

        }
        public MovementManager2D(PositionUnit2D position, IMovementBlockingStrategy movementBlockingStrategy)
        {
            this.position = position;
            this.movementBlockingStrategy = movementBlockingStrategy;
        }

        public IPositionUnit Position { get { return position; } }
        public void Move(IMovementUnit movement)
        {
            if (movement == null) return;
            if (almostZeroLength(movement)) return;

            //var movementBefore = toVector2D(movement);
            movement = movementBlockingStrategy.GenerateMovement(movement, position);
            //var movementAfter = toVector2D(movement);
            //if(movementBefore.DotProduct(movementAfter) >= 0)
            lastMovement = movement;
            position.MoveBy(movement);
            Moved?.Invoke(movementBlockingStrategy);
        }

        private bool almostZeroLength(IMovementUnit movement) {
            return Math.Abs(movement.Values[0]) < 0.00001 &&
                Math.Abs(movement.Values[1]) < 0.00001;
        }
        public IMovementUnit LastMovement
        {
            get
            {
                if (lastMovement == null) {
                    return new MovementUnit2D(0, 0);
                }
                return lastMovement;
            }
        }

        public IMovementBlockingStrategy MovementBlockingStrategy { get => movementBlockingStrategy; set => movementBlockingStrategy = value; }
        private MovementManager2D(IPositionUnit position, IMovementUnit lastMovement, IMovementBlockingStrategy movementBlockingStrategy)
        {
            this.position = new(position.Values[0], position.Values[1]);
            this.lastMovement = lastMovement;
            this.movementBlockingStrategy = movementBlockingStrategy;
        }

        public IMovementManager Copy()
        {
            return new MovementManager2D(position.Copy(),LastMovement.Clone(), movementBlockingStrategy);
        }

        public void QueueMovement(IMovementUnit movement)
        {
            if (movement == null) 
                movement = new MovementUnit2D(0,0);
            queuedMovements.Add(movement);
        }

        public void MoveQueued()
        {
            Vector2D movementVector = new();
            foreach (var movement in queuedMovements){
                movementVector += new Vector2D(movement.Values[0],movement.Values[1]);
            }
            Move(new MovementUnit2D(movementVector));
            queuedMovements.Clear();
        }
    }
}
