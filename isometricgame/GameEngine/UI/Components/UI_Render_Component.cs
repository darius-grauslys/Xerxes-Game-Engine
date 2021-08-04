using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Systems.Rendering;
using OpenTK;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI.Components
{
    /// <summary>
    /// Gives controlled exposure to the attached GameObject's RenderUnit. Control is based on
    /// the associated UI_Panel containing this object.
    /// </summary>
    public class UI_Render_Component : GameObject_Component
    {
        private SpriteLibrary UI_Render__Sprite_Library { get; set; }
        
        private string _ui_render__Sprite_Alias;
        public string UI_Render__Sprite_Alias { get => _ui_render__Sprite_Alias; set => Private_Set__Sprite_Alias__UI_Render(value);}

        internal Sprite Internal_Get__Sprite__UI_Render()
            => UI_Render__Sprite_Library.GetSprite(_ui_render__Sprite_Alias);
        
        public Vector3 UI_Render__Position
            => UI_Render__ELEMENT.UI_Element__Position;
        
        private void Private_Set__Sprite_Alias__UI_Render(string spriteAlias)
        {
            _ui_render__Sprite_Alias = spriteAlias;
            Private_Bind__Sprite__UI_Render();
        }

        public readonly UI_Element UI_Render__ELEMENT;

        internal UI_Render_Component
            (
            string spriteAlias = null,
            
            UI_Rect boundingRect = null,
            
            UI_Anchor majorAnchor = null,
            UI_Anchor lesserAnchor = null
            )
        {
            UI_Render__Sprite_Alias = spriteAlias;
            UI_Render__ELEMENT = new UI_Element
            (
                boundingRect ?? new UI_Rect(),
                
                majorAnchor ?? new UI_Anchor(UI_Anchor_Sort_Type.Left, 0, UI_Anchor_Padding_Type.Constrained__Pixel),
                lesserAnchor ?? new UI_Anchor(UI_Anchor_Sort_Type.Top, 0, UI_Anchor_Padding_Type.Constrained__Pixel)
            );

            UI_Render__ELEMENT.Event__Repositioned__UI_Element += Event_Handle__UI_Element__Repositioned;
            UI_Render__ELEMENT.Event__Scaled__UI_Element += Event_Handle__UI_Element__Rescaled;
        }

        private void Event_Handle__UI_Element__Repositioned(UI_Element element)
        {
            Component__Attached_GameObject.Position = element.UI_Element__Position;
        }

        private void Event_Handle__UI_Element__Rescaled(UI_Element element)
        {
            Internal_Get__Sprite__UI_Render().SetSize(element.UI_Element__Size);
        }

        protected override void Handle_Attach_To__GameObject__Component()
        {
            Private_Bind__Sprite__UI_Render();
            
            UI_Render__ELEMENT.Internal_Set__Associated_UI_GameObject__UI_Element
            (
                Component__Attached_GameObject as UI_GameObject
            );
        }

        private void Private_Bind__Sprite__UI_Render()
        {
            if (Component__Attached_GameObject == null)
                return;

            UI_Render__Sprite_Library = Component__Attached_GameObject.GameObject__Scene_Layer.Scene_Layer__Game
                .Game__Sprite_Library;

            Sprite sprite = Internal_Get__Sprite__UI_Render();

            Component__Attached_GameObject.renderUnit = UI_Render__Sprite_Library.ExtractRenderUnit(UI_Render__Sprite_Alias); 
            
            if (MathHelper.IsGreaterArea(sprite.Size, UI_Render__ELEMENT.UI_Element__Size))
            {
                float lowestScale;
                float scaleWidth = UI_Render__ELEMENT.UI_Element__Width / sprite.SubWidth;
                float scaleHeight = UI_Render__ELEMENT.UI_Element__Height / sprite.SubHeight;

                lowestScale = (scaleWidth < scaleHeight) ? scaleWidth : scaleHeight;
                
                sprite.SetSize(UI_Render__ELEMENT.UI_Element__Size);
            }
        }
    }
}