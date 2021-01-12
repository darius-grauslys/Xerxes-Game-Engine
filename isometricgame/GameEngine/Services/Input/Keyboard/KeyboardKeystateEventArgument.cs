using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Keyboard
{
    /// <summary>
    /// A key has become down or up.
    /// </summary>
    public class KeyboardKeystateEventArgument : KeyboardEventArgument
    {
        private KeyboardKeyEventArgs keystate;

        public KeyboardKeyEventArgs Keystate { get => keystate; private set => keystate = value; }

        public KeyboardKeystateEventArgument(KeyboardKeyEventArgs keystate, bool isClosingEvent = false) 
            : base(isClosingEvent)
        {
            this.keystate = keystate;
        }
    }
}
