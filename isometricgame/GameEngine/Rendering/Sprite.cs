using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace isometricgame.GameEngine.Rendering
{
    /// <summary>
    /// Use this for sprites that have multiple versions.
    /// </summary>
    public class Sprite
    {
        private string name;
        private Texture2D texture;

        private int[] vertexArrayObjects;

        private int subWidth;
        private int subHeight;

        private int vboIndex = 0;

        private float offsetX, offsetY;

        private int columnCount, rowCount;

        private int count;

        private VertexArray[] vertexArrays;
        
        public int[] VertexArrayObjects { get => vertexArrayObjects; set => vertexArrayObjects = value; }

        public string Name => name;

        public int SubWidth { get => subWidth; private set => subWidth = value; }
        public int SubHeight { get => subHeight; private set => subHeight = value; }
        public Texture2D Texture { get => texture; private set => texture = value; }

        public int VBO_Index { get => vboIndex; set => vboIndex = value; }
        public float OffsetX { get => offsetX; protected set => offsetX = value; }
        public float OffsetY { get => offsetY; protected set => offsetY = value; }

        public Sprite(Sprite s, int vboIndex = -1)
        {
            offsetX = s.offsetX;
            offsetY = s.offsetY;
            name = s.name;

            texture = s.texture;

            subWidth = s.subWidth;
            subHeight = s.subHeight;

            this.vboIndex = (vboIndex < 0) ? s.vboIndex : vboIndex;

            columnCount = s.columnCount;
            rowCount = s.rowCount;

            count = s.count;

            vertexArrays = new VertexArray[s.vertexArrays.Length];

            vertexArrayObjects = new int[s.vertexArrayObjects.Length];

            for (int i = 0; i < s.vertexArrayObjects.Length; i++)
                vertexArrayObjects[i] = s.vertexArrayObjects[i];

            for (int i = 0; i < count; i++)
                vertexArrays[i] = s.vertexArrays[i];
        }

        public Sprite(
            Texture2D texture, 
            int subWidth, 
            int subHeight, 
            string name="", 
            float offsetX = 0, 
            float offsetY = 0, 
            int vboIndex = 0,
            float r = 0,
            float g = 0,
            float b = 0,
            float a = 0)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.name = name;

            this.texture = texture;

            this.subWidth = subWidth;
            this.subHeight = subHeight;

            this.vboIndex = vboIndex;

            columnCount = texture.Width / subWidth;
            rowCount = texture.Height / subHeight;

            count = columnCount * rowCount;

            vertexArrays = new VertexArray[count];

            vertexArrayObjects = new int[count];

            for (int i = 0; i < vertexArrayObjects.Length; i++)
                vertexArrayObjects[i] = GL.GenVertexArray();

            Vertex[] vertices;

            for(int y = 0; y < rowCount; y++)
            {
                for (int x = 0; x < columnCount; x++)
                {
                    vertices = VertexArray.VerticesFromDimensions(texture.Width, texture.Height, subWidth, subHeight, x, y, r, g, b, a);

                    vertexArrays[x + (y * columnCount)] = new VertexArray(texture, vertices);
                }
            }

            BindVertexArray();
        }

        public void Use(int index=0)
        {
            vertexArrays[index].Use();
            GL.BindVertexArray(vertexArrayObjects[index]);
        }

        private void BindVertexArray()
        {
            for (int i = 0; i < vertexArrays.Length; i++)
            {
                GL.BindVertexArray(vertexArrayObjects[i]);
                vertexArrays[i].BindVertexBuffer();

                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 2 * sizeof(float));
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 8 * sizeof(float), 4 * sizeof(float));
                GL.EnableVertexAttribArray(2);
            }

            GL.BindVertexArray(0);
        }
    }
}
