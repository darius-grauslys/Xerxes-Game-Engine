using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Exceptions.WorldSpace
{
    /// <summary>
    /// This exception is thrown when the ChunkService class cannot delimit a tile based on a provided position.
    /// </summary>
    public class CouldNotDelimitException : Exception
    {
    }
}
