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

        public int Vertex_Object__GL_BUFFER_ID { get; private set; }
        public int Vertex_Object__GL_VERTEX_ARRAY_ID { get; private set; }

        internal Vertex_Object(Vertex[] vertices, Texture_R2 texture_R2)
            : this(vertices.Length, texture_R2)
        {
            Vertex_Object__Vertices = vertices;
            Internal_Set__Buffer_Data__Vertex_Object();
        }

        internal Vertex_Object(int count, Texture_R2 texture_R2)
        { 
            // Generate the buffer for vertice info on the gpu.
            Vertex_Object__GL_BUFFER_ID = GL.GenBuffer();
            // Generate the id for this object on the gpu.
            Vertex_Object__GL_VERTEX_ARRAY_ID = GL.GenVertexArray();
            Vertex_Object__Texture_R2 = texture_R2;
            Vertex_Object__Vertices = new Vertex[count];

            GL.BindVertexArray(Vertex_Object__GL_VERTEX_ARRAY_ID);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 2 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 8 * sizeof(float), 4 * sizeof(float));
            GL.EnableVertexAttribArray(2);
        }

        internal void Internal_Modify__Vertex_Array__Vertex_Object
        (
            int modificationIndex,
            Vertex[] modificaiton
        )
        {
            bool isValidIndex;
            int index;

            index = 
                Tools.Math_Helper
                .Clamp__Positive_Integer
                (
                    modificationIndex, 
                    out isValidIndex,
                    Vertex_Object__Vertices.Length
                );

            if (!isValidIndex)
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__Rendering_Setup,
                    Log.ERROR__VERTEX_OBJECT__INVALID_MODIFICATION_INDEX_2,
                    this,
                    index,
                    Vertex_Object__Vertices.Length
                );
                return;
            }

            int modificationRange = index + modificaiton.Length;
            bool isInvalidModificationLength = 
                 modificationRange < Vertex_Object__Vertices.Length;

            if (isInvalidModificationLength)
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__Rendering_Setup,
                    Log.ERROR__VERTEX_OBJECT__MODIFICATION_OUT_OF_BOUNDS_3,
                    index,
                    modificationRange,
                    Vertex_Object__Vertices.Length
                );
                return;
            }

            for(int i=index;i<modificaiton.Length;i++)
            {
                Vertex_Object__Vertices[i] = modificaiton[i-index];
            }
        }

#region Internal GL Initalizations
        internal void Internal_Set__Buffer_Data__Vertex_Object()
        {
            Internal_Bind__Buffer__Vertex_Object();
            GL.BufferData
            (
                BufferTarget.ArrayBuffer, 
                Vertex_Object__Vertices.Length * Vertex.SizeInBytes, 
                Vertex_Object__Vertices, 
                BufferUsageHint.StaticDraw
            );
        }
#endregion

#region Internal GL Utilizations
        internal void Internal_Use__Vertex_Object()
        {
            GL.BindTexture(TextureTarget.Texture2D, Vertex_Object__Texture_R2.ID);
            GL.BindVertexArray(Vertex_Object__GL_VERTEX_ARRAY_ID);
        }

        internal void Internal_Bind__Buffer__Vertex_Object()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Vertex_Object__GL_BUFFER_ID);
        }

        /// <summary>
        /// This should be moved to Render_Service.
        /// </summary>
        [Obsolete]
        public void Internal_Unbind__Vertex_Object()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
#endregion

        public void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(Vertex_Object__GL_BUFFER_ID);
        }
    }
}
