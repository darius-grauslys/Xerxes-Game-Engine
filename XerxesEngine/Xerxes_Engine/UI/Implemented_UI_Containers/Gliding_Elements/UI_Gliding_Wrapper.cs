using OpenTK;

namespace Xerxes_Engine.UI.Implemented_UI_Containers.Gliding_Elements
{
    public class UI_Gliding_Wrapper : UI_Wrapper
    {
        internal void Internal_Set__Position__UI_Element_Glide_Wrapper(Vector3 position)
            => UI_Wrapper__WRAPPED_ELEMENT.Internal_Set__Position__UI_Element
            (
                position
            );
        
        public UI_Gliding_Wrapper
            (
            UI_Element element,
            UI_Glide_Container glideContainer
            )
            : base (element, glideContainer)
        {
        }
    }
}
