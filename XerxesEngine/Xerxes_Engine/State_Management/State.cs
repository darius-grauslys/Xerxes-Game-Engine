using System;

namespace Xerxes_Engine.State_Management
{
    public class State
    {
        public State_Mode State__State_Mode { get; private set; }

        public State()
        {
            State__State_Mode = State_Mode.Concluded;
        }
        
        internal State_Transition_Response Internal_Enter__State()
        {
            if (State__State_Mode != State_Mode.Concluded)
                return State_Transition_Response.Invalid_Transition;

            bool transition_WasNotAccepted = !Handle_Enter__State();

            if (transition_WasNotAccepted)
                return State_Transition_Response.Rejected_Transition;
            
            State__State_Mode = State_Mode.Operating;

            return State_Transition_Response.Accepted_Transition;
        }

        internal State_Update_Response Internal_Update__State(Frame_Argument frameArgument)
        {
            if (State__State_Mode != State_Mode.Operating)
                return State_Update_Response.Break;

            return Handle_Update__State(frameArgument);
        }

        internal State_Transition_Response Internal_Conclude__State()
        {
            if (State__State_Mode != State_Mode.Operating)
                return State_Transition_Response.Invalid_Transition;

            bool wasTransitionAccept = Handle_Conclude__State();

            if (!wasTransitionAccept)
                return State_Transition_Response.Rejected_Transition;

            State__State_Mode = State_Mode.Concluded;
            
            return State_Transition_Response.Accepted_Transition;
        }
        
        /// <summary>
        /// Control point for beginning the state. Returning false puts state machine into panic.
        /// </summary>
        /// <returns></returns>
        protected virtual bool Handle_Enter__State() 
            => true;

        /// <summary>
        /// Control point for managing an active state.
        /// </summary>
        /// <param name="frameArgument">Frame information of the game.</param>
        protected virtual State_Update_Response Handle_Update__State(Frame_Argument frameArgument)
            => State_Update_Response.Progress;

        /// <summary>
        /// Control point for ending the state. Returning false puts state machine into panic.
        /// </summary>
        /// <returns></returns>
        protected virtual bool Handle_Conclude__State() 
            => true;

        public override string ToString()
        {
            string ret =
                String.Format
                (
                    "{0}:{1}",
                    base.ToString(),
                    State__State_Mode
                );

            return ret;
        }
    }
}
