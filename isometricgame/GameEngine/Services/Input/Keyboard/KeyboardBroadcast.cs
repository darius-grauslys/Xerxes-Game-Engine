using isometricgame.GameEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Keyboard
{
    public class KeyboardBroadcast : Broadcast
    {
        private KeyboardPressEventChannel keyboardPressEventChannel;
        private KeyboardKeystateEventChannel keyboardKeystateEventChannel;

        public KeyboardBroadcast()
            : base()
        {
            Channels.Add(keyboardPressEventChannel);
            Channels.Add(keyboardKeystateEventChannel);
        }

        protected override T FindChannel<T>()
        {
            return FindChannel<T>(Channels);
        }
    }
}
