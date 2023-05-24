using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.WorldInterfaces;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Data
{
    public class WorldCatalogue : Catalogue<IWorldFactory>
    {
        protected override void FillFactories(Dictionary<string, IWorldFactory> factories)
        {
            string name1 = "Empty";
            factories.Add(name1, new EmptyWorldFactory(name1));
        }

    }
}
