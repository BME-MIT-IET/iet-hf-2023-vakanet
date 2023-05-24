using BaseRPG.Physics.TwoDimensional.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Interfaces
{
    public interface IShapeFactory
    {
        IShape2D Create();
    }
}
