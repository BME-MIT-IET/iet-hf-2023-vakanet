using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Utility;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Collision.Ray;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    public static class Polygon2DExtension {
        public static Polygon2D Transform(this Polygon2D source, Matrix<double> m) {
            return new Polygon2D(source.Vertices.Select(v => v.TransformBy(m)));
        }
        public static Point2D ToPoint2D(this Vector2D source)
        {
            return new(source.X,source.Y);
        }
    }
    public class Polygon : IShape2D
    {

        private Vector2D lastMiddle;
        private Polygon2D polygon;
        private double angle;
        private ICollisionDetector owner;
        private IMovementManager movementManager;

        public Polygon(ICollisionDetector owner, IMovementManager movementManager, IEnumerable<Point2D> vertices)
        {
            polygon = new Polygon2D(vertices);
            this.owner = owner;
            this.MovementManager = movementManager;
            if (movementManager != null)
                movementManager.Moved += (s) =>
                {
                    var movementVector = MovementUnit2D.ToVector2D(movementManager.LastMovement);
                    if (movementVector.Length < 0.000001) return;
                    Angle newAngle = movementVector.SignedAngleTo(new(0, 1), true);
                    SetRotation(newAngle.Radians,s);
                };
        }

        private Polygon(ICollisionDetector owner, IMovementManager movementManager, double angle,Vector2D lastMiddle,Polygon2D polygon)
        {
            this.owner = owner;
            this.MovementManager = movementManager;
            this.polygon = polygon;
            this.angle = angle;
            this.lastMiddle = lastMiddle;
        }
        public static Polygon ForUnit(Unit unit, IEnumerable<Point2D> vertices, CollisionNotifier2D collisionNotifier2D) {
            var result = new Polygon(unit, unit.MovementManager, vertices);
            result.MovementManager.MovementBlockingStrategy = new ShapeMovementBlockingStrategy2D(result, collisionNotifier2D);
            return result;
        }
        public Vector2D Middle
        {
            get
            {

                Vector2D sum = new Vector2D(0, 0);
                foreach (Point2D p in polygon.Vertices)
                {
                    var v = new Vector2D(p.X, p.Y);
                    sum += v;
                }
                lastMiddle = sum / polygon.VertexCount;
                return lastMiddle;
            }

        }
        public Vector2D LastCalculatedMiddle => lastMiddle;
        public ICollisionDetector Owner
        {
            get { return owner; }
            set { owner = value; }
        }


        public Vector2D GlobalPosition {
            get
            {
                var values = MovementManager.Position.Values;
                return new(values[0], values[1]);
            }
        }
        public IMovementManager MovementManager { get => movementManager; set => movementManager = value; }

        public void Rotate(double angle)
        {
            this.angle += angle;
            polygon = polygon.RotateAround(Angle.FromRadians(angle), new(0, 0));
        }

        public bool CollidesWith(Vector2D point)
        {
            throw new NotImplementedException();
        }
        public bool IsColliding(IShape2D s2)
        {
            return IsColliding(s2.ToPolygon2D());
        }
        public bool IsColliding(Polygon2D polygon2) {
            if (Polygon2D.ArePolygonVerticesColliding(this.ToPolygon2D(), polygon2)) return true;
            if (AreEgesColliding(polygon2)) return true;
            return false;
        }
        private bool AreEgesColliding(Polygon2D polygon2) {
            foreach (var edge in polygon.Edges)
            {
                foreach (var edge2 in polygon2.Edges)
                {
                    Point2D point2D = new Point2D();
                    if (edge.TryIntersect(edge2, out point2D, Angle.FromDegrees(1)))
                        return true;
                }
            }
            return false;
        }
        public Polygon2D ToPolygon2D()
        {
            return polygon;
        }

        public IShape2D Shifted(Vector2D shift)
        {
            return Shifted(shift.X, shift.Y);
        }
        public IShape2D Shifted(params double[] values)
        {
            //List<Point2D> vertices = new List<Point2D>();
            //polygon.Vertices.ToList().ForEach(v => vertices.Add(v));
            //var newPolygon = new Polygon2D(vertices);
            return new Polygon(Owner, MovementManager.Copy(), polygon.TranslateBy(new(values[0], values[1])).Vertices.ToList());
        }

        public void SetRotation(double newAngle, IMovementBlockingStrategy movementBlockingStrategy)
        {
            var deltaAngle = newAngle - angle;
            
            deltaAngle = movementBlockingStrategy.CalculateTurnAngle(movementManager.Position, deltaAngle);
            if (Math.Abs(newAngle - angle) > 0.00000001 && Math.Abs(deltaAngle)>0.00000001)
            {

            }
            angle += deltaAngle;
            var rotated = polygon.RotateAround(Angle.FromRadians(deltaAngle), new(0, 0));
            polygon = rotated;
        }

        public Polygon ToPolygon()
        {
            return this;
        }

        //public bool IsCollidingCircle(Circle circleSector)
        //{
        //    throw new NotImplementedException();
        //}
        public static Polygon Circle(
            ICollisionDetector owner,
            IMovementManager movementManager,
            Vector2D center,
            double radius,
            int numberOfVertices = 20) {

            return new Polygon(owner, movementManager, CircleVertices(center, radius, numberOfVertices));
        }
        public static List<Point2D> RectangleVertices(Vector2D center, double width, double height) {
            return new List<Point2D> {
                new(center.X-width/2,center.Y-height/2),
                new(center.X-width/2,center.Y+height/2),
                new(center.X+width/2,center.Y+height/2),
                new(center.X+width/2,center.Y-height/2)};
        }
        public static List<Point2D> CircleVertices(
            Vector2D center,
            double radius,
            int numberOfVertices = 20)
        {
            return CircleVertices(center,radius,Angle.FromDegrees(0),Angle.FromDegrees(360),numberOfVertices);
        }

        public static List<Point2D> CircleVertices(
            Vector2D center,
            double radius,
            Angle angleBegin,
            Angle angleEnd,
            int numberOfVertices = 20)
        {
            double step = (Math.PI * 2) / numberOfVertices;
            List<Point2D> vertices = new();
            for (double i = angleBegin.Radians; i <= angleEnd.Radians; i += step)
            {
                var angle = i;// + angleBegin.Radians;
                Vector2D v = Vector2D.FromPolar(radius, Angle.FromRadians(angle)) + center;
                vertices.Add(new(v.X, v.Y));
            }
            return vertices;
        }

        public bool IsCollidingPoint(Vector2D point)
        {
            return polygon.EnclosesPoint(new(point.X, point.Y));
        }

        private (Vector2D, Vector2D) FurthestFrom(Line2D line) 
        {
            Point2D furthestLeft = polygon.Vertices.ElementAt(0);
            Point2D furthestRight = polygon.Vertices.ElementAt(1);
            foreach (var point in polygon.Vertices)
            {
                var currentToPoint = (point - line.ClosestPointTo(point, false));
                var currentToFurthestRight = (furthestRight - line.ClosestPointTo(furthestRight, false));
                var currentToFurthestLeft = (furthestLeft - line.ClosestPointTo(furthestLeft, false));
                if (line.Direction.SignedAngleTo(currentToPoint, false, true) < Angle.FromRadians(0)) {
                    if (currentToPoint.Length > currentToFurthestRight.Length)
                        furthestRight = point;
                    continue;
                }
                if (currentToPoint.Length > currentToFurthestLeft.Length)
                    furthestLeft = point;
            }
            return (furthestLeft.ToVector2D(), furthestRight.ToVector2D());
            
        }
        public RayCollection CastRays(Vector2D movementVector, int numberOfRays)
        {
            Angle angleToYAxis = movementVector.AngleTo(new(0,-1));
            Vector2D movementDirection = movementVector.Normalize();
            var baseTransformed = polygon.Rotate(angleToYAxis);
            var furthestPoints = FurthestFrom(new( Middle.ToPoint2D(), (Middle + movementDirection).ToPoint2D()));
            //Console.WriteLine($"furthest points: {furthestPoints.Item1},{furthestPoints.Item2}");
            Vector2D largestXPoint = baseTransformed.Vertices.MaxBy(p=>p.X).ToVector2D();
            Vector2D smallestXPoint = baseTransformed.Vertices.MinBy(p=>p.X).ToVector2D();
            //Console.WriteLine("largestXPoint before: " + largestXPoint);
            //Console.WriteLine("smallestXPoint before: " + smallestXPoint);
            smallestXPoint = smallestXPoint.Rotate(-angleToYAxis);
            largestXPoint = largestXPoint.Rotate(-angleToYAxis);
            //Console.WriteLine("largestXPoint after: " + largestXPoint);
            //Console.WriteLine("smallestXPoint after: " + smallestXPoint);
            //largestXPoint = furthestPoints.Item2;
            //smallestXPoint = furthestPoints.Item1;
            Vector2D diameter = (smallestXPoint - largestXPoint);
            
            //rays from the diameter
            IRay[] rays = new IRay[numberOfRays];
            for (int i = 0; i < numberOfRays; i++) {
                rays[i] = new NormalRay(largestXPoint + GlobalPosition + diameter * ((double)i / (numberOfRays - 1)), movementDirection);
            }
            var rayCollection = new RayCollection(rays);
            return rayCollection;
            // find where the rays cast from the diameter intersect the polygon
            RayPolygonIntersection.Collection intersections = rayCollection.Intersections(baseTransformed);


            IRay[] newRays = new IRay[numberOfRays];
            for (int i = 0; i < numberOfRays; i++)
            {
                var rayStartPoint = intersections[i].FurthestInFront();
                if (rayStartPoint == null) throw new Exception("Something very bad happened!");
                newRays[i] = new NormalRay(rayStartPoint.Value, movementDirection);
            }
            return new RayCollection(newRays);
        }

        public Polygon Rotated(double angle)
        {
            var res = new Polygon(Owner, MovementManager, angle,lastMiddle,polygon.RotateAround(Angle.FromRadians(angle), new(0, 0)));
            return res;
        }

        public IShape2D ShiftedByPos => this.Shifted(GlobalPosition);

        public double RotationAngle => angle;

    }
}
