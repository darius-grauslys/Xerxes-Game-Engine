using isometricgame.GameEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Mouse
{
    public class MouseBroadcast : Broadcast
    {
        private MouseButtonEventChannel mouseButtonChannel = new MouseButtonEventChannel();
        private MouseMoveEventChannel mouseMoveChannel = new MouseMoveEventChannel();

        public MouseBroadcast() : base()
        {
            Channels.Add(mouseButtonChannel);
        }

        /// <summary>
        /// Gets the first instance of the channel with the specified type. Returns null on failure.
        /// </summary>
        /// <typeparam name="T">Channel type.</typeparam>
        /// <returns></returns>
        protected override T FindChannel<T>()
        {
            return FindChannel<T>(Channels);
        }
    }
}
