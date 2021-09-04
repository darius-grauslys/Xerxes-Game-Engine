namespace isometricgame.GameEngine.State_Management
{
    internal class State_Flow
    {
        internal State State_Flow__STATE {get;}
        internal State_Flow State_Flow__Target_State_Flow { get; private set; }
        internal void Internal_Set__Target_State_Flow__State_Flow(State_Flow targetStateFlow)
            => State_Flow__Target_State_Flow = targetStateFlow;

        internal State_Flow
        (
            State state
        )
        {
            State_Flow__STATE = state;
        }
    }
}
