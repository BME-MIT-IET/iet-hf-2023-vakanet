using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Movement
{
    public interface IPhysicsFactory
    {
        public static IPhysicsFactory Instance {get;set;}
        IMovementManager CreateMovementManager(IPositionUnit initialPosition);
        IMovementManager CreateMovementManager( );
        IMovementUnit CreateMovement(params double[] args);
        IPositionUnit CreatePosition(params double[] args);
        IPositionUnit Origin { get; }
    }
}
