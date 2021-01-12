using isometricgame.GameEngine;
using isometricgame.Isogame;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow(1200, 900);
            IsoGame game = new IsoGame(window);

            window.Run();
        }
    }
}
