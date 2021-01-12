using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Keyboard
{
    /// <summary>
    /// A key has been pressed.
    /// </summary>
    public class KeyboardPressEventArgument : KeyboardEventArgument
    {
        private KeyPressEventArgs keyPress;

        public KeyboardPressEventArgument(KeyPressEventArgs keyPress, bool isClosingEvent = false)
            : base (isClosingEvent)
        {
            this.keyPress = keyPress;
        }

        public KeyPressEventArgs KeyPress { get => keyPress; private set => keyPress = value; }
    }
}
