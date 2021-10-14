using OpenTK;

namespace Xerxes_Engine
{
    public class Streamline_Argument_Draw : Streamline_Argument
    {
        public Vertex_Object_Handle Streamline_Argument_Draw__VERTEX_OBJECT_HANDLE__Internal { get; set; }

        public Vector3 Streamline_Argument_Draw__Position__Internal { get; set; }
        public Vector3 Streamline_Argument_Draw__Scale__Internal { get; set; }

        internal Matrix4 Streamline_Argument_Draw__World_Matrix__Internal { get; set; }

        internal Streamline_Argument_Draw(Streamline_Argument_Render e)
            : base(e)
        {
        }
    }
}
