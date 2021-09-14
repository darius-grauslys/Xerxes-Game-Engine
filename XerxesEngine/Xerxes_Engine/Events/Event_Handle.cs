namespace Xerxes_Engine.Events
{
    public sealed class Event_Handle : Distinct_Handle
    {
        internal Event_Handle(string internalEventHandle, object source)
            : base(internalEventHandle, source)
        {
        }
    }
}
