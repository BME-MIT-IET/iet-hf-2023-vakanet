using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Physics.TwoDimensional.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Interfaces
{
    public interface IAttackShapeFactory
    {
        IShape2D Create(Attack a);
    }
}
