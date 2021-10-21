namespace Xerxes_Engine.Engine_Objects.R2
{
    /// <summary>
    /// Gives public exposure to the attached Game_Object's RenderUnit.
    /// </summary>
    public class Sprite_Render_Component : Game_Object_Component 
    {
        private Sprite_Dictionary _Sprite_Render_Component__Sprite_Library_Dictionary__REFERENCE { get; set; }

        public Sprite_Handle Sprite_Render_Component__Active_Sprite { get; private set; }
        protected Sprite Sprite_Render_Component__Sprite__Protected { get; private set; }

        public Sprite_Render_Component() 
            : base()
        {
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <SA__Associate_Game>
                (
                    Private_Handle__Associate_To_Game
                );
            Protected_Declare__Downstream_Catch__Xerxes_Engine_Object
                <SA__Draw>
                (
                    Private_Handle__Draw__Sprite_Render_Component
                );
        }

        private void Private_Handle__Associate_To_Game(SA__Associate_Game e)
        {
            //TODO: Figure out a way for good rooting.
            Xerxes_Engine_Object__Root__Internal = e.SA__Associate_Game__GAME;
            Game game = Protected_Get__Root__Xerxes_Engine_Object();

            _Sprite_Render_Component__Sprite_Library_Dictionary__REFERENCE =
                game.Game__Sprite_Library.Sprite_Library__SPRITE_DICTIONARY__Internal;

            if (Sprite_Render_Component__Active_Sprite != null)
                Private_Set__Sprite__Sprite_Render_Component();
        }

        private void Private_Handle__Draw__Sprite_Render_Component(SA__Draw e)
        {
            Vertex_Object_Handle vertex_Object_Handle =
                Sprite_Render_Component__Sprite__Protected
                .Sprite__Active_Object__Internal
                .Vertex_Object__HANDLE;
            e.SA__Draw__VERTEX_OBJECT_HANDLE__Internal
                = vertex_Object_Handle;
        }

        public void Set__Sprite__Sprite_Render_Component(Sprite_Handle sprite_Handle)
        {
            Sprite_Render_Component__Active_Sprite = sprite_Handle;

            if (Protected_Check_If__Rooted__Xerxes_Engine_Object())
                Private_Set__Sprite__Sprite_Render_Component();
        }

        private void Private_Set__Sprite__Sprite_Render_Component()
        {
            Sprite_Render_Component__Sprite__Protected =
                _Sprite_Render_Component__Sprite_Library_Dictionary__REFERENCE
                .Internal_Get__Sprite__Sprite_Dictionary(Sprite_Render_Component__Active_Sprite);
        }
    }
}
