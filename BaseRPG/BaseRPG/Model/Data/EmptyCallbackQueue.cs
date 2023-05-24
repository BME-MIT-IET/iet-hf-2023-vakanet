using BaseRPG.Model.Interfaces.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Data
{
    internal class EmptyCallbackQueue : ICanQueueAction
    {
        public void QueueAction(Action action)
        {
            
        }
    }
}
