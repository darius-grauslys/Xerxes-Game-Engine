using OpenTK;

namespace Xerxes_Engine
{
    public class SA__Draw : Streamline_Argument
    {
        public Vertex_Object_Handle SA__Draw__VERTEX_OBJECT_HANDLE__Internal { get; set; }

        public Vector3 SA__Draw__Position__Internal { get; set; }
        public Vector3 SA__Draw__Scale__Internal { get; set; }

        internal Matrix4 SA__Draw__World_Matrix__Internal { get; set; }

        public SA__Draw(SA__Render e)
            : base(e)
        {
            SA__Draw__World_Matrix__Internal = 
                e.SA__Render__World_Matrix__Internal;
        }
    }
}
