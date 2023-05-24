using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Data
{
    public class GenericCallbackQueue<T>
    {
        private List<Action<T>> actions = new();
        public void QueueForExecute(Action<T> action)
        {
            lock (this)
                actions.Add(action);
        }

        public void ExecuteAll(T param)
        {
            lock (this)
            {
                foreach (var action in actions)
                {
                    action.Invoke(param);
                }
                //actions.ForEach(action => action());
                actions.Clear();
            }
        }
    }
}
