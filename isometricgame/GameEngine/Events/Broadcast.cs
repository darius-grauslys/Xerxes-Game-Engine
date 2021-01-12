using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Events
{
    /// <summary>
    /// An event communication channel for the Iso GameEngine.
    /// </summary>
    public abstract class Broadcast
    {
        private static int BROADCAST_ID;

        private bool locked;
        private int id;

        private List<EventChannel> channels;

        /// <summary>
        /// If locked, this broadcast cannot be discovered. This field is read by the EventService.
        /// </summary>
        public bool Locked => locked;

        public int ID => id;

        protected List<EventChannel> Channels { get => channels; private set => channels = value; }

        public Broadcast(bool enabled = true, bool locked = false)
        {
            this.id = BROADCAST_ID++;
            //this.enabled = enabled;
            this.locked = locked;

            Channels = new List<EventChannel>();
        }

        public T GetChannel<T>() where T : EventChannel { if (locked) return null;  return FindChannel<T>(); }

        protected abstract T FindChannel<T>() where T : EventChannel;

        /// <summary>
        /// The generic channel search and return. Returns first instance of inquired type. Null on failure.
        /// </summary>
        /// <typeparam name="T">The type of channel inquired.</typeparam>
        /// <param name="channels"></param>
        /// <returns></returns>
        protected static T FindChannel<T>(List<EventChannel> channels) where T : EventChannel
        {
            foreach (T channel in channels)
                return channel;
            return null;
        }

        //public bool ToggleActivity()
        //{
        //    return ToggleActivity(!enabled);
        //}

        //public bool ToggleActivity(bool state)
        //{
        //    return (enabled = state);
        //}

        public bool ToggleLock()
        {
            return ToggleLock(!locked);
        }

        public bool ToggleLock(bool state)
        {
            return (locked = state);
        }
    }
}
