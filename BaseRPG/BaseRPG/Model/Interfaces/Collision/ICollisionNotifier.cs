using BaseRPG.Physics.TwoDimensional.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Collision
{
    public interface ICollisionNotifier
    {
        void NotifyCollisions(double delta);

    }
}
