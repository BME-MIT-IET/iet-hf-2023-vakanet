using BaseRPG.Model.Utility;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView
{


    public class DrawableProvider : IDrawableProvider
    {
        private static OneToManyProvider<object, IDrawable> provider = new();



        public void Connect(IDrawable drawable, object gameObject, string name = "default")
        {
            provider.Connect(gameObject, drawable, name);
        }
        public bool AreConnected(IDrawable drawable, object gameObject)
        {
            return provider.AreConnected(drawable, gameObject);
        }

        public Dictionary<string, IDrawable> GetDrawablesOf(object gameObject)
        {
            return provider.GetObjectsOf(gameObject);
        }
        public List<IDrawable> GetDrawablesAsListOf(object gameObject)
        {
            return provider.GetObjectsAsListOf(gameObject);
        }
        public IDrawable GetDrawable(object gameObject, string name, bool fromCache = false)
        {
            return provider.GetObject(gameObject, name, fromCache);
        }
        public T GetDrawable<T>(object gameObject, string name, bool fromCache = false) where T : class, IDrawable
        {
            return provider.GetObject<T>(gameObject, name, fromCache);
        }

    }
}
