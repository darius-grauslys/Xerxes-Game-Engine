using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Tools
{
    public class TimedCallback
    {
        private EventScheduler EventScheduler { get; set; }

        public string EventTag { get; internal set; }

        private Timer Timer { get; set; }
        public double Duration => Timer.TimeLimit;
        private Action TimeElapsed_Callback;
        private Action<double> TimeReset_Callback;
        private Action<Timer> TimeDelta_Callback;
        public bool Triggered { get; private set; }

        public TimedCallback(
            double timeLimit=1, 
            Action<Timer> deltaTimeCallback=null, 
            Action elapsedCallback = null, 
            Action<double> resetCallback = null)
        {
            Timer = new Timer(timeLimit);
            TimeDelta_Callback = deltaTimeCallback;
            TimeElapsed_Callback = elapsedCallback;
            TimeReset_Callback = resetCallback;
        }

        internal void Bind_To_Schedule(EventScheduler eventScheduler, string givenTag)
        {
            EventTag = givenTag;
            EventScheduler = eventScheduler;
        }

        internal bool Progress_DeltaTime(double deltaTime)
        {
            if (Timer.Finished)
            {
                TimeElapsed_Callback?.Invoke();
                Triggered = true;
                return Triggered;
            }

            Timer.Increase_DeltaTime(deltaTime);
            TimeDelta_Callback?.Invoke(Timer);

            return false;
        }

        internal void Reset(double newTime = -1)
        {
            Triggered = false;
            Timer.Set(newTime);
            TimeReset_Callback?.Invoke(Timer.TimeLimit);
        }

        public void Invoke()
        {
            EventScheduler?.Invoke_Event(EventTag);
        }
    }
}
