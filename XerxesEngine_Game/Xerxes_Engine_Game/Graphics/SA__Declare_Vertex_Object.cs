
using System.Linq;

namespace Xerxes.Game_Engine.Graphics
{
    public class SA__Declare_Vertex_Object :
        Streamline_Argument
    {
        public ITexture_2D Declare_Vertex_Object__Texture { get; set; }

        private Batch_Index[] declare_vertex_object__batch_indices;
        public Batch_Index[] Declare_Vertex_Object__Batch_Indices 
        { 
            get => declare_vertex_object__batch_indices;
            set => declare_vertex_object__batch_indices = value.ToArray(); 
        }

        public IVertex_Object Declare_Vertex_Object__Vertex_Object__Returned { get; set; }

        public SA__Declare_Vertex_Object()
        {

        }
    }
}
