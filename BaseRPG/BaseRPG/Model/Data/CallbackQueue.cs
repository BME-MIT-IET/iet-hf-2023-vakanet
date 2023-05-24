using BaseRPG.Model.Interfaces.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Data
{
    public class CallbackQueue:ICanQueueAction
    {
        private List<Action> actions = new();
        private List<Action> bonusActions = new();
        private bool isExecuting = false;
        //private GenericCallbackQueue<object> innerCallbackQueue = new();
        public void QueueAction(Action action)
        {
            lock (this)
            {
                if (isExecuting)
                    bonusActions.Add(action);
                else
                    actions.Add(action);
            }
                
        }

        public void ExecuteAll()
        {
            lock (this)
            {
                isExecuting = true;
                actions.ForEach(a => a());
                actions.Clear();
                isExecuting = false;
                bonusActions.ForEach(a => a());
                bonusActions.Clear();
            }
        }
    }
}
