namespace Xerxes_Engine.State_Management.Implemented_State_Machines
{
    /// <summary>
    /// Gives public exposure to generic protected
    /// methods of State_Machine. Use this for multiple states
    /// of the same kind, and for basic state machine functionality.
    /// </summary>
    public class Generic_State_Machine : State_Machine
    {
        public Generic_State_Machine(State defaultState = null)
            : base(defaultState)
        {}

        public State_Handle Register__State__Generic_State_Machine(State state, string stringHandle = null)
        {
            return Protected_Register__State__State_Machine(state, stringHandle);
        }

        public void Define__State_Flow__Generic_State_Machine(State_Handle from, State_Handle to)
        {
            Protected_Define__State_Flow__State_Machine(from, to);
        }
    }
}
