using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Utility
{
    public class PositionTracker : IPositionProvider, IPositionWriter
    {
        public Vector2D Position { get; set; }
        public PositionTracker()
        {
        }
        public PositionTracker(Vector2D position)
        {
            Position = position;
        }
    }
}
