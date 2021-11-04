namespace Xerxes_Engine.Export_OpenTK
{
    public sealed class SA__Declare_Vertex_Object :
        Streamline_Argument
    {
        internal Texture_R2 Declare_Vertex_Object__TEXTURE_R2__Internal { get; }
        internal float Declare_Vertex_Object__SPLICE_WIDTH__Internal    { get; }
        internal float Delcare_Vertex_Object__SPLICE_HEIGHT__Internal   { get; }
        internal Integer_Vector_2[] Declare_Vertex_Object__BATCH_INDEX__Internal { get; }
        internal Integer_Vector_2[] Declare_Vertex_Object__BATCH_POSITIONS__Internal { get; }
        public Vertex_Object_Handle
            Declare_Vertex_Object__Vertex_Object_Handle__Internal { get; internal set; }

        public SA__Declare_Vertex_Object
        (
            Texture_R2 texture_R2,
            float spliceWidth = -1,
            float spliceHeight = -1,
            Integer_Vector_2[] batchIndex = null,
            Integer_Vector_2[] positions = null
        )
        {
            Declare_Vertex_Object__TEXTURE_R2__Internal =
                texture_R2;
            Declare_Vertex_Object__SPLICE_WIDTH__Internal =
                spliceWidth;
            Delcare_Vertex_Object__SPLICE_HEIGHT__Internal =
                spliceHeight;
            Declare_Vertex_Object__BATCH_INDEX__Internal =
                batchIndex;
            Declare_Vertex_Object__BATCH_POSITIONS__Internal =
                positions;
        }
    }
}
