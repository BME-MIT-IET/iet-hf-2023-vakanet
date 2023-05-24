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
    /// <summary>
    /// Represents rays that shoot in the Y diresction
    /// </summary>
    //public class VerticalRay2D : IRay
    //{
    //    private Point2D origin;

    //    public VerticalRay2D(Point2D origin)
    //    {
    //        this.origin = origin;
    //    }
    //    public VerticalRay2D(Vector2D origin)
    //    {
    //        this.origin = new(origin.X,origin.Y);
    //    }
    //    public Vector2D Origin => origin.ToVector2D();

    //    public IRayIntersection Intersect(Polygon2D polygon)
    //    {
    //        double bestIntersection = double.PositiveInfinity;
    //        foreach (var edge in polygon.Edges)
    //        {
    //            var intersectionPoint = new Point2D(0,0);
    //            LineSegment2D lineSegment2D = new LineSegment2D(origin, new Point2D(origin.X, origin.Y + 100));
    //            bool success = lineSegment2D.TryIntersect(edge, out intersectionPoint, Angle.FromDegrees(0.1));
    //            // the intesection point will always have the X coordinate of the origin, so we can ignore that
    //            if (success)
    //            {
    //                if (intersectionPoint.Y >= -0.00001) {
    //                    if (intersectionPoint.Y < bestIntersection)
    //                    {
    //                        bestIntersection = intersectionPoint.Y;
    //                    }
    //                }
    //            }

    //        }
    //        return new VerticalRay2DIntersection(this,bestIntersection);
    //    }
    //}
}
