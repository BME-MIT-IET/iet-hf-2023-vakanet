using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Controller.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Interfaces
{
    public interface IWindowsInitializer
    {
        WindowControl Initialize(MainWindow window,ShopControl shopControl);
    }
}
