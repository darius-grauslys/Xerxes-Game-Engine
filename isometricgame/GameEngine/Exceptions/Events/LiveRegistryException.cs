using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Exceptions.Events
{
    /// <summary>
    /// Thrown when a global broadcast registry is performed while the service is live.
    /// </summary>
    public class LiveRegistryException : Exception
    {
    }
}
