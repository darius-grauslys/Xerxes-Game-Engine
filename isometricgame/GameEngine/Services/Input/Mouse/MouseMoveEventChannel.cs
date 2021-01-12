using isometricgame.GameEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Mouse
{
    public class MouseMoveEventChannel : EventChannel
    {
        private GameEventHandler<MouseMoveEventArgument> mouseMoveEvent;

        public override void CloseChannel()
        {
            mouseMoveEvent?.Invoke(null, new MouseMoveEventArgument(null, true));
        }

        public override void Register<T>(GameEventHandler<T> handler)
        {
            Register(handler as GameEventHandler<MouseMoveEventArgument>);
        }

        public void Register(GameEventHandler<MouseMoveEventArgument> handler)
        {
            mouseMoveEvent += handler;
        }

        public override void Send<T>(GameObject sender, T args)
        {
            Send(sender, args as MouseButtonEventArgument);
        }

        public void Send(GameObject sender, MouseMoveEventArgument args)
        {
            mouseMoveEvent?.Invoke(sender, args);
        }
    }
}
