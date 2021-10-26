namespace Xerxes_Engine.Engine_Objects.R2
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
            Protected_Declare__Upstream_Extender__Xerxes_Engine_Object
                <SA__Get_Sprite>
                ();

            Protected_Declare__Downstream_Receiver__Xerxes_Engine_Object
                <SA__Draw>
                (
                    Private_Handle__Draw__Sprite_Render_Component
                );
        }

        private void Private_Handle__Draw__Sprite_Render_Component(SA__Draw e)
        {
            Vertex_Object_Handle vertex_Object_Handle =
                Sprite_Render_Component__Sprite__Protected
                .Sprite__Active_Object__Internal;
            e.Draw__VERTEX_OBJECT_HANDLE__Internal
                = vertex_Object_Handle;
        }

        public void Set__Sprite__Sprite_Render_Component(Sprite_Handle sprite_Handle)
        {
            Sprite_Render_Component__Active_Sprite = sprite_Handle;

            //TODO: resolve sealing.
            Private_Set__Sprite__Sprite_Render_Component();
        }

        private void Private_Set__Sprite__Sprite_Render_Component()
        {
            SA__Get_Sprite e = new
                SA__Get_Sprite(Sprite_Render_Component__Active_Sprite);

            Protected_Invoke__Ascending_Extender__Xerxes_Engine_Object
                (e);

            Sprite_Render_Component__Sprite__Protected =
                e.Get_Sprite__Sprite__Internal;
        }
    }
}
