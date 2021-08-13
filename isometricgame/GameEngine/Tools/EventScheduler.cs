using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Tools
{
    public class EventScheduler
    {
        private readonly Dictionary<string, TimedCallback> EventCatalog = new Dictionary<string, TimedCallback>();
        private readonly Dictionary<string, int> RecurringNames = new Dictionary<string, int>();
        private readonly List<TimedCallback> ActiveEvents = new List<TimedCallback>();
        private bool isActive = false;
        public bool IsActive => isActive;

        public bool CheckIf__Event_Is_Active__Event_Scheduler(TimedCallback @event)
        {
            return (ActiveEvents.Contains(@event));
        }

        public string Register_Event(string tag, TimedCallback timedEvent)
        {
            if (RecurringNames.ContainsKey(tag))
            {
                RecurringNames[tag]++;
            }
            else
            {
                RecurringNames.Add(tag, -1);
            }

            string bindingTag = tag + "_" + RecurringNames[tag];
            EventCatalog.Add(bindingTag, timedEvent);
            timedEvent.Bind_To_Schedule(this, bindingTag);

            return bindingTag;
        }
        public bool Remove_Event(string tag)
        {
            bool ret = EventCatalog.ContainsKey(tag);

            if (ret)
                EventCatalog.Remove(tag);

            return ret;
        }
        public void Invoke_Event(string tag, double newTime = -1)
        {
            TimedCallback subject = EventCatalog[tag];
            subject.Reset(newTime);
            ActiveEvents.Add(subject);
            isActive = true;
        }

        internal bool Progress_Events(double deltaTime)
        {
            if (!isActive)
                return false;

            bool ret, fullret = false;

            foreach(TimedCallback subject in ActiveEvents.ToList())
            {
                ret = subject.Progress_DeltaTime(deltaTime);
                if (ret)
                {
                    ActiveEvents.Remove(subject);
                }
                fullret = fullret || !ret;
            }
            
            return isActive = fullret;
        }
    }
}
