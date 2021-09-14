namespace Xerxes_Engine.State_Management
{
    internal class State_Flow
    {
        internal State State_Flow__STATE {get;}
        internal State_Handle State_Flow__Target_State_Handle { get; private set; }
        internal void Internal_Set__Target_State_Handle__State_Flow(State_Handle targetStateHandle)
            => State_Flow__Target_State_Handle = targetStateHandle;

        internal State_Flow
        (
            State state
        )
        {
            State_Flow__STATE = state;
        }
    }
}
