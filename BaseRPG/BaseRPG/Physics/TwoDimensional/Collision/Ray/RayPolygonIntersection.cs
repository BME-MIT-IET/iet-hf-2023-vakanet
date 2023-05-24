using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision.Ray
{
    public class RayPolygonIntersection
    {
        IEnumerable<Vector2D> intersectionPoints;
        private readonly IRay ray;

        public IRay Ray => ray;

        public RayPolygonIntersection(IEnumerable<Vector2D> intersectionPoints, IRay ray)
        {
            this.intersectionPoints = intersectionPoints;
            this.ray = ray;
        }
        //public Vector2D? ClosestTo(Vector2D other) {
        //    Vector2D closest = new(double.PositiveInfinity,double.PositiveInfinity);
        //    foreach (var intersection in intersectionPoints)
        //    {
        //        if ((intersection - other).Length < closest.Length)
        //            closest = intersection;
        //    }
        //    if (closest.X == double.PositiveInfinity) 
        //        return null;
        //    if (closest.Y == double.PositiveInfinity) 
        //        return null;
        //    return closest;
        //}

        /// <summary>
        /// Returns the furthest intersection point from the point given in the parameter that is in the direction of the direction parameter.
        /// If the "furthest" intersection point is the point in the paramtere itself, then it returns that
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector2D? FurthestFromPointInDirection(Vector2D direction, Vector2D point)
        {
            
            double furthestLength = double.NegativeInfinity;
            Vector2D? furthest = null;
            foreach (var intersection in intersectionPoints)
            {
                var intersectionDiresction = (intersection - point);
                // if the vector that points to the intersection from the given point faces in the same direction as the given direction
                // then this intersection point is a possible candidate for the closest point in the given direction
                if (intersectionDiresction.DotProduct(direction)>0 || point.Equals(intersection, 0.0000001)) {
                    var length = intersectionDiresction.Length;
                    if (length > furthestLength) {
                        furthestLength = length;
                        furthest = intersection;
                    }
                }
            }
            return furthest;
        }

        public Vector2D? ClosestToPointInDirection(Vector2D direction, Vector2D point)
        {

            double closestLength = double.PositiveInfinity;
            Vector2D? closest = null;
            foreach (var intersection in intersectionPoints)
            {
                var intersectionDirection = (intersection - point);
                // if the vector that points to the intersection from the given point faces the same direction as the given direction
                // then this intersection point is a possible candidate for the closest point in the given direction
                if (intersectionDirection.DotProduct(direction) > 0 || point.Equals(intersection, 0.0000001))
                {
                    var length = intersectionDirection.Length;
                    if (length < closestLength)
                    {
                        closestLength = length;
                        closest = intersection;
                    }
                }
            }
            return closest;
        }
        public Vector2D? ClosestInFront() {
            return ClosestToPointInDirection(Ray.Direction,Ray.Origin);
        }
        public Vector2D? FurthestInFront()
        {
            return FurthestFromPointInDirection(Ray.Direction, Ray.Origin);
        }

        public class Collection {
            private RayPolygonIntersection[] intersections;

            public Collection(RayPolygonIntersection[] intersections)
            {
                this.intersections = intersections;
            }
            public RayPolygonIntersection this[int index]            
            {
                get => intersections[index];  
            }
            public double ClosestIntersectionDistance() {

                double result = double.PositiveInfinity;
                foreach (var intersection in intersections)
                {
                    var closestPoint = intersection.ClosestInFront();
                    if (closestPoint == null) continue;
                    double length = (closestPoint - intersection.Ray.Origin).Value.Length;
                    if (length < result) {
                        result = length;
                    }
                }
                return result;
            }
        }
    }
}
