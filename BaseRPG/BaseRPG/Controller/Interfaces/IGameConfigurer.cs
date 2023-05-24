using BaseRPG.View.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Interfaces
{
    public interface IGameConfigurer
    {
        IReadOnlyGameConfiguration Configure(Container container);
    }
}
