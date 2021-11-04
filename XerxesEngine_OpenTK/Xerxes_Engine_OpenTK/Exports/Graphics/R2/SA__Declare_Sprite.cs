namespace Xerxes_Engine.Export_OpenTK
{
    public sealed class SA__Declare_Sprite :
        Streamline_Argument
    {
        internal string Declare_Sprite__ALIAS__Internal { get; }
        internal Vertex_Object_Handle[] Declare_Sprite__VERTEX_OBJECT_HANDLES__Internal { get; }
        public Sprite_Handle Declare_Sprite__Sprite_Handle { get; internal set; }
        public Sprite Declare_Sprite__Sprite { get; internal set; }

        public SA__Declare_Sprite
        (
            params Vertex_Object_Handle[] vertex_Object_Handles
        )
        {
            Declare_Sprite__VERTEX_OBJECT_HANDLES__Internal =
                vertex_Object_Handles;
        }
    }
}
