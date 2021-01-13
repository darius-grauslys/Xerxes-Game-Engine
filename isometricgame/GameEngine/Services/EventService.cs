using isometricgame.GameEngine.Events;
using isometricgame.GameEngine.Exceptions.Events;
using isometricgame.GameEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services
{
    public class EventService : GameSystem
    {
        /// <summary>
        /// Global Broadcasts are defined while the EventService is NOT live. The are used to cross communicate through scenes.
        /// </summary>
        private BroadcastNetwork globalNetwork = new BroadcastNetwork();
        //TODO: Perhaps sender needs to be null... and in which case a rework needs to be done.

        /// <summary>
        /// Broadcasts registered under scene references. These are only accessible by the scenes.
        /// </summary>
        private List<BroadcastNetwork> localNetworks = new List<BroadcastNetwork>();

        private bool isLive = false;

        public EventService(Game game) 
            : base(game)
        {
        }

        public void ToggleLive()
        {
            isLive = true;
        }

        public void RegisterGlobal<T>(T channel) where T : Broadcast
        {
            if (!isLive)
            {
                globalNetwork.RegisterBroadcast(channel);
            }
            else
            {
                throw new LiveRegistryException();
            }
        }

        public BroadcastNetwork RegisterNetwork()
        {
            return new BroadcastNetwork();
        }
    }
}
