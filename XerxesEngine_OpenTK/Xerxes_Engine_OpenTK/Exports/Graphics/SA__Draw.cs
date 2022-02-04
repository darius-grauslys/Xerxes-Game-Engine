using OpenTK;

namespace Xerxes.Xerxes_OpenTK
{
    public class SA__Draw :
        SA__Chronical
    {
        public Vertex_Object Draw__Vertex_Object { get; set; }

        public Vector3 Draw__Position { get; set; }
        public Vector3 Draw__Scale { get; set; }

        public SA__Draw(SA__Render e)
            : base(e)
        {}
    }
}
