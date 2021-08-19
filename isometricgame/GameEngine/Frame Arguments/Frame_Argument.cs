using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Events.Arguments
{
    public class Frame_Argument
    {
        /// <summary>
        /// Total elapsed time since game launch.
        /// </summary>
        public readonly double Time;

        /// <summary>
        /// Time since last loop.
        /// </summary>
        public readonly double DeltaTime;

        internal Frame_Argument(double time, double deltaTime) { Time = time; DeltaTime = deltaTime; }
    }
}
