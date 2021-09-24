using System;
using OpenTK.Graphics.OpenGL;

namespace Xerxes_Engine
{
    /// <summary>
    /// Represents a collection of verticies with the intent
    /// of representing something during render.
    /// </summary>
    public class Vertex_Object : IDisposable
    {
        public const int VERTEX_OBJECT__BASE_VERTEX_COUNT = 4;

        public Vertex[] Vertex_Object__Vertices { get; internal set; }
        public Texture_R2 Vertex_Object__Texture_R2 { get; }

        public int Vertex_Object__GL_ID { get; private set; }

        internal Vertex_Object(Vertex[] vertices)
        {
            Vertex_Object__GL_ID = GL.GenBuffer();
            Vertex_Object__Texture_R2 = default(Texture_R2);
            this.Vertex_Object__Vertices = vertices;
        }

        internal void Internal_Use__Vertex_Object()
        {
            GL.BindTexture(TextureTarget.Texture2D, Vertex_Object__Texture_R2.ID);
        }

        public void Internal_Set__Buffer_Data__Vertex_Object()
        {
            Internal_Bind__Vertex_Object();
            GL.BufferData
            (
                BufferTarget.ArrayBuffer, 
                Vertex_Object__Vertices.Length * Vertex.SizeInBytes, 
                Vertex_Object__Vertices, 
                BufferUsageHint.StaticDraw
            );
        }

        internal void Internal_Bind__Vertex_Object()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Vertex_Object__GL_ID);
        }

        /// <summary>
        /// This should be moved to Render_Service.
        /// </summary>
        [Obsolete]
        public void Internal_Unbind__Vertex_Object()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(Vertex_Object__GL_ID);
        }

        /// <summary>
        /// Creates a new Vertex_Object using a given texture.
        /// It does not do any batching.
        /// Consider moving this to Render_Service?
        /// </summary>
        public static Vertex_Object Create
        (
            Texture_R2 texture_R2
        )
        {
            
        }
        
        public static Vertex[] Extract__Splice
        (
            float width,
            float height,
            float subWidth,
            float subHeight,
            int row,
            int col,
            float offsetX = 0,
            float offsetY = 0,
            float r = 0,
            float g = 0,
            float b = 0,
            float a = 0
        )
        {
            float textCoord_X = subWidth / width;
            float textCoord_Y = subHeight / height;

            float textCoord_X_A = textCoord_X * col;
            float textCoord_X_B = textCoord_X * (col + 1);
            float textCoord_Y_A = textCoord_Y * row;
            float textCoord_Y_B = textCoord_Y * (row + 1);

            float x_a = offsetX, 
                  y_a = offsetY;
            float x_b = offsetX, 
                  y_b = offsetY + subHeight;
            float x_c = offsetX + subWidth, 
                  y_c = y_b;
            float x_d = x_c, 
                  y_d = y_b;

            Vertex[] vertices = new Vertex[]
            {
                Vertex.Create(x_a, y_a,  textCoord_X_A, textCoord_Y_B,   r,g,b,a),
                Vertex.Create(x_b, y_b,  textCoord_X_A, textCoord_Y_A,   r,g,b,a),
                Vertex.Create(x_c, y_c,  textCoord_X_B, textCoord_Y_A,   r,g,b,a),
                Vertex.Create(x_d, y_d,  textCoord_X_B, textCoord_Y_B,   r,g,b,a)
            };

            return vertices;
        }

        public static Vertex_Object Batch
        (
        )
        {

        }
    }
}
