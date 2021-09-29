namespace Xerxes_Engine.Engine_Objects.R2
{
    /// <summary>
    /// Gives public exposure to the attached Game_Object's RenderUnit.
    /// </summary>
    public class Sprite_Render_Component : Xerxes_Descendant<Game_Object,Sprite_Render_Component> 
    {
        private Sprite_Dictionary _Sprite_Render_Component__Sprite_Library_Dictionary__REFERENCE { get; set; }

        public Sprite_Handle Sprite_Render_Component__Active_Sprite { get; private set; }
        protected Sprite Sprite_Render_Component__Sprite__Protected { get; set; }

        public Sprite_Render_Component() 
            : base()
        {
            Protected_Subscribe__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Associate_Game>
                (
                    Private_Handle__Associate_To_Game
                );
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Render>
                (
                    Private_Handle__Render__Sprite_Render_Component
                );
            Protected_Declare__Ascending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Draw>
                ();
        }

        private void Private_Handle__Associate_To_Game(Streamline_Argument_Associate_Game e)
        {
            Game game = Protected_Get__Root__Xerxes_Engine_Object();

            _Sprite_Render_Component__Sprite_Library_Dictionary__REFERENCE =
                game.Game__Sprite_Library.Sprite_Library__SPRITE_DICTIONARY__Internal;

            if (Sprite_Render_Component__Active_Sprite != null)
                Private_Set__Sprite__Sprite_Render_Component();
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

        private void Private_Handle__Render__Sprite_Render_Component(Streamline_Argument_Render e)
        {
            Streamline_Argument_Draw response =
                new Streamline_Argument_Draw
                (
                    e,
                    Sprite_Render_Component__Sprite__Protected
                    .Sprite__Active_Object__Internal
                );

            Protected_Invoke__Ascending_Streamline__Xerxes_Engine_Object(response);
        }
    }
}
