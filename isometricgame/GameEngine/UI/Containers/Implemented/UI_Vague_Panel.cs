﻿using OpenTK;

namespace isometricgame.GameEngine.UI
{
    /// <summary>
    /// A container that has open overrides on UI_Container that allow
    /// child elements to break it's UI_Rect bounds, and to have
    /// unclamped anchor sorting styles. Use these containers to do
    /// whatever you want - but at the cost of predictability.
    /// </summary>
    public class UI_Vague_Panel : UI_Container
    {
        public UI_Vague_Panel(UI_Rect boundingRect, UI_GameObject associatedGameObject = null) 
            : base
                (
                boundingRect, 
                associatedGameObject
                )
        {
        }

        protected override bool Handle_Check_For__Sort_Integrity__UI_Container
        (
            UI_Indexed_Element indexedElementToSort, 
            Vector3 sortedPosition
        )
        {
            return true;
        }

        protected override UI_Anchor_Position_Type Handle_Clamp__Added_Element_Local_Origin__UI_Container
        (
            UI_Anchor_Position_Type localOrigin,
            UI_Anchor_Position_Type targetAnchor
        )
        {
            return localOrigin;
        }

        protected override UI_Anchor_Sort_Type Handle_Clamp__Horizontal_Sort_Type__UI_Container
        (
            UI_Anchor_Position_Type elementPositionType,
            UI_Anchor_Sort_Type horizontalSortType
        )
        {
            return horizontalSortType;
        }

        protected override UI_Anchor_Sort_Type Handle_Clamp__Vertical_Sort_Type__UI_Container
        (
            UI_Anchor_Position_Type elementPositionType,
            UI_Anchor_Sort_Type verticalSortType
        )
        {
            return verticalSortType;
        }
    }
}