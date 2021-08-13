using OpenTK;

namespace isometricgame.GameEngine.UI.Containers.Implemented_UI_Containers.Gliding_Elements
{
    public class UI_Gliding_Wrapper : UI_Wrapper
    {
        internal UI_Glide_Node UI_Element_Glide_Wrapper__Anchored_Node { get; set; }

        internal Vector3 UI_Element_Glide_Wrapper__Position_From_Node { get; set; }

        internal void Internal_Set__Position__UI_Element_Glide_Wrapper()
            => UI_Wrapper__WRAPPED_ELEMENT.Internal_Set__Position__UI_Element
            (
                UI_Element_Glide_Wrapper__Anchored_Node.Get__Position_In_UISpace__UI_Element()
                + 
                UI_Element_Glide_Wrapper__Position_From_Node
            );
        
        public UI_Gliding_Wrapper
            (
            UI_Element element,
            UI_Glide_Panel glidePanel
            )
            : base (element, glidePanel)
        {
        }
    }
}