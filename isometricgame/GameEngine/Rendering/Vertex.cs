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

        public Vector2 Position { get => position; set => position = value; }
        public Vector2 TextCoord => textcoord;
        public Vector4 ColorVector { get => color; set => color = value; }

        public Color Color
        {
            get => Color.FromArgb((int)(color.W * 255), (int)(color.X * 255), (int)(color.Y * 255), (int)(color.Z * 255));
            set
            {
                color = Systems.MathHelper.Color_To_Vec4(value);
            }
        }

        public static int SizeInBytes => Vector2.SizeInBytes * 2 + Vector4.SizeInBytes;

        public Vertex(Vector2 position, Vector2 textcoord, Vector4 color)
        {
            this.position = position;
            this.textcoord = textcoord;
            this.color = color;
        }

        public Vertex(Vector2 position, Vector2 textcoord, float r=0, float g=0, float b=0, float a=1)
        {
            this.position = position;
            this.textcoord = textcoord;
            this.color = new Vector4(r,g,b,a);
        }
    }
}
