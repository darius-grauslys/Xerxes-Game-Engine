using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Rendering
{
    public class Batcher
    {
        private List<Vertex> vertices = new List<Vertex>();
        
        public void AddVertexShape(VertexArray vertArray, Vector2 offset)
        {
            for(int i=0;i<vertArray.Vertices.Length; i++)
            {
                Vertex vert = new Vertex(
                    vertArray.Vertices[i].Position + offset,
                    vertArray.Vertices[i].TextCoord,
                    new Vector4(0,0,0,1));
                vertices.Add(vert);
            }
        }

        public VertexArray CreateVertexArray() => new VertexArray(vertices.ToArray());

        public static void ModifyArray(VertexArray batchedArray, VertexArray subArray, Vector2 offset, int index, int stride=-1)
        {
            stride = (stride > 0) ? stride : VertexArray.VERTEXARRAY_INDEX_COUNT;
            for (int i = 0; i < subArray.Vertices.Length; i++)
            {
                Vertex vert = new Vertex(
                    subArray.Vertices[i].Position + offset, 
                    subArray.Vertices[i].TextCoord,
                    new Vector4(0, 0, 0, 1));
                batchedArray.Vertices[(i + index) * stride] = vert;
            }
            batchedArray.BindVertexBuffer();
        }
    }
}
