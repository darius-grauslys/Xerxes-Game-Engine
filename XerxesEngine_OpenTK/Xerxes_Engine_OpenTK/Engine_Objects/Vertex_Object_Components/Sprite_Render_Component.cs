namespace Xerxes_Engine.Export_OpenTK.Engine_Objects.Vertex_Object_Components
{
    /// <summary>
    /// A Vertex_Object_Component with SA__Field[int] to set the current vertex object.
    /// </summary>
    public class Sprite_Render_Component : 
        Vertex_Object_Component
    {
        protected Sprite sprite_render_component__sprite;

        public Sprite_Render_Component()
        {
            Declare__Field<Sprite>
            (
                () => sprite_render_component__sprite,
                (sprite) => sprite_render_component__sprite = sprite
            );

            Declare__Field<Sprite.Sprite_Index>
            (
                () => sprite_render_component__sprite.Sprite__Active_Index,
                (sprite_index) => sprite_render_component__sprite.Sprite__Active_Index = sprite_index
            );
        }
    }
}
