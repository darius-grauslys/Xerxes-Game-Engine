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
        private readonly List<TimedCallback> ActiveEvents = new List<TimedCallback>();
        private bool isActive = false;
        public bool IsActive => isActive;

        public void Register_Event(string tag, TimedCallback timedEvent)
            => EventCatalog.Add(tag, timedEvent);
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

        public bool Progress_Events(double deltaTime)
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
