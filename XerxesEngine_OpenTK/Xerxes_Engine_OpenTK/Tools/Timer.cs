namespace Xerxes.Xerxes_OpenTK
{
    /// <summary>
    /// Keeps track of time. Used to fire events.
    /// A time limit of negative value will result
    /// in a never ending timer.
    /// </summary>
    public class Timer
    {
        public bool Timer__Loops                { get; private set; }

        public double Timer__Delta_Time         { get; private set; }
        public double Timer__Time_Elapsed       { get; private set; }

        public double Timer__Time_Limit         { get; private set; }
        private double _Timer__Default_Limit    { get; }
        public bool Timer__IsFinished           { get; private set; }

        public Timer(double timerDefaultLimit = 1, bool timerLoops = false)
        {
            _Timer__Default_Limit = timerDefaultLimit;
            Timer__Loops = timerLoops;
            Set__Timer();
        }

        public void Set__Timer(double timeLimit =-1, bool? timerLoops = null) 
        {
            Timer__Loops = timerLoops ?? Timer__Loops;
            Timer__IsFinished = false;
            Timer__Time_Limit = 
                (timeLimit > 0) 
                ? timeLimit 
                : _Timer__Default_Limit;
            Timer__Time_Elapsed = 0;
        }

        public void Progress__Timer(double deltaTime)
        {
            Timer__Delta_Time = deltaTime;
            Timer__IsFinished = Timer__Time_Elapsed >= Timer__Time_Limit && Timer__Time_Limit > 0;

            if(Timer__IsFinished)
            {
                if (!Timer__Loops)
                    return;
                Timer__Time_Elapsed = 0;
            }

            Timer__Time_Elapsed += deltaTime;
        }
    }
}
