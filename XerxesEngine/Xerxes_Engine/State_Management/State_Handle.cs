namespace Xerxes_Engine.State_Management
{
    public sealed class State_Handle : Distinct_Handle
    {
        internal State_Handle(string internalStateHandle, object source)
            : base (internalStateHandle, source)
        {}
    }
}
