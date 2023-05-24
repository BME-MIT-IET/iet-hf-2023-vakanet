using BaseRPG.Model.Interfaces.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Data
{
    public class BoolCallbackQueue : ICanQueueFunc<bool>
    {
        private List<(Func<bool>,Action<bool>)> _queue = new();
        public void QueueWithResult(Func<bool> func, Action<bool> onResult)
        {
            lock(_queue) _queue.Add((func, onResult));
        }

        internal void Tick()
        {
            lock (_queue) {
                _queue.ForEach(pair => pair.Item2(pair.Item1()));
                _queue.Clear();
            }
        }

    }
}
