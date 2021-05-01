using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Systems
{
    public class GameSystem
    {
        protected Game Game { get; set; }
        public bool Accessable { get; private set; }

        public GameSystem(Game game, bool accessable = true)
        {
            Game = game;
            Accessable = accessable;
        }

        public virtual void Load()
        {

        }

        public virtual void Unload()
        {

        }
    }
}
