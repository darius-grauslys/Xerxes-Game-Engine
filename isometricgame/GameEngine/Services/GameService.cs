using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services
{
    /// <summary>
    /// The Attribute equivalent for the Game object. Used for ContentPipes, Audio, whatever.
    /// </summary>
    public class GameService
    {
        public GameService(Game game)
        {
            Initalize(game);
        }

        internal virtual void Initalize(Game game)
        {

        }
    }
}
