using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision.Ray
{
    public class NormalRayIntersection: Interfaces.IRayIntersection
    {
        private readonly double value;
        private readonly Vector2D asVector;
        private readonly Point2D asPoint;

        public NormalRayIntersection(NormalRay ray, double value)
        {
            this.value = value;
            asVector = ray.Calculate(value);
            asPoint = new(asVector.X, asVector.Y);
        }

        public double Distance => value;

        public Vector2D AsVector => asVector;

        public Point2D AsPoint => asPoint;
    }
}
