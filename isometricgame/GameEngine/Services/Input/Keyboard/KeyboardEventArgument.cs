using isometricgame.GameEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Keyboard
{
    public class KeyboardEventArgument : GameEventArgument
    {
        public KeyboardEventArgument(bool isClosingEvent = false) : base(isClosingEvent) { }
    }
}
