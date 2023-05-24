using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Physics.TwoDimensional.Collision.Ray;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Spatial;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    public interface IShape2D
    {
        IMovementManager MovementManager { get; set; }

        public Vector2D GlobalPosition { get; }
        public ICollisionDetector Owner { get; set; }
        public Vector2D Middle { get; }
        void Rotate(double angle);
        Polygon Rotated(double angle);
        Polygon2D ToPolygon2D();
        Polygon ToPolygon();
        bool IsColliding(IShape2D s2);
        //bool IsCollidingCircle(Circle circleSector);
        bool IsCollidingPoint(Vector2D point);
        bool IsColliding(Polygon2D polygon2);
        IShape2D Shifted(Vector2D shift);
        IShape2D ShiftedByPos { get; }
        Vector2D LastCalculatedMiddle { get; }
        double RotationAngle { get; }

        IShape2D Shifted(params double[] values);
        void OnCollision(IShape2D shape2, double delta)
        {
            shape2.Owner.OnCollision(Owner, delta);
        }

        RayCollection CastRays(Vector2D movementVector, int numberOfRays);
    }
}
