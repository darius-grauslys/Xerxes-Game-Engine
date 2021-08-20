using isometricgame.GameEngine.Events.Arguments;

namespace isometricgame.GameEngine.State_Management
{
    public class State
    {
        public State_Phase State__State_Phase { get; private set; }
        public State_Handle State__Progressing_State_Handle { get; protected set; }

        public State()
        {
            State__State_Phase = State_Phase.Concluded;
            State__Progressing_State_Handle = State_Handle.DEFAULT_HANDLE;
        }
        
        internal State_Phase_Transition_Success Internal_Enter__State()
        {
            if (State__State_Phase != State_Phase.Concluded)
                return State_Phase_Transition_Success.Invalid_Transition;

            bool wasTransitionAccepted = Handle_Enter__State();

            if (!wasTransitionAccepted)
                return State_Phase_Transition_Success.Rejected_Transition;
            
            State__State_Phase = State_Phase.Begun;

            return State_Phase_Transition_Success.Accepted_Transition;
        }

        internal State_Phase_Update_Response Internal_Update__State(Frame_Argument frameArgument)
        {
            if (State__State_Phase != State_Phase.Operating)
                return State_Phase_Update_Response.Idle;

            return Handle_Update__State(frameArgument);
        }

        internal State_Phase_Transition_Success Internal_Conclude__State()
        {
            if (State__State_Phase != State_Phase.Operating)
                return State_Phase_Transition_Success.Invalid_Transition;

            bool wasTransitionAccept = Handle_Conclude__State();

            if (!wasTransitionAccept)
                return State_Phase_Transition_Success.Rejected_Transition;

            State__State_Phase = State_Phase.Concluded;
            
            return State_Phase_Transition_Success.Accepted_Transition;
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