namespace Xerxes_Engine.State_Management
{
    public class State
    {
        public State_Phase State__State_Phase { get; private set; }

        public State()
        {
            State__State_Phase = State_Phase.Concluded;
        }
        
        internal State_Phase_Transition_Response Internal_Enter__State()
        {
            if (State__State_Phase != State_Phase.Concluded)
                return State_Phase_Transition_Response.Invalid_Transition;

            bool wasTransitionAccepted = Handle_Enter__State();

            if (!wasTransitionAccepted)
                return State_Phase_Transition_Response.Rejected_Transition;
            
            State__State_Phase = State_Phase.Begun;

            return State_Phase_Transition_Response.Accepted_Transition;
        }

        internal State_Phase_Update_Response Internal_Update__State(Frame_Argument frameArgument)
        {
            if (State__State_Phase != State_Phase.Operating)
                return State_Phase_Update_Response.Idle;

            return Handle_Update__State(frameArgument);
        }

        internal State_Phase_Transition_Response Internal_Conclude__State()
        {
            if (State__State_Phase != State_Phase.Operating)
                return State_Phase_Transition_Response.Invalid_Transition;

            bool wasTransitionAccept = Handle_Conclude__State();

            if (!wasTransitionAccept)
                return State_Phase_Transition_Response.Rejected_Transition;

            State__State_Phase = State_Phase.Concluded;
            
            return State_Phase_Transition_Response.Accepted_Transition;
        }
        
        /// <summary>
        /// Control point for beginning the state. Returning false puts state machine into panic.
        /// </summary>
        /// <returns></returns>
        protected virtual bool Handle_Enter__State() => true;

        /// <summary>
        /// Control point for managing an active state.
        /// </summary>
        /// <param name="frameArgument">Frame information of the game.</param>
        protected virtual State_Phase_Update_Response Handle_Update__State(Frame_Argument frameArgument)
            => State_Phase_Update_Response.Idle;

        /// <summary>
        /// Control point for ending the state. Returning false puts state machine into panic.
        /// </summary>
        /// <returns></returns>
        protected virtual bool Handle_Conclude__State() => true;
    }
}
