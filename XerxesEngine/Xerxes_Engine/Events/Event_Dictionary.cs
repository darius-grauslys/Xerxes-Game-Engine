namespace Xerxes_Engine.Events
{
    internal sealed class Event_Dictionary : Distinct_Dictionary<Event_Handle, Event>
    {
        public Event_Dictionary(string format = null) 
            : base(format)
        {
        }

        internal Event_Handle Internal_Declare__Event__Event_Dictionary
        (
            string internalHandle,
            Event @event
        )
            => Protected_Declare__Element__Distinct_Dictionary(internalHandle, @event);

        protected override Event_Handle Handle_Get__New_Handle__Distinct_Dictionary(string internalStringHandle)
        {
            return new Event_Handle(internalStringHandle, this);
        }

        public Event this[Event_Handle eventHandle]
            => Protected_Get__Element__Distinct_Dictionary(eventHandle);
    }
}
