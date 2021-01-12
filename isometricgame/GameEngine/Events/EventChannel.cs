using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Events
{
    public delegate void GameEventHandler<T>(GameObject sender, T args) where T : GameEventArgument;

    public abstract class EventChannel
    {
        public abstract void Register<T>(GameEventHandler<T> handler) where T : GameEventArgument;

        public abstract void Send<T>(GameObject sender, T args) where T : GameEventArgument;

        public abstract void CloseChannel();
    }
}
