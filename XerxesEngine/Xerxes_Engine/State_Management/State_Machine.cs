namespace Xerxes_Engine.State_Management
{
    /// <summary>
    /// Manages defined states using State_Flows to
    /// establish a relation between each state. 
    /// The State_Machine is designed to operate off
    /// of Scene_Layer Update loops.
    /// <summary/>
    public class State_Machine 
    {
        public State_Handle State_Machine__DEFAULT_STATE_HANDLE { get; }

        private State_Dictionary _State_Machine__STATE_DICTIONARY { get; }
        public bool State_Machine__Is_Defined_For_All_States
            => _State_Machine__Undefined_State_Count == 0;
        private uint _State_Machine__Undefined_State_Count { get; set; }

        private State_Flow _State_Machine__Current_State_Flow { get; set; }

        protected State_Machine(State nullable_DefaultState = null)
        {
            State defaultState = nullable_DefaultState ?? new State();

            _State_Machine__STATE_DICTIONARY = new State_Dictionary();
            

            //Define Default State_Flow.
            State_Machine__DEFAULT_STATE_HANDLE =
                _State_Machine__STATE_DICTIONARY
                .Internal_Declare__State__State_Dictionary
                (
                    defaultState
                );
            _State_Machine__STATE_DICTIONARY[State_Machine__DEFAULT_STATE_HANDLE]
                .Internal_Set__Target_State_Handle__State_Flow
                (
                    State_Machine__DEFAULT_STATE_HANDLE
                );

            _State_Machine__Undefined_State_Count = 0;
            _State_Machine__Current_State_Flow = 
                _State_Machine__STATE_DICTIONARY[State_Machine__DEFAULT_STATE_HANDLE];

            // You'd think we'd want to remove this perhaps since we don't 
            // want to start any states in the constructor but this is
            // actually okay since the DEFAULT state will not progress until
            // the update calls begin.
            _State_Machine__Current_State_Flow
                .State_Flow__STATE
                .Internal_Enter__State();
        }

#region State Management
        //TODO: Make Xerxes_Object, which has functionality to hook onto scene Update/Render/etc.
        //      Just so developers cannot call Update__State_Machine and similar functions
        //      multiple times per loop.
        public State_Update_Response Update__State_Machine(Event_Argument_Frame e)
        {
            State_Update_Response response =
                _State_Machine__Current_State_Flow
                .State_Flow__STATE
                .Internal_Update__State(e);

            switch(response)
            {
                case State_Update_Response.Idle:
                    break;
                case State_Update_Response.Break:
                    Private_Panic__State_Machine();
                    break;
                case State_Update_Response.Progress:
                    Protected_Progress__Current_State_Flow__State_Machine();
                    break;
            }

            return response;
        }

        /// <summary>
        /// Returns null if an invalid handle was used. Otherwises true if
        /// the transition was successful and false if it otherwise failed. 
        /// For more information on invalid handles, see Distinct_Handle.cs .
        ///
        /// It is almost always better to rely on State_Flows for transitioning
        /// as opposed to manual transitions.
        /// <summary/>
        protected State_Transition_Response? Protected_Request__State_Transition__State_Machine
        (
            State_Handle stateHandle
        )
        {
            if
            (
                !stateHandle
                .Internal__Is_From_Source__Distinct_Handle(_State_Machine__STATE_DICTIONARY)
            )
            {
                return null;
            }

            State_Transition_Response concludeResponse = 
                Private_Conclude__Current_State_Flow__State_Machine();
            
            if(concludeResponse != State_Transition_Response.Accepted_Transition)
                return concludeResponse;

            State_Transition_Response enterResponse =
                Private_Set__Current_State_Flow__State_Machine(stateHandle);

            return enterResponse;
        }

        protected virtual void Handle_Invalid__State_Handle__State_Machine()
        {
            Protected_Panic__State_Machine();    
        }

        protected void Protected_Panic__State_Machine()
            => Private_Panic__State_Machine();

        private void Private_Panic__State_Machine()
        {
            Handle_Panicked__State_Machine();
        }

        //Implementation control for responding to a state machine panic.
        protected virtual void Handle_Panicked__State_Machine()
        {
            Log.Internal_Write__Verbose__Log("Panic! State:{0}", this, _State_Machine__Current_State_Flow.State_Flow__STATE);
        }

        protected State_Transition_Response Protected_Progress__Current_State_Flow__State_Machine()
        {
            State_Transition_Response response = Private_Progress__Current_State_Flow__State_Machine();

            switch(response)
            {
                case State_Transition_Response.Invalid_Transition:
                case State_Transition_Response.Rejected_Transition:
                    Private_Panic__State_Machine();
                    break;
                case State_Transition_Response.Accepted_Transition:
                    break;
            }

            return response;
        }

        private State_Transition_Response Private_Progress__Current_State_Flow__State_Machine()
        {
            State_Transition_Response concludedResponse = 
                Private_Conclude__Current_State_Flow__State_Machine();

            if (concludedResponse != State_Transition_Response.Accepted_Transition)
                return concludedResponse;

            State_Transition_Response enterResponse =
                Private_Set__Current_State_Flow__State_Machine
                (
                    _State_Machine__Current_State_Flow
                    .State_Flow__Target_State_Handle
                );

            return enterResponse;
        }

        private State_Transition_Response Private_Conclude__Current_State_Flow__State_Machine()
        {
            State state =
                _State_Machine__Current_State_Flow?
                .State_Flow__STATE;
            State_Transition_Response stateTransitionResponse = 
                state?
                .Internal_Conclude__State()
                ??
                State_Transition_Response.Accepted_Transition;

            return stateTransitionResponse;
        }

        private State_Transition_Response Private_Set__Current_State_Flow__State_Machine
        (
            State_Handle stateHandle
        )
        {
            State_Flow stateFlow = _State_Machine__STATE_DICTIONARY[stateHandle];

            State_Transition_Response response =
                stateFlow
                .State_Flow__STATE?
                .Internal_Enter__State()
                ?? State_Transition_Response.Invalid_Transition;

            switch(response)
            {
                case State_Transition_Response.Invalid_Transition:
                case State_Transition_Response.Rejected_Transition:
                    Private_Panic__State_Machine();
                    break;
                case State_Transition_Response.Accepted_Transition:
                    _State_Machine__Current_State_Flow = stateFlow;
                    break;
            }
            
            return response;
        }

#endregion

#region State Declarations
        protected State_Handle Protected_Register__State__State_Machine
        (
            State state,
            string stringHandle = null
        )
        {
            State_Handle stateHandle = 
                _State_Machine__STATE_DICTIONARY
                .Internal_Declare__State__State_Dictionary
                (
                    state,
                    stringHandle
                );

            _State_Machine__Undefined_State_Count++;

            return stateHandle;
        }

        protected void Protected_Define__State_Flow__State_Machine
        (
            State_Handle stateHandle_From,
            State_Handle stateHandle_To
        )
        {
            State_Flow flow_From = _State_Machine__STATE_DICTIONARY[stateHandle_From];

            if (flow_From.State_Flow__Target_State_Handle == null)
                _State_Machine__Undefined_State_Count--;

            flow_From.Internal_Set__Target_State_Handle__State_Flow
            (
                stateHandle_To
            );
        }
#endregion
#region Protected Logging
        protected void Protected_Log_Error__FAILED_TO_DEFINE_FLOW__State_Machine
        (
            string contextualMessage = ""
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__STATE_MACHINE__FAILED_TO_DEFINE_FLOW_1,
                this,
                contextualMessage
            );
        }
#endregion
    }
}
