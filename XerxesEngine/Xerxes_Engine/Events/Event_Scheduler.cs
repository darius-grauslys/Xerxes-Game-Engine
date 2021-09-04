using System;
using System.Collections.Generic;
using System.Linq;

namespace Xerxes_Engine.Events
{
    public sealed class Event_Scheduler
    {
        private const string RECURRING_EVENTHANDLE_FORMAT = "{0}_{1}";
        
        private Dictionary<Event_Handle, Event> _Event_Scheduler__EVENT_CATALOG { get; }
        private Dictionary<string, int> _Event_Scheduler__RECURRING_NAME_COUNTS { get; }
        private List<Event> _Event_Scheduler__ACTIVE_EVENTS { get; }
        private bool _event_Scheduler__IsActive;

        public bool Event_Scheduler__IsActive
        {
            get => _event_Scheduler__IsActive;
            private set => _event_Scheduler__IsActive = value;
        }

        internal Event_Scheduler()
        {
            _Event_Scheduler__EVENT_CATALOG = new Dictionary<Event_Handle, Event>();
            _Event_Scheduler__RECURRING_NAME_COUNTS = new Dictionary<string, int>();
            _Event_Scheduler__ACTIVE_EVENTS = new List<Event>();

            Event_Scheduler__IsActive = false;
        }
        
        public bool CheckIf__Event_Is_Active__Event_Scheduler(Event_Handle eventHandle)
        {
            Event @event =
                _Event_Scheduler__EVENT_CATALOG.ContainsKey(eventHandle)
                    ? _Event_Scheduler__EVENT_CATALOG[eventHandle]
                    : null;
            if(@event == null)
            {
                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__EVENT_SCHEDULER__CHECK__EVENT_NOT_FOUND_1,
                    this,
                    eventHandle
                );
                return false;
            }

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
            Event timedEvent
        )
        {
            Event_Handle validatedHandle = Private_Validate__Recurring_EventHandle__Event_Scheduler(desiredHandle);
            
            _Event_Scheduler__EVENT_CATALOG.Add(validatedHandle, timedEvent);
            timedEvent.Internal_Bind__Event_Handle__Event(validatedHandle);

            return validatedHandle;
        }

        private Event_Handle Private_Validate__Recurring_EventHandle__Event_Scheduler(string eventHandle)
        {
            if (_Event_Scheduler__RECURRING_NAME_COUNTS.ContainsKey(eventHandle))
            {
                _Event_Scheduler__RECURRING_NAME_COUNTS[eventHandle]++;

                Event_Handle newEventHandle = Private_Get__Latest_Recurring_EventHandle__Event_Scheduler(eventHandle);

                Log.Internal_Write__Info__Log

                (
                    Log.INFO__EVENT_HANDLER__RECURRING_NAME_2,
                    this,
                    eventHandle,
                    newEventHandle
                );

                return newEventHandle;
            }
            
            _Event_Scheduler__RECURRING_NAME_COUNTS.Add(eventHandle, 0);
            return Private_Get__Latest_Recurring_EventHandle__Event_Scheduler(eventHandle);
        }
        
        private Event_Handle Private_Get__Latest_Recurring_EventHandle__Event_Scheduler(string eventHandle)
        {
            int count = _Event_Scheduler__RECURRING_NAME_COUNTS[eventHandle];

            string handle = String.Format(RECURRING_EVENTHANDLE_FORMAT, eventHandle, count);

            return new Event_Handle(handle);
        }
        
        /// <summary>
        /// Removes the event if it exists, and will cause it to reset prior to removal.
        /// </summary>
        /// <param name="eventHandle"></param>
        /// <returns></returns>
        public bool Remove__Event__Event_Scheduler(Event_Handle eventHandle)
        {
            bool ret = _Event_Scheduler__EVENT_CATALOG.ContainsKey(eventHandle);

            if (!ret)
            {
                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__EVENT_SCHEDULER__REMOVE__EVENT_NOT_FOUND_1,
                    this,
                    eventHandle
                );
                return false;
            }

            Event @event = _Event_Scheduler__EVENT_CATALOG[eventHandle];
            
            if(CheckIf__Event_Is_Active__Event_Scheduler(@event))
                @event.Internal_Reset__Event();
            
            _Event_Scheduler__EVENT_CATALOG.Remove(eventHandle);

            return ret;
        }

        public void Invoke__Event__Event_Scheduler(Event_Handle eventHandle, double newTime = -1)
        {
            if (!_Event_Scheduler__EVENT_CATALOG.ContainsKey(eventHandle))
            {
                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__EVENT_SCHEDULER__INVOKE__EVENT_NOT_FOUND_1,
                    this,
                    eventHandle
                );
                return;
            }
            
            Event subject = _Event_Scheduler__EVENT_CATALOG[eventHandle];
            
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
