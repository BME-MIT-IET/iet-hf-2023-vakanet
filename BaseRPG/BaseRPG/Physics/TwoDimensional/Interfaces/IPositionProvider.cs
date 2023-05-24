using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Interfaces
{
    public interface IPositionProvider
    {
        Vector2D Position { get; }
    }
}
