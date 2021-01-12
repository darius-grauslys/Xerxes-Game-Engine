using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Exceptions.Events
{
    /// <summary>
    /// Thrown whenever a broadcast is registered to a network twice.
    /// </summary>
    public class DoubleBroadcastRegisterException : Exception
    {
    }
}
