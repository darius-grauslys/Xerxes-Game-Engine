namespace Xerxes_Engine
{
    public sealed class SA__Set_Sprite :
        Streamline_Argument
    {
        internal Sprite_Handle SA__Set_Sprite__Sprite_Handle { get; }

        public SA__Set_Sprite
        (
            Streamline_Argument e,
            Sprite_Handle sprite_Handle
        )
        : base (e)
        {
            SA__Set_Sprite__Sprite_Handle = 
                sprite_Handle;
        }
    }
}
