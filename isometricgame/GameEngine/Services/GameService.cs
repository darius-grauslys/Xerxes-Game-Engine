using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services
{
    public class GameSystem
    {
        private Game gameRef;

        protected Game GameRef { get => gameRef; set => gameRef = value; }

        public GameSystem(Game gameRef)
        {
            GameRef = gameRef;
        }
    }
}
