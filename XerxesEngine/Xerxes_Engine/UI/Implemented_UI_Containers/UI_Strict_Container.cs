using OpenTK;

namespace Xerxes_Engine.UI.Implemented_UI_Containers
{
    /// <summary>
    /// A UI_Container that has sealed the base overrides for UI_Container.
    /// Containers that derive from UI_Strict_Panel have predictable sort behaviors.
    /// </summary>
    public class UI_Strict_Container : UI_Inclusive_Container
    {
        public UI_Strict_Container
            (
            UI_Rect boundingRect
            )
        : base
            (
                boundingRect
            )
        {
        }

        public UI_Anchored_Wrapper[] Get__Child_Elements__UI_Strict_Panel()
            => Get__CHILD_ELEMENTS__UI_Container();

        public bool Add__Element__UI_Strict_Panel(UI_Element element, UI_Anchor bindingAnchor = null)
            => Add__UI_Element__UI_Container(element, bindingAnchor);

        public bool Add__UI_Game_Object__UI_Strict_Panel(UI_Game_Object uiGame_Object, UI_Anchor bindingAnchor = null)
            => Add__Element__UI_Strict_Panel(uiGame_Object.Get__UI_Element__UI_Game_Object(), bindingAnchor);

        protected sealed override void Handle_Scale__UI_Element()
        {
            base.Handle_Scale__UI_Element();
        }

        protected sealed override Vector3 Handle_Recover__Sort__UI_Container(UI_Anchored_Wrapper anchoredWrapper, Vector3 minorAnchorPosition)
        {
            return base.Handle_Recover__Sort__UI_Container(anchoredWrapper, minorAnchorPosition);
        }

        protected sealed override Vector3 Handle_Get__Alignment_Offset__UI_Container(UI_Anchor_Sort_Type anchorSortType, UI_Rect rect_OfElement_ToSort,
            UI_Rect rect_OfOverlapping_ChildElement, Vector3 targetPosition)
        {
            return base.Handle_Get__Alignment_Offset__UI_Container(anchorSortType, rect_OfElement_ToSort, rect_OfOverlapping_ChildElement, targetPosition);
        }

        protected sealed override bool Handle_Check_For__Sort_Integrity__UI_Container(UI_Anchored_Wrapper anchoredWrapperToSort, Vector3 sortedPosition)
        {
            return base.Handle_Check_For__Sort_Integrity__UI_Container(anchoredWrapperToSort, sortedPosition);
        }

        protected sealed override UI_Anchor_Sort_Type Handle_Clamp__Horizontal_Sort_Type__UI_Container(UI_Anchor_Position_Type elementPositionType,
            UI_Anchor_Sort_Type horizontalSortType)
        {
            return base.Handle_Clamp__Horizontal_Sort_Type__UI_Container(elementPositionType, horizontalSortType);
        }

        protected sealed override UI_Anchor_Sort_Type Handle_Clamp__Vertical_Sort_Type__UI_Container(UI_Anchor_Position_Type elementPositionType,
            UI_Anchor_Sort_Type verticalSortType)
        {
            return base.Handle_Clamp__Vertical_Sort_Type__UI_Container(elementPositionType, verticalSortType);
        }

        protected sealed override float? Handle_Scale__Determine_Child_Hypotenuse__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            return base.Handle_Scale__Determine_Child_Hypotenuse__UI_Container(anchoredWrapper);
        }

        protected sealed override UI_Anchor_Position_Type Handle_Clamp__Added_Element_Local_Origin__UI_Container(UI_Anchor_Position_Type localOrigin,
            UI_Anchor_Position_Type targetAnchor)
        {
            return base.Handle_Clamp__Added_Element_Local_Origin__UI_Container(localOrigin, targetAnchor);
        }

        protected sealed override Vector3 Handle_Get__Initial_Position_For_Element__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            return base.Handle_Get__Initial_Position_For_Element__UI_Container(anchoredWrapper);
        }

        protected sealed override Vector3 Handle_Get__Relative_Position_To_Anchor__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            return base.Handle_Get__Relative_Position_To_Anchor__UI_Container(anchoredWrapper);
        }

        protected sealed override Vector3 Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            return base.Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(anchoredWrapper);
        }
    }
}
