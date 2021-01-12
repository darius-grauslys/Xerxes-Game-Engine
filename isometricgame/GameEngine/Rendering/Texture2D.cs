using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Rendering
{
    public struct Texture2D
    {
        private int id;
        private Vector2 size;

        public Texture2D(int texid, Vector2 texsize)
        {
            id = texid;
            size = texsize;
        }

        public int ID => id;
        public Vector2 Size => size;
        public int Width => (int)size.X;
        public int Height => (int)size.Y;

        public int Area => Width * Height;
    }
}
