using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using OpenTK;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI
{
    /// <summary>
    /// The abstract foundation for Container UI_Elements. Offers base functionality for scaling and repositioning.
    /// </summary>
    public abstract class UI_Container : UI_Element
    {
        private readonly List<UI_Indexed_Element> _UI_Container__CHILD_ELEMENTS = new List<UI_Indexed_Element>();
        public int UI_Container__UI_Element_Count => _UI_Container__CHILD_ELEMENTS.Count;

        protected UI_Indexed_Element Get__Indexed_Element__UI_Container(int index)
            => _UI_Container__CHILD_ELEMENTS[index];

        protected T Get__Element__UI_Container<T>(int index) where T : UI_Element
            => Get__Indexed_Element__UI_Container(index).UI_Indexed_Element__ELEMENT as T;
        
        protected bool Check_If__Index_Within_Bounds__UI_Container(int index)
            => MathHelper.CheckIf__Obeys_IClamp(index, 0, UI_Container__UI_Element_Count);
        
        internal List<UI_Indexed_Element> Internal_Get__CHILD_ELEMENTS__UI_Container()
            => _UI_Container__CHILD_ELEMENTS;

        protected UI_Indexed_Element[] Get__CHILD_ELEMENTS__UI_Container()
            => _UI_Container__CHILD_ELEMENTS.ToArray();

        protected void For_Each__Child_Element__UI_Container(Action<UI_Indexed_Element> action)
        {
            foreach (UI_Indexed_Element indexedElement in _UI_Container__CHILD_ELEMENTS)
                action.Invoke(indexedElement);
        }
        
        /// <summary>
        /// Adds, sorts, and binds scaling values based on container implementation.
        /// </summary>
        /// <param name="childElement"></param>
        protected bool Add__UI_Element__UI_Container(UI_Indexed_Element indexedElement)
        {
            UI_Element childElement = indexedElement.UI_Indexed_Element__ELEMENT;
            Vector3? sortedPosition = Private_Attempt__Sort__UI_Container(indexedElement);

            bool success = sortedPosition != null;
            
            if (success)
            {
                childElement.Internal_Set__Position__UI_Element((Vector3) sortedPosition);
                _UI_Container__CHILD_ELEMENTS.Add(indexedElement);
                Private_Bind__Relative_Position_To_Anchor__UI_Container(indexedElement);
            }

            return success;
        }

        private Vector3? Private_Attempt__Sort__UI_Container(UI_Indexed_Element indexedElement)
        {
            Vector3 initalPosition =
                Handle_Get__Initial_Position_For_Element__UI_Container(indexedElement)
                + Get__Offset_Against_Anchors__UI_Container(indexedElement);
            Vector3 minorAnchorPosition = initalPosition;
            Vector3 sortedPosition = initalPosition;
            
            while (Handle_Check_For__Sort_Integrity__UI_Container(indexedElement, initalPosition))
            {
                bool wasSorted;
                
                sortedPosition =
                    Private_Sort__UI_Element_On_Major_Anchor__UI_Container
                    (
                        indexedElement.UI_Indexed_Element__Major_Sort,
                        indexedElement,
                        sortedPosition,
                        out wasSorted
                    );

                sortedPosition = minorAnchorPosition = Private_Verify__Major_Sort__UI_Container
                (
                    indexedElement,
                    minorAnchorPosition,
                    sortedPosition
                );
                
                if (!wasSorted)
                {
                    return sortedPosition;
                }
            }

            return null;
        }

        /// <summary>
        /// After being sorted a long an anchor in Private_Attempt__Sort__UI_Container,
        /// check if the sort is bounded by the container, otherwise fallback to recovery.
        /// </summary>
        /// <param name="indexedElement"></param>
        /// <param name="nullableSortedPosition"></param>
        private Vector3 Private_Verify__Major_Sort__UI_Container
        (
            UI_Indexed_Element indexedElement, 
            Vector3 minorAnchorPosition,
            Vector3 sortedPosition
        )
        {
            if (Handle_Check_For__Sort_Integrity__UI_Container(indexedElement, sortedPosition))
            {
                return sortedPosition;
            }

            return Handle_Recover__Sort__UI_Container(indexedElement, minorAnchorPosition);
        }

        protected virtual Vector3 Handle_Recover__Sort__UI_Container
        (
            UI_Indexed_Element indexedElement, 
            Vector3 minorAnchorPosition
        )
        {
            Vector3 minorHadamard = Vector3.One;

            UI_Anchor_Sort_Type minorSort = indexedElement.UI_Indexed_Element__Minor_Sort;

            Vector3? offset = Private_Sort__Offset_Step__UI_Container
            (
                minorSort,
                indexedElement,
                minorAnchorPosition
            );

            return minorAnchorPosition + (offset ?? Vector3.Zero);
        }
        
        /// <summary>
        /// Determines the initial position for a given element. This typically relates to the container's anchors.
        /// </summary>
        /// <param name="indexedElementToSort"></param>
        /// <returns></returns>
        protected virtual Vector3 Handle_Get__Initial_Position_For_Element__UI_Container(
            UI_Indexed_Element indexedElementToSort)
        {
            Vector3 basePosition =
                Get__Anchor_Position__UI_Element(indexedElementToSort.UI_Indexed_Element__Anchor_Position_Type);

            return basePosition;
        }

        private Vector3 Get__Offset_Against_Anchors__UI_Container(UI_Indexed_Element indexedElement)
        {
            UI_Element element = indexedElement.UI_Indexed_Element__ELEMENT;
            
            Vector3 offset = 
                Handle_Get__Offset_Against_Anchor__UI_Container
                (
                    indexedElement.UI_Indexed_Element__Anchor_Position_Type,
                    element.UI_Element__BOUNDING_RECT
                );

            return offset;
        }

        /// <summary>
        /// Gets the offset needed to keep the element within the container's edge.
        /// </summary>
        /// <param name="indexedElement"></param>
        /// <returns></returns>
        protected virtual Vector3 Handle_Get__Offset_Against_Anchor__UI_Container
        (
            UI_Anchor_Position_Type anchorPositionType,
            UI_Rect elementRect
        )
        {
            Vector3 ret = Vector3.Zero;
            
            switch (anchorPositionType)
            {
                case UI_Anchor_Position_Type.Top_Left:
                case UI_Anchor_Position_Type.Top_Middle:
                case UI_Anchor_Position_Type.Top_Right:
                    ret = -elementRect.UI_Rect__Height__As_Vector3
                        + new Vector3(0, elementRect.UI_Rect__Local_Origin_Offset__Internal.Y, 0);
                    break;
            }

            switch (anchorPositionType)
            {
                case UI_Anchor_Position_Type.Top_Right:
                case UI_Anchor_Position_Type.Middle_Right:
                case UI_Anchor_Position_Type.Bottom_Right:
                    ret += -elementRect.UI_Rect__Width__As_Vector3
                        + new Vector3(elementRect.UI_Rect__Local_Origin_Offset__Internal.X, 0, 0);
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Determines if the element can still be sorted in this container.
        /// </summary>
        /// <returns></returns>
        protected virtual bool Handle_Check_For__Sort_Integrity__UI_Container
        (
            UI_Indexed_Element indexedElementToSort,
            Vector3 sortedPosition
        )
        {
            UI_Element element = indexedElementToSort.UI_Indexed_Element__ELEMENT;
            
            return UI_Rect.CheckIf__Rect_Is_Bound_By_Rect
            (
                element.UI_Element__BOUNDING_RECT,
                UI_Element__BOUNDING_RECT,
                sortedPosition
            );
        }

        private UI_Indexed_Element Private_Find__Overlapping_Element__UI_Container
        (
            UI_Indexed_Element indexedElementToSort,
            Vector3 sortedPosition
        )
        {
            UI_Element elementToSort = indexedElementToSort.UI_Indexed_Element__ELEMENT;
            
            foreach (UI_Indexed_Element indexedChildElement in _UI_Container__CHILD_ELEMENTS)
            {
                UI_Element childElement = indexedChildElement.UI_Indexed_Element__ELEMENT;

                if
                (
                    UI_Rect.CheckIf__Rects_Overlap
                    (
                        elementToSort.UI_Element__BOUNDING_RECT,
                        childElement.UI_Element__BOUNDING_RECT,
                        sortedPosition
                    )
                )
                    return indexedChildElement;
            }

            return null;
        }
        
        private Vector3 Private_Sort__UI_Element_On_Major_Anchor__UI_Container
            (
            UI_Anchor_Sort_Type anchorSortType,
            UI_Indexed_Element indexedElementToSort,
            Vector3 sortedPosition,
            out bool possessedOverlap
            )
        {
            Vector3 offsetPosition = Vector3.Zero;
            Vector3? offsetStep = Vector3.Zero;

            do
            {
                offsetStep = Private_Sort__Offset_Step__UI_Container
                (
                    anchorSortType,
                    indexedElementToSort,
                    sortedPosition + offsetPosition
                );

                offsetPosition += offsetStep ?? Vector3.Zero;
            } while (offsetStep != null);

            possessedOverlap = offsetPosition != Vector3.Zero;
            
            return sortedPosition + offsetPosition;
        }

        private Vector3? Private_Sort__Offset_Step__UI_Container
        (
            UI_Anchor_Sort_Type anchorSortType,
            UI_Indexed_Element indexedElementToSort,
            Vector3 targetPosition
        )
        {
            UI_Indexed_Element overlappingIndexedElement = Private_Find__Overlapping_Element__UI_Container
            (
                indexedElementToSort,
                targetPosition
            );

            if (overlappingIndexedElement == null)
                return null;

            UI_Element elementToSort = indexedElementToSort.UI_Indexed_Element__ELEMENT;
            UI_Element overlappingElement = overlappingIndexedElement.UI_Indexed_Element__ELEMENT;
            
            Vector3 offset = Handle_Get__Alignment_Offset__UI_Container
            (
                anchorSortType,
                elementToSort.UI_Element__BOUNDING_RECT,
                overlappingElement.UI_Element__BOUNDING_RECT
            );

            return offset;
        }

        /// <summary>
        /// Determines the offset required by an overlapping element to become isolated.
        /// Returns null if an offset cannot be determined.
        /// </summary>
        /// <param name="anchorSortType"></param>
        /// <param name="rect_OfElement_ToSort"></param>
        /// <param name="rect_OfOverlapping_ChildElement"></param>
        /// <returns></returns>
        protected virtual Vector3 Handle_Get__Alignment_Offset__UI_Container
        (
            UI_Anchor_Sort_Type anchorSortType,
            UI_Rect rect_OfElement_ToSort,
            UI_Rect rect_OfOverlapping_ChildElement
        )
        {
            switch (anchorSortType)
            {
                //Moves left of the compared element.
                case UI_Anchor_Sort_Type.Left:
                    return 
                        -rect_OfElement_ToSort.UI_Rect__Width__As_Vector3;
                //Moves right of the compared element.
                case UI_Anchor_Sort_Type.Right:
                    return
                        rect_OfOverlapping_ChildElement.UI_Rect__Width__As_Vector3;
                //Moves up of the compared element.
                case UI_Anchor_Sort_Type.Top:
                    return
                        rect_OfOverlapping_ChildElement.UI_Rect__Height__As_Vector3;
                //Moves down of the compared element.
                case UI_Anchor_Sort_Type.Bottom:
                    return 
                        -rect_OfElement_ToSort.UI_Rect__Height__As_Vector3;
            }
            
            //critical failure has occured. This should never happen.
            throw new InvalidOperationException();
        }

        private void Private_Bind__Relative_Position_To_Anchor__UI_Container(UI_Indexed_Element indexedElement)
        {
            Vector3 relativePosition = Handle_Get__Relative_Position_To_Anchor__UI_Container(indexedElement);
            
            indexedElement.Set__Relative_Position_From_Anchor__UI_Indexed_Element(relativePosition);
        }

        /// <summary>
        /// Virtualized for base functionality. Finds the distance to the associated anchor minus the offset in element size.
        /// </summary>
        /// <param name="indexedElement"></param>
        /// <returns></returns>
        protected virtual Vector3 Handle_Get__Relative_Position_To_Anchor__UI_Container(UI_Indexed_Element indexedElement)
        {
            UI_Element element = indexedElement.UI_Indexed_Element__ELEMENT;
            UI_Anchor_Position_Type positionType = indexedElement.UI_Indexed_Element__Anchor_Position_Type;

            Vector3 anchorPoint = Get__Anchor_Position__UI_Element(positionType);

            Vector3 relativePosition_PriorToSizeOffset = element.UI_Element__Position_Without_Local_Origin_Offset - anchorPoint;

            Vector3 relativePosition = relativePosition_PriorToSizeOffset - Get__Offset_Against_Anchors__UI_Container(indexedElement);

            return relativePosition;
        }
        
        internal override void Internal_Set__Position__UI_Element(Vector3 position)
        {
            //TODO: make the position clamped to possible parent element.
            UI_Element__BOUNDING_RECT.Internal_Set__Position__UI_Rect(position);

            foreach (UI_Indexed_Element elementContainer in _UI_Container__CHILD_ELEMENTS)
            {
                UI_Element element = elementContainer.UI_Indexed_Element__ELEMENT;
                Vector3 childPosition = Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(elementContainer);
                
                element.Internal_Set__Position__UI_Element(childPosition);
            }
        }
        
        internal override void Internal_Handle_Scale__UI_Element(float newHypotenuse)
        {
            base.Internal_Handle_Scale__UI_Element(newHypotenuse);

            foreach (UI_Indexed_Element elementContainer in _UI_Container__CHILD_ELEMENTS)
            {
                Private_Scale__Child_Element_Size__UI_Container(elementContainer);
                Private_Scale__Child_Element_Position__UI_Container(elementContainer);
            }
        }

        private void Private_Scale__Child_Element_Position__UI_Container(UI_Indexed_Element indexedElement)
        {
            Vector3 position = Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(indexedElement);

            UI_Element element = indexedElement.UI_Indexed_Element__ELEMENT;
            element.Internal_Set__Position__UI_Element(position);
        }

        protected virtual Vector3 Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(
            UI_Indexed_Element indexedElement)
        {
            Vector3 anchorPosition =
                Get__Anchor_Position__UI_Element(indexedElement.UI_Indexed_Element__Anchor_Position_Type);
            Vector3 scaledPositionFromAnchor = indexedElement.Get__Current_Position_From_Anchor__UI_Indexed_Element();
            Vector3 offsetAgainstAnchor = Handle_Get__Offset_Against_Anchor__UI_Container
            (
                indexedElement.UI_Indexed_Element__Anchor_Position_Type,
                indexedElement.UI_Indexed_Element__ELEMENT.UI_Element__BOUNDING_RECT
            );
            
            Vector3 elementPosition =
                    UI_Element__Position
                    + anchorPosition
                    + scaledPositionFromAnchor
                    + offsetAgainstAnchor
                ;

            return elementPosition;
        }
        
        private void Private_Scale__Child_Element_Size__UI_Container(UI_Indexed_Element indexedElement)
        {
            float? newHypotenuse = Handle_Scale__Determine_Child_Hypotenuse__UI_Container(indexedElement);
            
            indexedElement.Internal_Scale__Element__UI_Indexed_Element(newHypotenuse);
        }
        
        /// <summary>
        /// Returns null if to resort to internal default size scaling.
        /// </summary>
        /// <param name="indexedElement"></param>
        /// <returns></returns>
        protected virtual float? Handle_Scale__Determine_Child_Hypotenuse__UI_Container(UI_Indexed_Element indexedElement)
        {
            return null;
        }
        
        public UI_Container
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
    }
}