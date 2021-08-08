using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Success 
        /// </summary>
        public bool UI_Container__Element_Addition_Success_State { get; private set; }

        #region Get-Elements
        protected UI_Indexed_Element Get__Indexed_Element__UI_Container(int index)
            => _UI_Container__CHILD_ELEMENTS[index];

        protected T Get__Element__UI_Container<T>(int index) where T : UI_Element
            => Get__Indexed_Element__UI_Container(index).UI_Indexed_Element__ELEMENT as T;

        internal List<UI_Indexed_Element> Internal_Get__CHILD_ELEMENTS__UI_Container()
            => Handle_Internal_Get__CHILD_ELEMENTS__UI_Container();

        protected virtual List<UI_Indexed_Element> Handle_Internal_Get__CHILD_ELEMENTS__UI_Container()
            => _UI_Container__CHILD_ELEMENTS.ToList();

        protected UI_Indexed_Element[] Get__CHILD_ELEMENTS__UI_Container()
            => _UI_Container__CHILD_ELEMENTS.ToArray();
        
        protected bool Check_If__Index_Within_Bounds__UI_Container(int index)
            => MathHelper.CheckIf__Obeys_IClamp(index, 0, UI_Container__UI_Element_Count);
        #endregion
        
        #region Add-Elements

        /// <summary>
        /// Adds, sorts, and binds scaling values based on container implementation.
        /// </summary>
        /// <param name="childElement"></param>
        protected bool Add__UI_Element__UI_Container(UI_Element element, UI_Anchor bindingAnchor = null)
        {
            UI_Anchor clampedAnchor = new UI_Anchor();
            
            clampedAnchor.UI_Anchor__Sort_Style = Private_Clamp__Sort_Style__UI_Container
            (
                bindingAnchor?.UI_Anchor__Target_Anchor_Point ?? UI_Anchor_Position_Type.Top_Left,
                bindingAnchor?.Get__Major_Sort_Type__UI_Anchor() ?? UI_Anchor_Sort_Type.Right,
                bindingAnchor?.Get__Minor_Sort_Type__UI_Anchor() ?? UI_Anchor_Sort_Type.Bottom
            );
            
            element.Internal_Set__Local_Origin_Position_Type__UI_Element
            (
                Handle_Clamp__Added_Element_Local_Origin__UI_Container
                (
                    element.Get__Local_Origin_Position_Type__UI_Element(),
                    clampedAnchor.UI_Anchor__Target_Anchor_Point
                )
            );

            UI_Indexed_Element indexedElement = new UI_Indexed_Element
            (
                element,
                clampedAnchor,
                this
            );
            
            Vector3? sortedPosition = Private_Attempt__Sort__UI_Container(indexedElement);

            UI_Container__Element_Addition_Success_State = sortedPosition != null;
            
            if (UI_Container__Element_Addition_Success_State)
            {
                element.Internal_Set__Position__UI_Element((Vector3) sortedPosition);
                _UI_Container__CHILD_ELEMENTS.Add(indexedElement);
                Private_Bind__Relative_Position_To_Anchor__UI_Container(indexedElement);
            }

            return UI_Container__Element_Addition_Success_State;
        }

        #region Pre--Add-Element

        protected virtual UI_Anchor_Position_Type Handle_Clamp__Added_Element_Local_Origin__UI_Container
        (
            UI_Anchor_Position_Type localOrigin,
            UI_Anchor_Position_Type targetAnchor
        )
        {
            return targetAnchor;
        }
        
        private UI_Anchor_Sort_Style Private_Clamp__Sort_Style__UI_Container
        (
            UI_Anchor_Position_Type elementPositionType,
            UI_Anchor_Sort_Type major,
            UI_Anchor_Sort_Type minor
        )
        {
            UI_Anchor_Sort_Type clampedMajor =
                Private_Clamp__Sort_Type__UI_Container(elementPositionType, major);
            UI_Anchor_Sort_Type clampedMinor =
                Private_Clamp__Sort_Type__UI_Container(elementPositionType, minor);

            return new UI_Anchor_Sort_Style(clampedMajor, clampedMinor);
        }

        private UI_Anchor_Sort_Type Private_Clamp__Sort_Type__UI_Container
        (
            UI_Anchor_Position_Type elementPositionType,
            UI_Anchor_Sort_Type sortType
        )
        {
            switch (sortType)
            {
                case UI_Anchor_Sort_Type.Left:
                case UI_Anchor_Sort_Type.Right:
                    return Handle_Clamp__Horizontal_Sort_Type__UI_Container(elementPositionType, sortType);
                default:
                    return Handle_Clamp__Vertical_Sort_Type__UI_Container(elementPositionType, sortType);
            }
        }
        
        protected virtual UI_Anchor_Sort_Type Handle_Clamp__Horizontal_Sort_Type__UI_Container
        (
            UI_Anchor_Position_Type elementPositionType,
            UI_Anchor_Sort_Type horizontalSortType
        )
        {
            switch (elementPositionType)
            {
                case UI_Anchor_Position_Type.Top_Left:
                case UI_Anchor_Position_Type.Middle_Left:
                case UI_Anchor_Position_Type.Bottom_Left:
                    return UI_Anchor_Sort_Type.Right;
                default:
                    return UI_Anchor_Sort_Type.Left;
            }
        }

        protected virtual UI_Anchor_Sort_Type Handle_Clamp__Vertical_Sort_Type__UI_Container
            (
            UI_Anchor_Position_Type elementPositionType,
            UI_Anchor_Sort_Type verticalSortType
            )
        {
            switch (elementPositionType)
            {
                case UI_Anchor_Position_Type.Top_Left:
                case UI_Anchor_Position_Type.Top_Middle:
                case UI_Anchor_Position_Type.Top_Right:
                    return UI_Anchor_Sort_Type.Bottom;
                default:
                    return UI_Anchor_Sort_Type.Top;
            }
        }

        #endregion
        
        #region Post--Add-Element
        
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

            Vector3 relativePosition = 
                element.Get__Position_In_UISpace__UI_Element() 
                - anchorPoint;

            return relativePosition;
        }
        
        #endregion
        
        #region Add-Element--Sort
        
        private Vector3? Private_Attempt__Sort__UI_Container(UI_Indexed_Element indexedElement)
        {
            Vector3 minorAnchorPosition = Handle_Get__Initial_Position_For_Element__UI_Container(indexedElement);
            Vector3 sortedPosition = Handle_Get__Initial_Position_For_Element__UI_Container(indexedElement);
            
            while (Handle_Check_For__Sort_Integrity__UI_Container(indexedElement, sortedPosition))
            {
                bool wasSorted;
                
                sortedPosition =
                    Private_Sort__UI_Element_On_Major_Anchor__UI_Container
                    (
                        indexedElement.Get__Major_Sort_Type__UI_Indexed_Element,
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
        /// Determines the initial position for a given element. This typically relates to the container's anchors.
        /// </summary>
        /// <param name="indexedElementToSort"></param>
        /// <returns></returns>
        protected virtual Vector3 Handle_Get__Initial_Position_For_Element__UI_Container
        (
            UI_Indexed_Element indexedElementToSort
        )
        {
            Vector3 basePosition =
                Get__Anchor_Position__UI_Element(indexedElementToSort.UI_Indexed_Element__Anchor_Position_Type);

            return basePosition;
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

            UI_Anchor_Sort_Type minorSort = indexedElement.Get__Minor_Sort_Type__UI_Indexed_Element;

            Vector3? offset = Private_Sort__Offset_Step__UI_Container
            (
                minorSort,
                indexedElement,
                minorAnchorPosition
            );

            return minorAnchorPosition + (offset ?? Vector3.Zero);
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
        
        #endregion
        
        #endregion
        
        #region Mutate-Container
        
        #region Mutate-Size--Of-Container

        internal override void Internal_Handle_Scale__UI_Element(float newHypotenuse)
        {
            base.Internal_Handle_Scale__UI_Element(newHypotenuse);

            foreach (UI_Indexed_Element elementContainer in _UI_Container__CHILD_ELEMENTS)
            {
                Private_Scale__Child_Element_Size__UI_Container(elementContainer);
                Private_Scale__Child_Element_Position__UI_Container(elementContainer);
            }
        }

        #region Handle-Scale-Mutation--Of-Container
        
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

        private void Private_Scale__Child_Element_Position__UI_Container(UI_Indexed_Element indexedElement)
        {
            Vector3 position = Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(indexedElement);

            UI_Element element = indexedElement.UI_Indexed_Element__ELEMENT;
            element.Internal_Set__Position__UI_Element(position);
        }

        #endregion

        #endregion

        #region  Mutate-Position--Of-Container

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

        protected virtual Vector3 Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(
            UI_Indexed_Element indexedElement)
        {
            Vector3 anchorPosition =
                Get__Anchor_Position__UI_Element(indexedElement.UI_Indexed_Element__Anchor_Position_Type);
            Vector3 scaledPositionFromAnchor = indexedElement.Get__Current_Position_From_Anchor__UI_Indexed_Element();
            
            Vector3 elementPosition =
                    anchorPosition
                    + scaledPositionFromAnchor
                ;

            return elementPosition;
        }

        #endregion
        
        #endregion

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