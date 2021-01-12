using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Events
{
    public class GameEventArgument
    {
        private bool isClosingEvent;

        public bool IsClosingEvent { get => isClosingEvent; private set => isClosingEvent = value; }

        public GameEventArgument(bool isClosingEvent = false)
        {
            this.isClosingEvent = isClosingEvent;
        }
    }
}
