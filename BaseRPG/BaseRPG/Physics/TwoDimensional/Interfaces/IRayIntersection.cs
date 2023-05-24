using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Interfaces
{
    public interface IRayIntersection
    {
        public double Distance { get; }
        public Vector2D AsVector { get; }
        Point2D AsPoint { get; }
    }
}
