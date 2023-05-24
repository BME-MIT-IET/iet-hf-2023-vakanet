using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Collision.Ray;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public class ShapeMovementBlockingStrategy2D : IMovementBlockingStrategy
    {
        private readonly IShape2D shape;
        private readonly CollisionNotifier2D collisionNotifier2D;
        public ShapeMovementBlockingStrategy2D(IShape2D shape, CollisionNotifier2D collisionNotifier2D)
        {
            this.shape = shape;
            this.collisionNotifier2D = collisionNotifier2D;
        }
        private bool canStepThere(IShape2D shiftedShape) {
            var shapes = collisionNotifier2D.ShapesCollidingWith(shiftedShape);
            bool result = true;
            foreach (IShape2D shape in shapes) {
                if (!shape.Owner.CanBeOver)
                    result = false;
            }
            return result;
        }
        public IMovementUnit GenerateMovement(IMovementUnit movement, IPositionUnit position)
        {
            var movementVector = MovementUnit2D.ToVector2D(movement);
            var shiftedShape = shape.ShiftedByPos;
            if (canStepThere(shiftedShape.Shifted(movementVector))) return movement;
            var interval = FindLargestWithBinary(
                0, movementVector.Length,
                middle => !canStepThere(shiftedShape.Shifted(movementVector * middle)),
                10);
            return movement.Scaled(interval.Item1);
        }

        //Not used right now
        public IMovementUnit GenerateMovementWithRays(IMovementUnit movement, IPositionUnit position)
        {
            if (!(shape.Owner is Hero)) return movement;
            Vector2D movementVector = MovementUnit2D.ToVector2D(movement);
            //angle to the y axis

            var rays = shape.CastRays(movementVector.Normalize(),3);
            var ray2 = rays.Rays[1];
            var ray = new NormalRay(shape.ShiftedByPos.Middle,movementVector.Normalize());
            if (!ray.Origin.Equals(ray2.Origin, 0.00001)) {
            
            }
            double shortestDistance = movementVector.Length;
            //var shapes = collisionNotifier2D.ShapesCollidingWith(shape.ShiftedByPos.Shifted(MovementUnit2D.ToVector(movement)));
            var shapes = collisionNotifier2D.Shapes;
            foreach (var s in shapes)
            {

                if (shape.Owner == s.Owner) continue;
                if (!shape.Owner.CanCollide(s.Owner) || !s.Owner.CanCollide(shape.Owner))
                {
                    //return new MovementUnit2D(-1*(s.GlobalPosition - shape.GlobalPosition).Normalize());
                    var other = s.ShiftedByPos.ToPolygon2D();
                    //var intersections = rays.Intersections(other);
                    //var intersection = intersections[1];//.ClosestIntersectionDistance();
                    var intersection = ray.Intersect(other);

                    var closest = intersection.ClosestInFront();
                    if (closest != null)
                    {

                        var length = (intersection.Ray.Origin - closest.Value).Length;
                        if (length < shortestDistance)
                        {
                            shortestDistance = length;
                            //Console.WriteLine($"new shortest distance: {shortestDistance}");
                        }
                    }
                    //var intersection = rays.Intersections(s.ShiftedByPos.ToPolygon2D()).ClosestIntersectionDistance();
                    //if (intersection < shortestDistance) {
                    //    shortestDistance = intersection;
                    //}

                }
            }
            if (shortestDistance < movementVector.Length-0.000001) {
                shortestDistance -= movementVector.Length * 0.1;
            }
            return movement.WithLength(shortestDistance );
        }

        public static (double,double) FindLargestWithBinary(double intervalBegin, double intervalEnd, Func<double,bool> condition, int depth) {
            if (depth <= 0) return (intervalBegin,intervalEnd);
            double interval = intervalEnd - intervalBegin;
            var middle = intervalBegin + interval / 2;
            if (condition(middle)) return FindLargestWithBinary(intervalBegin, middle, condition, depth - 1);
            return FindLargestWithBinary(middle, intervalEnd,condition,depth-1);
        }

        public double CalculateTurnAngle(IPositionUnit position, double turnAngle)
        {
            var shiftedRotatedShape = shape.Rotated(turnAngle).ShiftedByPos;
            var shapes = collisionNotifier2D.ShapesCollidingWith(shiftedRotatedShape);
            if (canStepThere(shiftedRotatedShape)) {
                return turnAngle;
            }
            var epsilon = 0.1;
            var interval = FindLargestWithBinary(0, turnAngle, middle => !canStepThere(shape.Rotated(middle).ShiftedByPos), 10);
            return (interval.Item1 + interval.Item2)/(2 + epsilon);
        }
    }
}
