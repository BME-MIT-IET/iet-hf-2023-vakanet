using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision.Ray
{
    public class NormalRay:IRay
    {
        private Point2D origin;
        private Vector2D direction;
        public NormalRay(Vector2D origin,Vector2D direction):this(new Point2D(origin.X,origin.Y),direction)
        {
        }
        public NormalRay(Point2D origin, Vector2D direction)
        {
            this.origin = origin;
            this.direction = direction;
        }
        internal Vector2D Calculate(double value)
        {
            return new Vector2D(origin.X, origin.Y) + value * direction;
        }

        public Vector2D Origin => origin.ToVector2D();

        public Vector2D Direction => direction;

        public RayPolygonIntersection Intersect(Polygon2D polygon)
        {
            LinkedList<Vector2D> intersections = new();
            foreach (var edge in polygon.Edges)
            {
                var intersectionPoint = new Point2D(0, 0);
                var directionLarge = direction * 999999999D;
                LineSegment2D line = new LineSegment2D(origin - directionLarge, new Point2D(origin.X+ directionLarge.X, origin.Y + directionLarge.Y));
                bool success = line.TryIntersect(edge, out intersectionPoint, Angle.FromDegrees(0.1));
                if (success)
                {
                    intersections.AddLast(intersectionPoint.ToVector2D());
                }

            }
            return new RayPolygonIntersection(intersections,this);
        }
    }
}
