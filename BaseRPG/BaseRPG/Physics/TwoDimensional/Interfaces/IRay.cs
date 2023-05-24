using BaseRPG.Physics.TwoDimensional.Collision.Ray;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Interfaces
{
    public interface IRay
    {
        Vector2D Direction { get; }
        Vector2D Origin { get; }

        RayPolygonIntersection Intersect(Polygon2D polygon);
    }
}
