using System;
using System.Collections.Generic;

namespace XerxesEngine.State_Management
{
    public class State_Machine_Distinct 
    {
        private State _State_Machine_Distinct__DEFAULT_STATE { get; }

        private Dictionary<Type,State_Flow> _State_Machine_Distinct__REGISTERED_STATES { get; }
        public bool State_Machine_Distinct__IsDefined_All_States
            => _State_Machine_Distinct__Undefined_State_Count > 0;
        private uint _State_Machine_Distinct__Undefined_State_Count { get; set; }

        private State_Flow _State_Machine__Current_State_Flow { get; set; }

        private State_Flow Private_Get__State_Flow__State_Machine<T>()
        {
            Type tStateType = typeof(T);
            if(_State_Machine_Distinct__REGISTERED_STATES.ContainsKey(tStateType))
                return _State_Machine_Distinct__REGISTERED_STATES[tStateType];
            return null;
        }

        public State_Machine_Distinct(State defaultState = null)
        {
            _State_Machine_Distinct__DEFAULT_STATE = defaultState ?? new State();

            _State_Machine_Distinct__REGISTERED_STATES = new Dictionary<Type,State_Flow>();
            
            Register__State__State_Machine_Distinct<State>(_State_Machine_Distinct__DEFAULT_STATE);

            //Define Default State_Flow.
            Define__State_Flow__State_Machine_Distinct<State,State>();
            

            _State_Machine_Distinct__Undefined_State_Count = 0;
        }

#region State Management
        
        public bool Request__State_Transition__State_Machine_Distinct<T>() where T : State
        {
            State_Flow tStateFlow = Private_Get__State_Flow__State_Machine<T>();

            if(tStateFlow == null)
                return false;

            State_Phase_Transition_Response stateTransitionResponse = 
                Private_Set__Current_State_Flow__State_Machine_Distinct(tStateFlow);

            return true;
        }

        private void Private_Panic__State_Machine_Distinct()
        {
            Handle_Panicked__State_Machine_Distinct();
        }

        //Implementation control for responding to a state machine panic.
        protected virtual void Handle_Panicked__State_Machine_Distinct()
        {

        }

        private State_Phase_Transition_Response Private_Set__Current_State_Flow__State_Machine_Distinct
        (
            State_Flow newStateFlow
        )
        {
            State_Phase_Transition_Response stateTransitionResponse = 
                _State_Machine__Current_State_Flow?
                .State_Flow__STATE
                .Internal_Conclude__State()
                ??
                State_Phase_Transition_Response.Accepted_Transition;

            switch(stateTransitionResponse)
            {
                case State_Phase_Transition_Response.Rejected_Transition:
                case State_Phase_Transition_Response.Invalid_Transition:
                    break;
                default:
                case State_Phase_Transition_Response.Accepted_Transition:
                    _State_Machine__Current_State_Flow = newStateFlow;
                    break;
            }

            return stateTransitionResponse;
        }

#endregion

#region State Declarations
        public bool Register__State__State_Machine_Distinct<T>
        (
            T state
        ) where T : State
        {
            Type stateType = typeof(T);
            if(_State_Machine_Distinct__REGISTERED_STATES.ContainsKey(stateType))
                return false;

            _State_Machine_Distinct__REGISTERED_STATES.Add
            (
                typeof(T),
                new State_Flow(state)
            );

            _State_Machine_Distinct__Undefined_State_Count++;

            return true;
        }

        public bool Define__State_Flow__State_Machine_Distinct<T,Y>()
            where T : State
            where Y : State
        {
            State_Flow tState = Private_Get__State_Flow__State_Machine<T>();
            State_Flow yState = Private_Get__State_Flow__State_Machine<Y>();

            if(tState == null || yState == null)
                return false;

            if(tState.State_Flow__Target_State_Flow == null)
                _State_Machine_Distinct__Undefined_State_Count--;

            tState.Internal_Set__Target_State_Flow__State_Flow(yState);

            return true;
        }
#endregion
    }
}
