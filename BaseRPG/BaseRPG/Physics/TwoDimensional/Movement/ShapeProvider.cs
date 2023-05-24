using BaseRPG.Model.Utility;
using BaseRPG.Physics.TwoDimensional.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public class ShapeProvider:OneToManyProvider<object,IShape2D>
    {
    }
}
