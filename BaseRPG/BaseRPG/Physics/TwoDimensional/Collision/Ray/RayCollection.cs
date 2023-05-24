using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision.Ray
{
    public class RayCollection
    {
        private IRay[] rays;

        public RayCollection(IRay[] rays)
        {
            this.Rays = rays;
        }

        public IRay[] Rays { get => rays; set => rays = value; }

        public RayPolygonIntersection.Collection Intersections(Polygon2D polygon)
        {
            RayPolygonIntersection[] result = new RayPolygonIntersection[Rays.Length];

            for(int i = 0;i<result.Length;++i)
            {
                result[i] = Rays[i].Intersect(polygon);
            }
            return new RayPolygonIntersection.Collection(result);
        }
    }
}
