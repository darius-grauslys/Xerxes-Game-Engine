using isometricgame.GameEngine.Exceptions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Events
{
    public class BroadcastNetwork
    {
        private List<Broadcast> channels = new List<Broadcast>();

        /// <summary>
        /// Throws LockedChannelException if the accessed channel is locked.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetBroadcast<T>(int id) where T : Broadcast
        {
            foreach (T b in channels)
            {
                if (b.ID == id)
                {
                    if (!b.Locked)
                    {
                        return b;
                    }
                    else
                    {
                        throw new LockedChannelException();
                    }
                }
            }
            return null;
        }

        public void RegisterBroadcast(Broadcast b)
        {
            if (channels.Contains(b))
                throw new DoubleBroadcastRegisterException();
            channels.Add(b);
        }
    }
}
