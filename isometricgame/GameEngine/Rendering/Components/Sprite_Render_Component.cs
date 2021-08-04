using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Systems.Rendering;
using OpenTK;

namespace isometricgame.GameEngine.Components
{
    /// <summary>
    /// Gives public exposure to the attached GameObject's RenderUnit.
    /// </summary>
    public class Sprite_Render_Component : GameObject_Component
    {
        private SpriteLibrary SpriteLibrary => Component__Attached_GameObject?.GameObject__Scene_Layer?.Scene_Layer__Game?.Game__Sprite_Library;
        public Sprite Sprite => SpriteLibrary?.GetSprite(Component__Attached_GameObject.renderUnit.id);

        internal Sprite_Render_Component() 
            : base()
        {
        }

        public virtual void Set__Sprite__Sprite_Render(string name)
        {
            Vector3 pos = Component__Attached_GameObject.renderUnit.Position;
            Component__Attached_GameObject.renderUnit = SpriteLibrary.ExtractRenderUnit(name);
            Component__Attached_GameObject.renderUnit.Position = pos;
        }

        /// <summary>
        /// might need to make this thread safe.
        /// </summary>
        /// <param name="s"></param>
        public virtual void Set__Sprite__Sprite_Render(RenderUnit ru, bool copyPosition = false)
        {
            if (Component__Attached_GameObject != null)
            {
                if (copyPosition)
                    ru.Position = Component__Attached_GameObject.Position;
                Component__Attached_GameObject.renderUnit = ru;
            }
        }

        public virtual void Set__Sprite__Sprite_Render(int spriteId, int vao_Index = 0)
        {
            if (Component__Attached_GameObject != null)
            {
                Component__Attached_GameObject.renderUnit.id = spriteId;
                Component__Attached_GameObject.renderUnit.vaoIndex = vao_Index;
                Component__Attached_GameObject.renderUnit.IsInitialized = true;
            }
        }
    }
}
