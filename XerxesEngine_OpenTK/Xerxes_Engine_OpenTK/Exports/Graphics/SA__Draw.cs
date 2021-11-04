using OpenTK;

namespace Xerxes_Engine.Export_OpenTK
{
    public class SA__Draw :
        SA__Chronical
    {
        internal Matrix4 Draw__Projection_Matrix__Internal { get; set; }

        public Vertex_Object_Handle Draw__VERTEX_OBJECT_HANDLE__Internal { get; set; }

        public Vector3 Draw__Position__Internal { get; set; }
        public Vector3 Draw__Scale__Internal { get; set; }

        internal SA__Draw(SA__Render e)
            : base(e)
        {}

        public SA__Draw(SA__Draw e)
            : base(e)
        {
        }
    }
}
