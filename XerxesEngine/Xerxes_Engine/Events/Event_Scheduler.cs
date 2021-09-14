using System.Collections.Generic;
using System.Linq;

namespace Xerxes_Engine.Events
{
    public sealed class Event_Scheduler
    {
        private Event_Dictionary _Event_Scheduler__EVENT_DICTIONARY { get; }

        private List<Event> _Event_Scheduler__ACTIVE_EVENTS { get; }
        private bool _event_Scheduler__IsActive;

        public bool Event_Scheduler__IsActive
        {
            get => _event_Scheduler__IsActive;
            private set => _event_Scheduler__IsActive = value;
        }

        internal Event_Scheduler()
        {
            _Event_Scheduler__EVENT_DICTIONARY = new Event_Dictionary();
            _Event_Scheduler__ACTIVE_EVENTS = new List<Event>();

            Event_Scheduler__IsActive = false;
        }
        
        public bool CheckIf__Event_Is_Active__Event_Scheduler(Event_Handle eventHandle)
        {
            Event @event = _Event_Scheduler__EVENT_DICTIONARY[eventHandle];

            bool isActive = CheckIf__Event_Is_Active__Event_Scheduler(@event);

            return isActive;
        }
        
        public bool CheckIf__Event_Is_Active__Event_Scheduler(Event @event)
        {
            return (_Event_Scheduler__ACTIVE_EVENTS.Contains(@event));
        }

        public Event_Handle Bind__Event_To_Scheduler__Event_Scheduler
        (
            string desiredHandle, 
            Event @event
        )
        {
            Event_Handle eventHandle = _Event_Scheduler__EVENT_DICTIONARY.Internal_Declare__Event__Event_Dictionary
            (
                desiredHandle,
                @event
            );

            return eventHandle;
        }

        public void Invoke__Event__Event_Scheduler(Event_Handle eventHandle, double newTime = -1)
        {
            Event subject = _Event_Scheduler__EVENT_DICTIONARY[eventHandle];
            
            subject.Internal_Reset__Event(newTime);
            
            _Event_Scheduler__ACTIVE_EVENTS.Add(subject);
            
            Event_Scheduler__IsActive = true;
        }

        public void Invoke__Event__Event_Scheduler(Event @event, double newTime = -1)
        {
            Invoke__Event__Event_Scheduler(@event.Event__Handle, newTime);
        }
        
        internal bool Internal_Progress__Events__Event_Scheduler(double deltaTime)
        {
            if (!Event_Scheduler__IsActive)
                return false;

            bool ret, fullret = false;

            foreach(Event subject in _Event_Scheduler__ACTIVE_EVENTS.ToList())
            {
                ret = subject.Internal_Progress__Delta_Time__Event(deltaTime);
                if (ret)
                {
                    _Event_Scheduler__ACTIVE_EVENTS.Remove(subject);
                }
                fullret = fullret || !ret;
            }
            
            return Event_Scheduler__IsActive = fullret;
        }
    }
}
