using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Xerxes_Engine.Systems.Graphics
{
    /// <summary>
    /// Use this for sprites that have multiple versions.
    /// </summary>
    public class Sprite
    {
        private string name;

        private int[] vertexArrayObjects;

        private int baseSubWidth;
        private int baseSubHeight;
        private Vector2 size;
        private float scale = 1;

        internal int vaoIndex = 0, vaoRow = 0;

        private float offsetX, offsetY;

        private int columnCount, rowCount;

        private int count;

        public Vertex_Array[] VertexArrays { get; set; }
        
        public int[] VertexArrayObjects { get => vertexArrayObjects; set => vertexArrayObjects = value; }

        public string Name => name;

        public float SubWidth { get => baseSubWidth * scale; private set => baseSubWidth = (int)value; }
        public float SubHeight { get => baseSubHeight * scale; private set => baseSubHeight = (int)value; }
        public Vector2 Size { get => size; set => SetSize(value); }
        public float Scale { get => scale; set => SetScale(value); }

        public uint VAO_Index { get => (uint)(vaoIndex + (VAO_Row * columnCount)); set => vaoIndex = (int)value; }
        public uint VAO_Row { get => (uint)vaoRow; set => vaoRow = (int)value; }
        public float OffsetX { get => offsetX; protected set => offsetX = value; }
        public float OffsetY { get => offsetY; protected set => offsetY = value; }

        public Sprite(Sprite s, int vboIndex = -1)
        {
            Vertex_Array[] vertexArrays = new Vertex_Array[s.VertexArrays.Length];

            int[] vertexArrayObjects = new int[s.vertexArrayObjects.Length];

            for (int i = 0; i < s.vertexArrayObjects.Length; i++)
                vertexArrayObjects[i] = s.vertexArrayObjects[i];

            for (int i = 0; i < count; i++)
                vertexArrays[i] = s.VertexArrays[i];

            Init(
                s.offsetX,
                s.offsetY,
                name = s.name,
                s.baseSubWidth,
                s.baseSubHeight,
                s.vaoIndex,
                s.columnCount,
                s.rowCount,
                vertexArrays,
                vertexArrayObjects
                );
        }

        public Sprite(
            Vertex_Array vertArray,
            string name = "",
            int offsetX = 0,
            int offsetY = 0
            )
        {
            Vertex_Array[] vertexArrays = new Vertex_Array[] { vertArray };

            int[] vertexArrayObjects = new int[] { vertArray.VertexBufferObject };

            Init(
                offsetX,
                offsetY,
                name,
                0,
                0,
                0,
                1,
                1,
                vertexArrays,
                vertexArrayObjects
                );

            BindVertexArrayData();
        }

        public Sprite(
            Texture_R2 texture, 
            int subWidth, 
            int subHeight, 
            string name="", 
            float offsetX = 0, 
            float offsetY = 0, 
            uint vaoIndex = 0,
            float r = 0,
            float g = 0,
            float b = 0,
            float a = 0)
        {
            int columnCount = texture.Width / subWidth, rowCount = texture.Height / subHeight;
            int count = rowCount * columnCount;

            Vertex_Array[] vertexArrays = new Vertex_Array[count];

            Vertex[] vertices;

            for (int y = 0; y < rowCount; y++)
            {
                for (int x = 0; x < columnCount; x++)
                {
                    vertices = Vertex_Array.VerticesFromDimensions(texture.Width, texture.Height, subWidth, subHeight, x, y, r, g, b, a);

                    vertexArrays[x + (y * columnCount)] = new Vertex_Array(texture, vertices);
                }
            }
            
            int[] vertexArrayObjects = new int[count];

            for (int i = 0; i < vertexArrayObjects.Length; i++)
                vertexArrayObjects[i] = GL.GenVertexArray();

            Init(
                offsetX,
                offsetY,
                name,
                subWidth,
                subHeight,
                (int)vaoIndex,
                columnCount,
                rowCount,
                vertexArrays,
                vertexArrayObjects
                );

            BindVertexArrayData();
        }

        private void Init(
            float offsetX, 
            float offsetY, 
            string name, 
            int baseSubWidth,
            int baseSubHeight,
            int vaoIndex,
            int columnCount,
            int rowCount,
            Vertex_Array[] vertexArrays,
            int[] vertexArrayObjects
            )
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.name = name;

            this.baseSubWidth = baseSubWidth;
            this.baseSubHeight = baseSubHeight;

            this.size = new Vector2(baseSubWidth, baseSubHeight);
            
            this.vaoIndex = vaoIndex;

            this.columnCount = columnCount;
            this.rowCount = rowCount;

            count = columnCount * rowCount;
            VertexArrays = vertexArrays;
            this.vertexArrayObjects = vertexArrayObjects;
        }
        
        private void OperateArrays(Func<Vertex, Vertex> operation)
        {
            for (int i = 0; i < VertexArrays.Length; i++)
            {
                for (int j = 0; j < VertexArrays[i].Vertices.Length; j++)
                {
                    VertexArrays[i].Vertices[j] = operation(VertexArrays[i].Vertices[j]);
                }
                VertexArrays[i].SetBufferData();
            }
        }

        private void SetScale(float scale)
        {
            OperateArrays((v) => 
            {
                v.Position /= this.scale;
                v.Position *= scale;
                return v;
            });
            this.scale = scale;
        }

        public void SetSize(Vector2 size)
        {
            OperateArrays((v) =>
            {
                Vector2 ret = new Vector2(0, 0);
                if (v.Position.X > 0)
                    ret.X = size.X;
                if (v.Position.Y > 0)
                    ret.Y = size.Y;
                v.Position = ret;
                return v;
            });
        }

        public void SetColor(Vector4 color)
        {
            OperateArrays((v) => 
            {
                v.ColorVector = color;
                return v;
            });
        }

        public void Use()
        {
            VertexArrays[VAO_Index].Use();
            GL.BindVertexArray(vertexArrayObjects[VAO_Index]);
        }

        private void BindVertexArrayData()
        {
            for (int i = 0; i < VertexArrays.Length; i++)
            {
                GL.BindVertexArray(vertexArrayObjects[i]);
                VertexArrays[i].BindVertexBuffer();
                VertexArrays[i].SetBufferData();

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
