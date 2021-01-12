using isometricgame.GameEngine.Events;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Mouse
{
    public class MouseButtonEventChannel : EventChannel
    {
        private GameEventHandler<MouseButtonEventArgument> mouseButtonEvent;
        
        public override void CloseChannel()
        {
            mouseButtonEvent?.Invoke(null, new MouseButtonEventArgument(null, true));
        }

        public override void Register<T>(GameEventHandler<T> handler)
        {
            Register(handler as GameEventHandler<MouseButtonEventArgument>);
        }

        public void Register(GameEventHandler<MouseButtonEventArgument> handler)
        {
            mouseButtonEvent += handler;
        }

        public override void Send<T>(GameObject sender, T args)
        {
            Send(sender, args as MouseButtonEventArgument);
        }

        public void Send(GameObject sender, MouseButtonEventArgument args)
        {
            mouseButtonEvent?.Invoke(sender, args);
        }
    }
}
