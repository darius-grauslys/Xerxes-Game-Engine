using OpenTK;
using System;
using System.Drawing;
using Math_Helper = Xerxes_Engine.Tools.Math_Helper;

namespace Xerxes_Engine
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
                color = Math_Helper.Convert__Color_To_Vec4(value);
            }
        }

        public static int SizeInBytes => Vector2.SizeInBytes * 2 + Vector4.SizeInBytes;

        public override string ToString()
        {
            return String.Format
                (
                    "p:{0},t:{1},c:{2}",
                    position,
                    textcoord,
                    color
                );
        }

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

        public Vertex
        (
            Vertex baseline,
            Vector2? nullable_Position = null,
            Vector2? nullable_Textcoord = null,
            Vector4? nullable_Color = null
        )
        {
            this.position = nullable_Position ?? baseline.position;
            this.textcoord = nullable_Textcoord ?? baseline.position;
            this.color = nullable_Color ?? baseline.color;
        }

        public static Vertex Create
        (
            float vector_X,
            float vector_Y,

            float textCoord_X,
            float textCoord_Y,

            float r = 0,
            float g = 0,
            float b = 0,
            float a = 0
        )
            => 
            new Vertex
            (
                new Vector2(vector_X, vector_Y), 
                new Vector2(textCoord_X, textCoord_Y),
                r, g, b, a
            );
    }
}
