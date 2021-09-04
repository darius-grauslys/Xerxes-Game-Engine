using System;
using XerxesEngine.Events.Event_Arguments;

namespace XerxesEngine.Events
{
    public class Event
    {
        public Event_Handle Event__Handle { get; internal set; }

        private Timer _Event__Timer { get; set; }
        public double Event__Duration => _Event__Timer.Timer__Time_Limit;
        private Action _Event__Elapsed__Callback;
        private Action<Event_Reset_Argument> _Event__Reset__Callback;
        private Action<Event_Progression_Argument> _Event__Progress__Callback;
        public bool Event__Triggered { get; private set; }

        public Event(
            double timeLimit=1, 
            Action<Event_Progression_Argument> progress_Callback=null, 
            Action elapsed_Callback = null, 
            Action<Event_Reset_Argument> reset_Callback = null)
        {
            _Event__Timer = new Timer(timeLimit);
            _Event__Progress__Callback = progress_Callback;
            _Event__Elapsed__Callback = elapsed_Callback;
            _Event__Reset__Callback = reset_Callback;
        }

        internal void Internal_Bind__Event_Handle__Event(Event_Handle eventHandle)
        {
            Event__Handle = eventHandle;
        }

        internal bool Internal_Progress__Delta_Time__Event(double deltaTime)
        {
            if (_Event__Timer.Timer__IsFinished)
            {
                _Event__Elapsed__Callback?.Invoke();
                Event__Triggered = true;
                return Event__Triggered;
            }

            Event_Progression_Argument progressionArgument = new Event_Progression_Argument
            (
                _Event__Timer.Timer__Delta_Time,
                _Event__Timer.Timer__Time_Elapsed,
                _Event__Timer.Timer__Time_Limit
            );
            
            _Event__Timer.Progress__Timer(deltaTime);
            _Event__Progress__Callback?.Invoke(progressionArgument);

            return false;
        }

        internal void Internal_Reset__Event(double newTime = -1)
        {
            Event__Triggered = false;
            _Event__Timer.Set__Timer(newTime);

            Event_Reset_Argument resetArgument = new Event_Reset_Argument
            (
                _Event__Timer.Timer__Time_Limit
            );
            
            _Event__Reset__Callback?.Invoke(resetArgument);
        }
    }
}
