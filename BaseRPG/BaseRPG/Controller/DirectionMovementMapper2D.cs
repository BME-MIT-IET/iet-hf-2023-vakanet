using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller
{
    public class DirectionMovementUnitMapper
    {
        private Dictionary<MoveDirection, IMovementUnit> map;
        public DirectionMovementUnitMapper(Dictionary<MoveDirection, IMovementUnit> map)
        {
            this.map = map;
        }
        public static DirectionMovementUnitMapper CreateDefault2D() {
            DirectionMovementUnitMapper directionVectorMapper =
                new DirectionMovementUnitMapper(new Dictionary<MoveDirection, IMovementUnit>());
            PhysicsFactory2D physicsFactory = new();
            directionVectorMapper.map.Add(    MoveDirection.Left, physicsFactory.CreateMovement(-1,  0));
            directionVectorMapper.map.Add(   MoveDirection.Right, physicsFactory.CreateMovement( 1,  0));
            directionVectorMapper.map.Add( MoveDirection.Forward, physicsFactory.CreateMovement( 0, -1));
            directionVectorMapper.map.Add(MoveDirection.Backward, physicsFactory.CreateMovement( 0,  1));
            return directionVectorMapper;
        }

        public IMovementUnit FromDirection(MoveDirection moveDirection)
        {
            return map[moveDirection];
        }
    }
}
