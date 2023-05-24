using BaseRPG.Model.Data;
using System;

namespace BaseRPG.Model.Interfaces.Movement
{
    public interface IMovementManager
    {
        void QueueMovement(IMovementUnit movement);
        public event Action<IMovementBlockingStrategy> Moved;
        IMovementUnit LastMovement { get; }
        IPositionUnit Position { get; }
        IMovementBlockingStrategy MovementBlockingStrategy { get; set; }
        void MoveQueued();
        void Move(IMovementUnit movement);
        IMovementManager Copy();
        
    }
}