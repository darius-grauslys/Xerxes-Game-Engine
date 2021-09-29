namespace Xerxes_Engine.Systems.Graphics.R2
{
    public class Sprite_Library : Game_System
    {
        internal Sprite_Dictionary Sprite_Library__SPRITE_DICTIONARY__Internal { get; }
        
        private Vertex_Object_Library _Sprite_Library__VERTEX_OBJECT_LIBRARY__REFERENCE { get; }

        public Sprite_Library(Game game, bool accessable = true) 
            : base(game, accessable)
        {
            Sprite_Library__SPRITE_DICTIONARY__Internal = new Sprite_Dictionary();

            _Sprite_Library__VERTEX_OBJECT_LIBRARY__REFERENCE
                = game.Game__Vertex_Object_Library;
        }

        public Sprite_Handle Define__Sprite__Sprite_Library
        (
            string alias = null,
            params Vertex_Object_Handle[] vertex_Object_Handles
        )
        {
            Vertex_Object[] vertex_Objects = 
                _Sprite_Library__VERTEX_OBJECT_LIBRARY__REFERENCE
                .Internal_Get__Vertex_Objects__Vertex_Object_Library
                (
                    vertex_Object_Handles
                );
            Sprite sprite = new Sprite(vertex_Objects);

            Sprite_Handle handle =
                Sprite_Library__SPRITE_DICTIONARY__Internal
                .Internal_Declare__Sprite__Sprite_Dictionary
                (
                    sprite,
                    alias
                );

            return handle;
        }

        internal Sprite Internal_Get__Sprite__Sprite_Library
        (
            Sprite_Handle handle
        )
            => Sprite_Library__SPRITE_DICTIONARY__Internal.Internal_Get__Sprite__Sprite_Dictionary(handle);
    }
}
