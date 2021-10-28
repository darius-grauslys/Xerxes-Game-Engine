using OpenTK;

namespace Xerxes_Engine
{
    public class SA__Draw : Streamline_Argument
    {
        internal Matrix4 Draw__Projection_Matrix__Internal { get; set; }

        public Vertex_Object_Handle Draw__VERTEX_OBJECT_HANDLE__Internal { get; set; }

        public Vector3 Draw__Position__Internal { get; set; }
        public Vector3 Draw__Scale__Internal { get; set; }

        public SA__Draw(Streamline_Argument e)
            : base(e)
        {
        }
    }
}
