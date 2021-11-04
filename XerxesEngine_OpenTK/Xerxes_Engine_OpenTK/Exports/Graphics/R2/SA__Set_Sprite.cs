namespace Xerxes_Engine.Export_OpenTK
{
    public sealed class SA__Set_Sprite :
        SA__Update 
    {
        internal Sprite_Handle SA__Set_Sprite__Sprite_Handle { get; }

        public SA__Set_Sprite
        (
            SA__Sealed_Under_Game e,
            Sprite_Handle sprite_Handle
        )
            : this(SA__Update.TIMELESS, sprite_Handle)
        {}

        public SA__Set_Sprite
        (
            SA__Update e,
            Sprite_Handle sprite_Handle
        )
        : base(e)
        {
            SA__Set_Sprite__Sprite_Handle = 
                sprite_Handle;
        }
    }
}
