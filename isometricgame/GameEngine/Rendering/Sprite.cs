using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace isometricgame.GameEngine.Rendering
{
    public class Sprite : IDisposable
    {
        private Vertex[] vertices;
        private int vertexBufferObject;
        private int vertexArrayObject;
        private Texture2D texture;

        private bool flippedX, flippedY;

        private float offsetX, offsetY;

        public Vertex[] Vertices => vertices;
        public Texture2D Texture => texture;

        public float OffsetX { get => offsetX; set => offsetX = value; }
        public float OffsetY { get => offsetY; set => offsetY = value; }
        public int VertexBufferObject { get => vertexBufferObject; private set => vertexBufferObject = value; }
        public int VertexArrayObject { get => vertexArrayObject; private set => vertexArrayObject = value; }

        public Sprite(Texture2D texture, Vertex[] vertices = null)
        {
            if (vertices == null)
            {
                this.vertices = new Vertex[]
                {
                    new Vertex(new Vector2(0, 0), new Vector2(1, 0)),
                    new Vertex(new Vector2(0, texture.Height), new Vector2(1, 1)),
                    new Vertex(new Vector2(texture.Width, texture.Height), new Vector2(0, 1)),
                    new Vertex(new Vector2(texture.Width, 0), new Vector2(0, 0)),
                };
            }
            else
            {
                this.vertices = vertices;
            }

            flippedX = false;
            flippedY = false;

            VertexBufferObject = GL.GenBuffer();
            VertexArrayObject = GL.GenVertexArray();
            
            this.texture = texture;

            offsetX = 0;
            offsetY = 0;

            BindSprite();
        }

        //I think this goes here.
        private void BindSprite()
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertex.SizeInBytes * vertices.Length), vertices, BufferUsageHint.StaticDraw);
            
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 2 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 8 * sizeof(float), 4 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Flip(bool x=true, bool y=true)
        {
            if (x)
            {
                vertices[0] = new Vertex(new Vector2(0, 0), flippedX ? new Vector2(1, 0) : new Vector2(0, 0));
                vertices[2] = new Vertex(new Vector2(texture.Width, texture.Height), flippedX ? new Vector2(0, 1) : new Vector2(1, 1));
                flippedX = !flippedX;
            }
            if (y)
            {
                vertices[1] = new Vertex(new Vector2(0, texture.Height), flippedY ? new Vector2(1, 1) : new Vector2(0, 1));
                vertices[3] = new Vertex(new Vector2(texture.Width, 0), flippedY ? new Vector2(0, 0) : new Vector2(1, 0));
                flippedY = !flippedY;
            }
        }

        public void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        }
    }
}
