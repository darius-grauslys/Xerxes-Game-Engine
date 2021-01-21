using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Events.Arguments
{
    public class FrameArgument
    {
        /// <summary>
        /// Total elapsed time since game launch.
        /// </summary>
        public readonly double Time;

        /// <summary>
        /// Time since last loop.
        /// </summary>
        public readonly double DeltaTime;

        public FrameArgument(double time, double deltaTime) { Time = time; DeltaTime = deltaTime; }
    }
}
