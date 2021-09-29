using OpenTK;

namespace Xerxes_Engine
{
    internal class Streamline_Argument_Draw : Streamline_Argument
    {
        internal Vertex_Object Streamline_Argument_Draw__VERTEX_OBJECT__Internal { get; }
        internal Vector3 Streamline_Argument_Draw__Position__Internal { get; set ;}

        internal Matrix4 Streamline_Argument_Draw__World_Matrix__Internal { get; set; }

        internal Streamline_Argument_Draw(Streamline_Argument_Render e, Vertex_Object toDraw)
            : base(e)
        {
            Streamline_Argument_Draw__VERTEX_OBJECT__Internal = toDraw;
        }
    }
}
