using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Utility
{
    public interface ICanQueueFunc<T>
    {
        public void QueueWithResult(Func<T> func,Action<T> onResult);
    }
}
