using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces
{
    public interface IPositionWriter
    {
        public Vector2D Position { set; }
    }
}
