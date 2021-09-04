namespace Xerxes_Engine.UI
{
    /// <summary>
    /// Containers that inherit from this will always have a public UI_Element and UI_Game_Object
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

        public bool Add__UI_Game_Object__UI_Inclusive_Container
        (
            UI_Game_Object uiGame_Object,
            UI_Anchor bindingAnchor = null
        )
        {
            return Add__UI_Element__UI_Inclusive_Container
            (
                uiGame_Object.Get__UI_Element__UI_Game_Object(),
                bindingAnchor
            );
        }
    }
}
