using BaseRPG.Model.Interfaces.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.State
{
    public class StateHandler<STATE> where STATE : IState<STATE>
    {
        private List<STATE> damageTakingStates = new();

        protected STATE CurrentState
        {
            get => damageTakingStates.Last();
        }
        public StateHandler(STATE baseDamageTakingState)
        {
            damageTakingStates.Add(baseDamageTakingState);
        }

        /// <summary>
        /// Inserts the given state above the first state which allows the switch
        /// </summary>
        /// <param name="newState"> the state to be inserted</param>
        /// <returns>wether the new state could be inserted</returns>
        public bool SwichState(STATE newState)
        {
            for (int i = damageTakingStates.Count - 1; i >= 0; i--)
            {
                if (newState.CanBeActivated(damageTakingStates[i]))
                {
                    damageTakingStates.Insert(i + 1, newState);
                    return true;
                }
            }
            return false;

        }
        public void BackToPreviousState()
        {
            if (damageTakingStates.Count <= 1) return;
            damageTakingStates.Remove(damageTakingStates.Last());
        }
    }
}
