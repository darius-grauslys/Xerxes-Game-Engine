namespace Xerxes_Engine
{
    public sealed class SA__Declare_Vertex_Object :
        Streamline_Argument
    {
        internal Texture_R2 Declare_Vertex_Object__TEXTURE_R2__Internal { get; }
        internal float Declare_Vertex_Object__SPLICE_WIDTH__Internal    { get; }
        internal float Delcare_Vertex_Object__SPLICE_HEIGHT__Internal   { get; }
        internal int? Declare_Vertex_Object__COUNT_CONSTRAINT__Internal { get; }
        public Vertex_Object_Handle []
            Declare_Vertex_Object__Vertex_Object_Handles__Internal { get; internal set; }

        public SA__Declare_Vertex_Object
        (
            Texture_R2 texture_R2,
            float spliceWidth = -1,
            float spliceHeight = -1,
            int? nullabledCountConstraint = null
        )
        : base
        (Streamline_Argument.TIMELESS)
        {
            Declare_Vertex_Object__TEXTURE_R2__Internal =
                texture_R2;
            Declare_Vertex_Object__SPLICE_WIDTH__Internal =
                spliceWidth;
            Delcare_Vertex_Object__SPLICE_HEIGHT__Internal =
                spliceHeight;
            Declare_Vertex_Object__COUNT_CONSTRAINT__Internal =
                nullabledCountConstraint;
        }
    }
}
