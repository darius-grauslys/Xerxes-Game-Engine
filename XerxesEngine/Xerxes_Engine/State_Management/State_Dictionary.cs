namespace Xerxes_Engine.State_Management
{
    internal class State_Dictionary : Distinct_Handle_Dictionary<State_Handle, State_Flow>
    {
        internal State_Dictionary(string format = null) 
            : base(format)
        {
        }

        internal State_Handle Internal_Declare__State__State_Dictionary
        (
            State state,
            string stringStateHandle = null
        )
        {
            State_Flow stateFlow = new State_Flow(state);
            State_Handle stateHandle = Protected_Declare__Element__Distinct_Handle_Dictionary
            (
                stringStateHandle ?? stateFlow.ToString(),
                stateFlow
            );

            return stateHandle;
        }

        protected override State_Handle Handle_Get__New_Handle__Distinct_Handle_Dictionary
        (
            string internalStringHandle
        )
        {
            return new State_Handle(internalStringHandle, this);
        }

        public State_Flow this[State_Handle stateHandle]
            => Protected_Get__Element__Distinct_Handle_Dictionary(stateHandle);
    }
}
