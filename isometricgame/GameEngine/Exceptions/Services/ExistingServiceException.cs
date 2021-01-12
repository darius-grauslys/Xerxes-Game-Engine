using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Exceptions.Services
{
    /// <summary>
    /// Thrown when a GameService of a certain type is already registered.
    /// </summary>
    public class ExistingServiceException : Exception
    {
    }
}
