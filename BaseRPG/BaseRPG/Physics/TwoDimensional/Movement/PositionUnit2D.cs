using BaseRPG.Model.Interfaces.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public class PositionUnit2D : IPositionUnit
    {
        private Point2D position;

        public PositionUnit2D(Point2D position)
        {
            this.position = position;
        }
        public PositionUnit2D(Vector2D position)
        {
            this.position = new(position.X,position.Y);
        }
        public PositionUnit2D(double x, double y)
        {
            this.position = new Point2D(x, y);
        }

        public double[] Values => new double[]{position.X,position.Y};

        public IPositionUnit Copy()
        {
            return new PositionUnit2D(position);
        }

        public void MoveBy(IMovementUnit movementUnit)
        {
            position = new Point2D(position.X + movementUnit.Values[0], position.Y + movementUnit.Values[1]);
        }

        public IMovementUnit MovementTo(IPositionUnit positionUnit)
        {
            Vector2D pos1 = new(positionUnit.Values[0], positionUnit.Values[1]);
            return new MovementUnit2D(pos1-position.ToVector2D());
        }
        public static Vector2D ToVector2D(IPositionUnit position) {
            var values = position.Values;
            return new(values[0],values[1]);
        }
       
    }
}
