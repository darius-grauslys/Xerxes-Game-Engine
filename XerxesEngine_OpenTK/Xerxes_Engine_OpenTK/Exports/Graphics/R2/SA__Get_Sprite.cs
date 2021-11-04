namespace Xerxes_Engine.Export_OpenTK
{
    internal sealed class SA__Get_Sprite :
        Streamline_Argument
    {
        internal Sprite_Handle Get_Sprite__SPRITE_HANDLE__Internal { get; }
        internal string Get_Sprite__ALIAS__Internal { get; }

        internal Sprite SA__Get_Sprite__Sprite__Internal { get; set; }

        public SA__Get_Sprite
        (
            SA__Sealed_Under_Game e, 
            Sprite_Handle handle = null, 
            string alias = null
        )
        {
            Get_Sprite__SPRITE_HANDLE__Internal = handle;
            Get_Sprite__ALIAS__Internal = alias;
        }

        public SA__Get_Sprite
        (
            SA__Update e, 
            Sprite_Handle handle = null, 
            string alias = null
        )
        {
            Get_Sprite__SPRITE_HANDLE__Internal = handle;
            Get_Sprite__ALIAS__Internal = alias;
        }
    }
}
