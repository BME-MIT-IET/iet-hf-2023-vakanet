using BaseRPG.View.EntityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces.Providers
{
    public interface IDrawableProvider
    {
        bool AreConnected(IDrawable drawable, object gameObject);
        void Connect(IDrawable drawable, object gameObject, string name = "default");
        IDrawable GetDrawable(object gameObject, string name, bool fromCache = false);
        T GetDrawable<T>(object gameObject, string name, bool fromCache = false) where T : class, IDrawable;
        List<IDrawable> GetDrawablesAsListOf(object gameObject);
        Dictionary<string, IDrawable> GetDrawablesOf(object gameObject);
    }
}
