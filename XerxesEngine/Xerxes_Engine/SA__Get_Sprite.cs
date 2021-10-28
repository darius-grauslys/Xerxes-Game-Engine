namespace Xerxes_Engine
{
    internal sealed class SA__Get_Sprite :
        Streamline_Argument
    {
        internal Sprite_Handle Get_Sprite__SPRITE_HANDLE__Internal { get; }
        internal string Get_Sprite__ALIAS__Internal { get; }

        internal Sprite Get_Sprite__Sprite__Internal { get; set; }

        internal SA__Get_Sprite
        (
            Sprite_Handle handle
        )
        : base
        (Streamline_Argument.TIMELESS)
        {
            Get_Sprite__SPRITE_HANDLE__Internal = handle;
        }

        internal SA__Get_Sprite
        (
            string alias
        )
        : base
        (Streamline_Argument.TIMELESS)
        {
            Get_Sprite__ALIAS__Internal = alias;
        }
    }
}
