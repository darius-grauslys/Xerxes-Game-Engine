using Xerxes_Engine.Systems.Graphics;
using OpenTK;
using Math_Helper = Xerxes_Engine.Tools.Math_Helper;
using Xerxes_Engine.Systems.Graphics.R2;

namespace Xerxes_Engine.UI.Implemented_UI_Components
{
    /// <summary>
    /// Gives controlled exposure to the attached Game_Object's RenderUnit. Control is based on
    /// the associated UI_Panel containing this object.
    /// </summary>
    public class UI_Render_Component : Game_Object_Component
    {
        private Sprite_Library UI_Render__Sprite_Library { get; set; }
        
        private string _ui_render__Sprite_Alias;
        public string UI_Render__Sprite_Alias { get => _ui_render__Sprite_Alias; set => Private_Set__Sprite_Alias__UI_Render(value);}

        internal Sprite Internal_Get__Sprite__UI_Render()
            => (_ui_render__Sprite_Alias != null) ? UI_Render__Sprite_Library.Get__Sprite_From_Name__Sprite_Library(_ui_render__Sprite_Alias) : null;
        
        public Vector3 UI_Render__Position
            => UI_Render__Element.Get__Position_In_UISpace__UI_Element();
        
        private void Private_Set__Sprite_Alias__UI_Render(string spriteAlias)
        {
            _ui_render__Sprite_Alias = spriteAlias;
            Private_Bind__Game_Object__UI_Render();
        }

        public UI_Element UI_Render__Element { get; private set; }

        internal UI_Render_Component
            (
            string spriteAlias = null,
            
            UI_Element uiElement = null
            )
        {
            UI_Render__Sprite_Alias = spriteAlias;
            UI_Render__Element = uiElement;
        }

        private void Event_Handle__UI_Element__Repositioned(UI_Element element)
        {
            UI_Game_Object ui_Game_Object = Protected_Get__Attached_Object__Game_Object_Component() as UI_Game_Object;
            
            ui_Game_Object.Game_Object__Render_Unit_Position__Internal = 
                    element.Get__Position_In_GameSpace__UI_Element();
        }

        private void Event_Handle__UI_Element__Rescaled(UI_Element element)
        {
            Internal_Get__Sprite__UI_Render()?.SetSize(element.Get__Size__UI_Element());
        }

        protected override void Handle_Attach_To__Game_Object__Component()
        {
            UI_Render__Sprite_Library = Protected_Get__Attached_Object__Game_Object_Component().Game_Object__Scene_Layer
                .Scene_Layer__Game
                .Game__Sprite_Library;
            
            Private_Bind__Game_Object__UI_Render();
            
            UI_Render__Element.Internal_Set__Associated_UI_Game_Object__UI_Element
            (
                Protected_Get__Attached_Object__Game_Object_Component() as UI_Game_Object
            );
        }

        private void Private_Bind__Game_Object__UI_Render()
        {
            if (Protected_Get__Attached_Object__Game_Object_Component() == null || UI_Render__Element == null)
                return;

            UI_Render__Element.Event__Repositioned__UI_Element += Event_Handle__UI_Element__Repositioned;
            UI_Render__Element.Event__Scaled__UI_Element += Event_Handle__UI_Element__Rescaled;

            if (UI_Render__Sprite_Alias == null)
                return;
            
            Sprite sprite = Internal_Get__Sprite__UI_Render();

            UI_Render__Sprite_Library
                .Extract__Render_Unit__Sprite_Library
            (
                UI_Render__Sprite_Alias, 
                out Protected_Get__Attached_Object__Game_Object_Component()._game_Object__Render_Unit
            );

            if (UI_Render__Element == null)
                UI_Render__Element = new UI_Element(sprite.Size);
            
            if (Math_Helper.CheckIf__Greater_Area(sprite.Size, UI_Render__Element.Get__Size__UI_Element()))
            {
                float lowestScale;
                float scaleWidth = UI_Render__Element.Get__Width__UI_Element() / sprite.SubWidth;
                float scaleHeight = UI_Render__Element.Get__Height__UI_Element() / sprite.SubHeight;

                lowestScale = (scaleWidth < scaleHeight) ? scaleWidth : scaleHeight;
                
                sprite.SetSize(UI_Render__Element.Get__Size__UI_Element());
            }
        }
    }
}
