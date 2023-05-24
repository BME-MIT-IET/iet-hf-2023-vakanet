using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Utility
{
    public interface ICanQueueAction
    {
        void QueueAction(Action action);
    }
}
