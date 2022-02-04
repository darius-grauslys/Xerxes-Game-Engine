using OpenTK;
using System;
using System.Drawing;
using Math_Helper = Xerxes.Tools.Math_Helper;

namespace Xerxes.Xerxes_OpenTK
{
    public struct Vertex
    {
        public Vector3 Position { get; set; }
        public Vector2 Texture_Coordinate { get; set; } 
        public Vector4 Color_Vector { get; set; }

        public Color Color
        {
            get => 
                Color
                .FromArgb
                (
                    (int)(Color_Vector.W * 255), 
                    (int)(Color_Vector.X * 255), 
                    (int)(Color_Vector.Y * 255), 
                    (int)(Color_Vector.Z * 255)
                );
            set
            {
                Color_Vector = Math_Helper.Convert__Color_To_Vec4(value);
            }
        }

        public static int Size_In_Bytes => Vector3.SizeInBytes + Vector2.SizeInBytes + Vector4.SizeInBytes;

        public override string ToString()
        {
            return String.Format
                (
                    "p:{0},t:{1},c:{2}",
                    Position,
                    Texture_Coordinate,
                    Color_Vector 
                );
        }

        public Vertex(Vector3 position, Vector2 textcoord, Vector4 color)
        {
            Position = position;
            Texture_Coordinate = textcoord;
            Color_Vector = color;
        }

        public Vertex(Vector2 position, Vector2 textcoord, Vector4 color)
        : this
        (
            new Vector3(position),
            textcoord,
            color
        )
        {}

        public Vertex
        (
            float x, float y, float z, 
            float tx, float ty,
            float r, float g, float b, float a
        )
        : this
        (
            new Vector3(x,y,z),
            new Vector2(tx, ty),
            new Vector4(r,g,b,a)
        )
        {}
    }
}
