using isometricgame.GameEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Keyboard
{
    public class KeyboardPressEventChannel : EventChannel
    {
        private GameEventHandler<KeyboardPressEventArgument> keyboardPressEvent;

        public override void CloseChannel()
        {
            keyboardPressEvent?.Invoke(null, new KeyboardPressEventArgument(null, true));
        }

        public override void Register<T>(GameEventHandler<T> handler)
        {
            Register(handler as GameEventHandler<KeyboardPressEventArgument>);
        }

        public void Register(GameEventHandler<KeyboardPressEventArgument> handler)
        {
            keyboardPressEvent += handler;
        }

        public override void Send<T>(GameObject sender, T args)
        {
            Send(sender, args);
        }

        public void Send(GameObject sender, KeyboardPressEventArgument args)
        {
            keyboardPressEvent?.Invoke(sender, args);
        }
    }
}
