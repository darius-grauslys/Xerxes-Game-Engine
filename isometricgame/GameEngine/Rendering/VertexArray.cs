using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace isometricgame.GameEngine.Rendering
{
    public class VertexArray : IDisposable
    {
        public static readonly int VERTEXARRAY_INDEX_COUNT = 4;

        public Vertex[] Vertices { get; internal set; }
        private Texture2D texture;

        public int VertexBufferObject { get; private set; }

        private bool flippedX, flippedY;

        public Texture2D Texture => texture;

        internal VertexArray(Vertex[] vertices)
        {
            VertexBufferObject = GL.GenBuffer();
            flippedX = false;
            flippedY = false;
            texture = default(Texture2D);
            this.Vertices = vertices;
        }

        public VertexArray(Texture2D texture, Vertex[] vertices = null,
            float r = 0,
            float g = 0,
            float b = 0,
            float a = 0)
        {
            VertexBufferObject = GL.GenBuffer();
                
            if (vertices == null)
            {
                this.Vertices = VerticesFromDimensions(texture.Width, texture.Height, -1, -1, 0, 0, r, g, b, a);
            }
            else
            {
                this.Vertices = vertices;
            }

            flippedX = false;
            flippedY = false;
            
            this.texture = texture;
        }

        public void Use()
        {
            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
        }
        
        //potentially remove?
        public void Flip(bool x=true, bool y=true)
        {
            if (x)
            {
                Vertices[0] = new Vertex(new Vector2(0, 0), flippedX ? new Vector2(1, 0) : new Vector2(0, 0));
                Vertices[2] = new Vertex(new Vector2(texture.Width, texture.Height), flippedX ? new Vector2(0, 1) : new Vector2(1, 1));
                flippedX = !flippedX;
            }
            if (y)
            {
                Vertices[1] = new Vertex(new Vector2(0, texture.Height), flippedY ? new Vector2(1, 1) : new Vector2(0, 1));
                Vertices[3] = new Vertex(new Vector2(texture.Width, 0), flippedY ? new Vector2(0, 0) : new Vector2(1, 0));
                flippedY = !flippedY;
            }
        }

        public void SetBufferData()
        {
            BindVertexBuffer();
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vertex.SizeInBytes, Vertices, BufferUsageHint.StaticDraw);
        }

        public void BindVertexBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        }

        //remove
        public void UnbindVertexBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        }

        /// <summary>
        /// This function is not loop friendly, but it doesn't matter right now as this is not used in the main loop.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="maxWidth"></param>
        /// <param name="height"></param>
        /// <param name="maxHeight"></param>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public static Vertex[] VerticesFromDimensions(
            float width, 
            float height, 
            float subWidth=-1, 
            float subHeight=-1, 
            int indexX=0, 
            int indexY=0, 
            float r = 0,
            float g = 0,
            float b = 0,
            float a = 0)
        {
            float vertX = (subWidth > 0) ? subWidth / width : 1;
            float vertY = (subHeight > 0) ? subHeight / height : 1;

            float vertXi1 = vertX * indexX;
            float vertXi2 = vertX * (indexX + 1);
            float vertYi1 = vertY * indexY;
            float vertYi2 = vertY * (indexY + 1);

            return new Vertex[]
                {
                    new Vertex(new Vector2(0, 0), new Vector2(vertXi1, vertYi2), r, g, b, a),
                    new Vertex(new Vector2(0, subHeight), new Vector2(vertXi1, vertYi1), r, g, b, a),
                    new Vertex(new Vector2(subWidth, subHeight), new Vector2(vertXi2, vertYi1), r, g, b, a),
                    new Vertex(new Vector2(subWidth, 0), new Vector2(vertXi2, vertYi2), r, g, b, a),
                };
        }
    }
}
