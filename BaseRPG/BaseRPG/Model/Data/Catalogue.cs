using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Data
{
    public class Catalogue<FactoryInterface>
    {
        private Dictionary<string, FactoryInterface> factories = new Dictionary<string, FactoryInterface>();

        public Catalogue(Dictionary<string, FactoryInterface> factories)
        {
            this.factories = factories;
        }
        public Catalogue()
        {
            FillFactories(factories);
        }

        public FactoryInterface this[string key]{
            get
            {
                return factories[key];
            }
        }
        protected virtual void FillFactories(Dictionary<string, FactoryInterface> factories) { }

    }
}
