using OpenTK;

namespace XerxesEngine.UI
{
    public class UI_Wrapper
    {
        public UI_Container UI_Wrapper__CONTAINER { get; }
        public UI_Element UI_Wrapper__WRAPPED_ELEMENT { get; }

        #region Wrapped-Element--Getters
        
        public UI_GameObject Get__Associated_UI_GameObject__UI_Element()
            => UI_Wrapper__WRAPPED_ELEMENT.UI_Element__Associated_UI_GameObject;

        public Vector3 Get__Local_Origin_Offset__Of_ELEMENT__UI_Indexed_Element()
            => UI_Wrapper__WRAPPED_ELEMENT.Get__Local_Origin_Offset__UI_Element();
        
        #endregion
        
        #region Wrapped-Element--Scaling
        private float UI_Indexed_Element__RATIO_OF_HYPOTENUSE_TO_PARENT { get; }
        
        internal void Internal_Scale__Element__UI_Indexed_Element(float? newHypotenuse = null)
            => UI_Wrapper__WRAPPED_ELEMENT.Internal_Scale__UI_Element
            (
                newHypotenuse 
                ??
                UI_Wrapper__CONTAINER.Get__Hypotenuse_Of_Rect__UI_Element()
                *
                UI_Indexed_Element__RATIO_OF_HYPOTENUSE_TO_PARENT
            );
        
        #endregion
        
        public UI_Wrapper
        (
            UI_Element wrappedElement,
            UI_Container container
        )
        {
            UI_Wrapper__CONTAINER = container;
            UI_Wrapper__WRAPPED_ELEMENT = wrappedElement;

            UI_Indexed_Element__RATIO_OF_HYPOTENUSE_TO_PARENT =
                UI_Wrapper__WRAPPED_ELEMENT.Get__Hypotenuse_Of_Rect__UI_Element()
                /
                UI_Wrapper__CONTAINER.Get__Hypotenuse_Of_Rect__UI_Element();
        }
    }
}