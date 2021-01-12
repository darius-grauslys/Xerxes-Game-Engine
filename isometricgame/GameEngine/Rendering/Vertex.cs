using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Rendering
{
    public struct Vertex
    {
        private Vector2 position;
        private Vector2 textcoord;
        private Vector4 color;

        public Vector2 Position => position;
        public Vector2 TextCoord => textcoord;

        public Color Color
        {
            get => Color.FromArgb((int)(color.W * 255), (int)(color.X * 255), (int)(color.Y * 255), (int)(color.Z * 255));
            set
            {
                this.color = new Vector4(value.R / 255f, value.G / 255f, value.B /255f, value.A / 255f);
            }
        }

        public static int SizeInBytes => Vector2.SizeInBytes * 2 + Vector4.SizeInBytes;

        public Vertex(Vector2 position, Vector2 textcoord)
        {
            this.position = position;
            this.textcoord = textcoord;
            this.color = new Vector4(1, 1, 1, 1);
        }
    }
}
