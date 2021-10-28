namespace Xerxes_Engine.Exports.Graphics.R2
{
    public sealed class Sprite_Library : 
        Xerxes_Export
    {
        internal Sprite_Dictionary Sprite_Library__SPRITE_DICTIONARY__Internal { get; }

        public Sprite_Library() 
        {
            Sprite_Library__SPRITE_DICTIONARY__Internal = 
                new Sprite_Dictionary();
        }

        protected override void Handle__Rooted__Xerxes_Export()
        {
            Protected_Declare__Catch__Xerxes_Export
                <SA__Declare_Sprite>
                (
                    Private_Declare__Sprite__Sprite_Library
                );
            Protected_Declare__Catch__Xerxes_Export
                <SA__Get_Sprite>
                (
                    Private_Get__Sprite__Sprite_Library
                );
        }

        public void Private_Declare__Sprite__Sprite_Library
        (
            SA__Declare_Sprite e
        )
        {
            Sprite sprite = new Sprite(e.Declare_Sprite__VERTEX_OBJECT_HANDLES__Internal);

            Sprite_Handle handle =
                Sprite_Library__SPRITE_DICTIONARY__Internal
                .Internal_Declare__Sprite__Sprite_Dictionary
                (
                    sprite,
                    e.Declare_Sprite__ALIAS__Internal
                );

            e.Declare_Sprite__Sprite_Handle =
                handle;
            e.Declare_Sprite__Sprite =
                sprite;
        }

        internal void Private_Get__Sprite__Sprite_Library
        (
            SA__Get_Sprite e
        )
        {
            Sprite s =
                Sprite_Library__SPRITE_DICTIONARY__Internal
                .Internal_Get__Sprite__Sprite_Dictionary(e.Get_Sprite__SPRITE_HANDLE__Internal);

            e.Get_Sprite__Sprite__Internal = s;
        }
    }
}
