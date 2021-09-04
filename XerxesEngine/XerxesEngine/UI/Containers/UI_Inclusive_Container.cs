namespace XerxesEngine.UI
{
    /// <summary>
    /// Containers that inherit from this will always have a public UI_Element and UI_GameObject
    /// insertion method.
    /// </summary>
    public class UI_Inclusive_Container : UI_Container
    {
        public UI_Inclusive_Container
        (
            UI_Rect boundingRect
        ) 
            : base(boundingRect)
        {
        }

        public bool Add__UI_Element__UI_Inclusive_Container
        (
            UI_Element element,
            UI_Anchor bindingAnchor = null
        )
        {
            return Add__UI_Element__UI_Container
            (
                element,
                bindingAnchor
            );
        }

        public bool Add__UI_GameObject__UI_Inclusive_Container
        (
            UI_GameObject uiGameObject,
            UI_Anchor bindingAnchor = null
        )
        {
            return Add__UI_Element__UI_Inclusive_Container
            (
                uiGameObject.Get__UI_Element__UI_GameObject(),
                bindingAnchor
            );
        }
    }
}
