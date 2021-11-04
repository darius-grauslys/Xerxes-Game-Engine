namespace Xerxes_Engine.Export_OpenTK.Engine_Objects.R2
{
    /// <summary>
    /// Gives public exposure to the attached Game_Object's RenderUnit.
    /// </summary>
    public class Sprite_Render_Component : Game_Object_Component 
    {
        public Sprite_Handle Sprite_Render_Component__Active_Sprite { get; private set; }
        protected Sprite Sprite_Render_Component__Sprite__Protected { get; private set; }

        public Sprite_Render_Component() 
            : base()
        {
            Declare__Streams()
                .Upstream  .Extending<SA__Get_Sprite>()
                .Downstream.Receiving<SA__Draw>
                (
                    Private_Draw__Sprite_Render_Component
                )
                .Downstream.Receiving<SA__Set_Sprite>
                (
                    Private_Set__Sprite__Sprite_Render_Component
                )
                ;
        }

        private void Private_Draw__Sprite_Render_Component
        (SA__Draw e)
        {
            Vertex_Object_Handle vertex_Object_Handle =
                Sprite_Render_Component__Sprite__Protected
                .Sprite__Active_Object__Internal;
            e.Draw__VERTEX_OBJECT_HANDLE__Internal
                = vertex_Object_Handle;
        }

        private void Private_Set__Sprite__Sprite_Render_Component
        (SA__Set_Sprite e)
        {
            Sprite_Render_Component__Active_Sprite = 
                e.SA__Set_Sprite__Sprite_Handle;

            if (!Xerxes_Object_Base__Is_Rooted__Protected)
                return;

            SA__Get_Sprite e1 = new
                SA__Get_Sprite(e, Sprite_Render_Component__Active_Sprite);

            Invoke__Ascending
                (e1);

            Sprite_Render_Component__Sprite__Protected =
                e1.SA__Get_Sprite__Sprite__Internal;
        }
    }
}
