using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Tools
{
    public class Timer
    {
        private double deltaTime = 0;
        public double Frame_DeltaTime => deltaTime;
        private double timeElapsed = 0;
        public double TimeElapsed => timeElapsed;
        private double timeLimit;
        public double TimeLimit => timeLimit;
        private double defaultLimit;
        public bool Finished { get; private set; }

        public Timer(double defaultLimit = 1)
        {
            this.defaultLimit = defaultLimit;
            Set();
        }

        public void Set(double timeLimit =-1)
        {
            Finished = false;
            this.timeLimit = (timeLimit > 0) ? timeLimit : defaultLimit;
            timeElapsed = 0;
        }

        public void Increase_DeltaTime(double deltaTime)
        {
            this.deltaTime = deltaTime;
            if (!(Finished = (timeElapsed >= timeLimit)))
            {
                timeElapsed += deltaTime;
            }
        }
    }
}
