using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.State
{
    public interface IState<T> where T : IState<T>
    {
        /// <summary>Returns wether this state can be activated if the current state is the parameter</summary>
        bool CanBeActivated(T newState);
    }
}
