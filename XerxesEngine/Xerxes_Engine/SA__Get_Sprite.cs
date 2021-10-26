namespace Xerxes_Engine
{
    internal sealed class SA__Get_Sprite :
        Streamline_Argument
    {
        internal Sprite_Handle Get_Sprite__SPRITE_HANDLE__Internal { get; }
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
    }
}
