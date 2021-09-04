namespace XerxesEngine
{
    public class Timer
    {
        public double Timer__Delta_Time { get; private set; }
        public double Timer__Time_Elapsed { get; private set; }

        public double Timer__Time_Limit { get; private set; }
        private double _Timer__Default_Limit { get; }
        public bool Timer__IsFinished { get; private set; }

        public Timer(double timerDefaultLimit = 1)
        {
            _Timer__Default_Limit = timerDefaultLimit;
            Set__Timer();
        }

        public void Set__Timer(double timeLimit =-1)
        {
            Timer__IsFinished = false;
            Timer__Time_Limit = (timeLimit > 0) ? timeLimit : _Timer__Default_Limit;
            Timer__Time_Elapsed = 0;
        }

        public void Progress__Timer(double deltaTime)
        {
            Timer__Delta_Time = deltaTime;
            if (!(Timer__IsFinished = (Timer__Time_Elapsed >= Timer__Time_Limit)))
            {
                Timer__Time_Elapsed += deltaTime;
            }
        }
    }
}
