using OpenTK;

namespace isometricgame.GameEngine.UI.Containers.Implemented
{
    /// <summary>
    /// A UI_Container that has sealed the base overrides for UI_Container.
    /// Containers that derive from UI_Strict_Panel have predictable sort behaviors.
    /// </summary>
    public class UI_Strict_Panel : UI_Container
    {
        public UI_Strict_Panel
            (
            UI_Rect boundingRect,
            
            UI_GameObject associatedGameObject = null
            )
        : base
            (
                boundingRect,
                
                associatedGameObject
            )
        {
        }

        public UI_Indexed_Element[] Get__Child_Elements__UI_Strict_Panel()
            => Get__CHILD_ELEMENTS__UI_Container();

        public bool Add__Element__UI_Strict_Panel(UI_Element element, UI_Anchor bindingAnchor = null)
            => Add__UI_Element__UI_Container(element, bindingAnchor);

        public bool Add__UI_GameObject__UI_Strict_Panel(UI_GameObject uiGameObject, UI_Anchor bindingAnchor = null)
            => Add__Element__UI_Strict_Panel(uiGameObject.UI_GameObject__UI_Element__Internal, bindingAnchor);

        protected sealed override void Handle_Scale__UI_Element()
        {
            base.Handle_Scale__UI_Element();
        }

        protected sealed override Vector3 Handle_Recover__Sort__UI_Container(UI_Indexed_Element indexedElement, Vector3 minorAnchorPosition)
        {
            return base.Handle_Recover__Sort__UI_Container(indexedElement, minorAnchorPosition);
        }

        protected sealed override Vector3 Handle_Get__Alignment_Offset__UI_Container(UI_Anchor_Sort_Type anchorSortType, UI_Rect rect_OfElement_ToSort,
            UI_Rect rect_OfOverlapping_ChildElement)
        {
            return base.Handle_Get__Alignment_Offset__UI_Container(anchorSortType, rect_OfElement_ToSort, rect_OfOverlapping_ChildElement);
        }

        protected sealed override bool Handle_Check_For__Sort_Integrity__UI_Container(UI_Indexed_Element indexedElementToSort, Vector3 sortedPosition)
        {
            return base.Handle_Check_For__Sort_Integrity__UI_Container(indexedElementToSort, sortedPosition);
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

        protected sealed override float? Handle_Scale__Determine_Child_Hypotenuse__UI_Container(UI_Indexed_Element indexedElement)
        {
            return base.Handle_Scale__Determine_Child_Hypotenuse__UI_Container(indexedElement);
        }

        protected sealed override UI_Anchor_Position_Type Handle_Clamp__Added_Element_Local_Origin__UI_Container(UI_Anchor_Position_Type localOrigin,
            UI_Anchor_Position_Type targetAnchor)
        {
            return base.Handle_Clamp__Added_Element_Local_Origin__UI_Container(localOrigin, targetAnchor);
        }

        protected sealed override Vector3 Handle_Get__Initial_Position_For_Element__UI_Container(UI_Indexed_Element indexedElement)
        {
            return base.Handle_Get__Initial_Position_For_Element__UI_Container(indexedElement);
        }

        protected sealed override Vector3 Handle_Get__Relative_Position_To_Anchor__UI_Container(UI_Indexed_Element indexedElement)
        {
            return base.Handle_Get__Relative_Position_To_Anchor__UI_Container(indexedElement);
        }

        protected sealed override Vector3 Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(UI_Indexed_Element indexedElement)
        {
            return base.Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(indexedElement);
        }
    }
}