using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Tools
{
    public class TimedCallback
    {
        private Timer Timer { get; set; }
        private Action TimeElapsed_Callback;
        private Action<double> TimeReset_Callback;
        private Action<Timer> TimeDelta_Callback;
        public bool Triggered { get; private set; }

        public TimedCallback(Timer timer, Action<Timer> deltaTimeCallback, Action elapsedCallback = null, Action<double> resetCallback = null)
        {
            Timer = timer;
            TimeDelta_Callback = deltaTimeCallback;
            TimeElapsed_Callback = elapsedCallback;
            TimeReset_Callback = resetCallback;
        }

        public TimedCallback(Action<Timer> deltaTimeCallback, float timeLimit = 1, Action callback = null, Action<double> resetCallback = null)
        {
            Timer = new Timer(timeLimit);
            TimeDelta_Callback = deltaTimeCallback;
            TimeElapsed_Callback = callback;
            TimeReset_Callback = resetCallback;
        }

        public bool Progress_DeltaTime(double deltaTime)
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

        public void Reset(double newTime = -1)
        {
            Triggered = false;
            Timer.Set(newTime);
            TimeReset_Callback?.Invoke(Timer.TimeLimit);
        }
    }
}
