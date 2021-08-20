using System;
using System.Collections.Generic;
using isometricgame.GameEngine.Tools.Collections.Recurring_Dictionary;

namespace isometricgame.GameEngine.State_Management
{
    public class State_Machine : Recurring_Handle_Catalog<State_Handle, State>
    {
        private State _State_Machine__DEFAULT_STATE { get; }

        private Dictionary<State_Handle, State> _State_Machine__STATES { get; }
        
        public State_Machine(State defaultState = null)
        {
            _State_Machine__DEFAULT_STATE = defaultState ?? new State();
        }

        public State_Handle Bind__State__State_Machine(string desiredHandle, State state)
            => Protected_Add__Entry__Recurring_Handle_Catalog(desiredHandle, state);

        protected override State_Handle Handle_Format__Internal_Handle__Recurring_Handle_Catalog(string handle, int index)
        {
            return new State_Handle
            (
                String.Format(DEFAULT_RECURRING_FORMAT, handle, index)
            );
        }
    }
}