using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using Math_Helper = Xerxes_Engine.Tools.Math_Helper;

namespace Xerxes_Engine.UI
{
    /// <summary>
    /// The abstract foundation for Container UI_Elements. Offers base functionality for scaling and repositioning.
    /// </summary>
    public class UI_Container : UI_Element
    {
        private readonly List<UI_Anchored_Wrapper> _UI_Container__CHILD_ELEMENTS = new List<UI_Anchored_Wrapper>();
        public int UI_Container__UI_Element_Count => _UI_Container__CHILD_ELEMENTS.Count;

        /// <summary>
        /// Success 
        /// </summary>
        public bool UI_Container__Element_Addition_Success_State { get; private set; }

        #region Get-Elements
        protected UI_Anchored_Wrapper Get__Indexed_Element__UI_Container(int index)
            => _UI_Container__CHILD_ELEMENTS[index];

        protected T Get__Element__UI_Container<T>(int index) where T : UI_Element
            => Get__Indexed_Element__UI_Container(index).UI_Wrapper__WRAPPED_ELEMENT as T;

        internal List<UI_Wrapper> Internal_Get__CHILD_ELEMENTS__UI_Container()
            => Handle_Internal_Get__CHILD_ELEMENTS__UI_Container();

        protected virtual List<UI_Wrapper> Handle_Internal_Get__CHILD_ELEMENTS__UI_Container()
            => _UI_Container__CHILD_ELEMENTS.ToList<UI_Wrapper>();

        protected UI_Anchored_Wrapper[] Get__CHILD_ELEMENTS__UI_Container()
            => _UI_Container__CHILD_ELEMENTS.ToArray();
        
        protected bool Check_If__Index_Within_Bounds__UI_Container(int index)
            => Math_Helper.Check_If__Obeys_Clamp__Integer(index, 0, UI_Container__UI_Element_Count);
        #endregion
        
        #region Add-Elements

        /// <summary>
        /// Adds, sorts, and binds scaling values based on container implementation.
        /// </summary>
        /// <param name="childElement"></param>
        protected bool Add__UI_Element__UI_Container(UI_Element element, UI_Anchor bindingAnchor = null)
        {
            UI_Anchor clampedAnchor = Private_Get__Clamped_Anchor__UI_Container(bindingAnchor);
            UI_Anchored_Wrapper anchoredWrapper = Private_Clamp__Element_Into_Anchored_Wrapper__UI_Container
            (
                element,
                clampedAnchor
            );
            
            Vector3? sortedPosition = Private_Attempt__Sort__UI_Container(anchoredWrapper);

            UI_Container__Element_Addition_Success_State = sortedPosition != null;
            
            if (UI_Container__Element_Addition_Success_State)
            {
                element.Internal_Set__Position__UI_Element((Vector3) sortedPosition);
                _UI_Container__CHILD_ELEMENTS.Add(anchoredWrapper);
                Private_Bind__Relative_Position_To_Anchor__UI_Container(anchoredWrapper);
            }

            return UI_Container__Element_Addition_Success_State;
        }

        #region Pre--Add-Element

        private UI_Anchored_Wrapper Private_Clamp__Element_Into_Anchored_Wrapper__UI_Container(UI_Element element, UI_Anchor clampedAnchor)
        {
            element.Internal_Set__Local_Origin_Position_Type__UI_Element
            (
                Handle_Clamp__Added_Element_Local_Origin__UI_Container
                (
                    element.Get__Local_Origin_Position_Type__UI_Element(),
                    clampedAnchor.UI_Anchor__Target_Anchor_Point
                )
            );

            UI_Anchored_Wrapper anchoredWrapper = new UI_Anchored_Wrapper
            (
                element,
                this,
                clampedAnchor
            );

            return anchoredWrapper;
        }
        
        private UI_Anchor Private_Get__Clamped_Anchor__UI_Container(UI_Anchor bindingAnchor)
        {
            UI_Anchor clampedAnchor = new UI_Anchor
            (
                bindingAnchor?.UI_Anchor__Target_Anchor_Point ?? UI_Anchor_Position_Type.Top_Left,
                bindingAnchor?.UI_Anchor__Offset_Type__UI_Anchor ?? UI_Anchor_Offset_Type.Pixel,
                bindingAnchor?.UI_Anchor__Offset_Vector__UI_Anchor ?? Vector3.Zero
            );
            
            clampedAnchor.UI_Anchor__Sort_Style = Private_Clamp__Sort_Style__UI_Container
            (
                clampedAnchor.UI_Anchor__Target_Anchor_Point,
                bindingAnchor?.Get__Major_Sort_Type__UI_Anchor() ?? UI_Anchor_Sort_Type.Right,
                bindingAnchor?.Get__Minor_Sort_Type__UI_Anchor() ?? UI_Anchor_Sort_Type.Bottom
            );

            return clampedAnchor;
        }
        
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
                case UI_Anchor_Position_Type.Top_Right:
                case UI_Anchor_Position_Type.Middle_Right:
                case UI_Anchor_Position_Type.Bottom_Right:    
                    return UI_Anchor_Sort_Type.Left;
                default:
                    return horizontalSortType;
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
                case UI_Anchor_Position_Type.Bottom_Left:
                case UI_Anchor_Position_Type.Bottom_Middle:
                case UI_Anchor_Position_Type.Bottom_Right:
                    return UI_Anchor_Sort_Type.Top;    
                default:
                    return verticalSortType;
            }
        }

        #endregion
        
        #region Post--Add-Element
        
        private void Private_Bind__Relative_Position_To_Anchor__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            Vector3 relativePosition = Handle_Get__Relative_Position_To_Anchor__UI_Container(anchoredWrapper);
            
            anchoredWrapper.Set__Relative_Position_From_Anchor__UI_Indexed_Element(relativePosition);
        }
        /// <summary>
        /// Virtualized for base functionality. Finds the distance to the associated anchor minus the offset in element size.
        /// </summary>
        /// <param name="anchoredWrapper"></param>
        /// <returns></returns>
        protected virtual Vector3 Handle_Get__Relative_Position_To_Anchor__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            UI_Element element = anchoredWrapper.UI_Wrapper__WRAPPED_ELEMENT;
            UI_Anchor_Position_Type positionType = anchoredWrapper.UI_Indexed_Element__Anchor_Position_Type;

            Vector3 anchorPoint = Get__Anchor_Position__UI_Element(positionType);

            Vector3 relativePosition = 
                element.Get__Position_In_UISpace__UI_Element() 
                - anchorPoint;

            return relativePosition;
        }
        
        #endregion
        
        #region Add-Element--Sort
        
        private Vector3? Private_Attempt__Sort__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            Vector3 minorAnchorPosition = Handle_Get__Initial_Position_For_Element__UI_Container(anchoredWrapper);
            Vector3 sortedPosition = Handle_Get__Initial_Position_For_Element__UI_Container(anchoredWrapper);
            
            while (Handle_Check_For__Sort_Integrity__UI_Container(anchoredWrapper, sortedPosition))
            {
                bool wasSorted;
                
                sortedPosition =
                    Private_Sort__UI_Element_On_Major_Anchor__UI_Container
                    (
                        anchoredWrapper.Get__Major_Sort_Type__UI_Indexed_Element,
                        anchoredWrapper,
                        sortedPosition,
                        out wasSorted
                    );

                sortedPosition = minorAnchorPosition = Private_Verify__Major_Sort__UI_Container
                (
                    anchoredWrapper,
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
        /// <param name="anchoredWrapper"></param>
        /// <returns></returns>
        protected virtual Vector3 Handle_Get__Initial_Position_For_Element__UI_Container
        (
            UI_Anchored_Wrapper anchoredWrapper
        )
        {
            Vector3 basePosition =
                Get__Anchor_Position__UI_Element(anchoredWrapper.UI_Indexed_Element__Anchor_Position_Type)
                + Private_Get__Offset_From_Anchor__UI_Container(anchoredWrapper);

            return basePosition;
        }

        private Vector3 Private_Get__Offset_From_Anchor__UI_Container
        (
            UI_Anchored_Wrapper anchoredWrapper
        )
        {
            UI_Anchor anchor = anchoredWrapper.UI_Indexed_Element__Anchor;

            switch (anchor.UI_Anchor__Offset_Type__UI_Anchor)
            {
                case UI_Anchor_Offset_Type.Percentage:
                    return Math_Helper.Get__Hadamard_Product
                    (
                        anchor.UI_Anchor__Offset_Vector__UI_Anchor,
                        UI_Element__BOUNDING_RECT.UI_Rect__Size__As_Vector3
                    );
                default:
                    return anchor.UI_Anchor__Offset_Vector__UI_Anchor;
            }
        }
        
        /// <summary>
        /// Determines if the element can still be sorted in this container.
        /// </summary>
        /// <returns></returns>
        protected virtual bool Handle_Check_For__Sort_Integrity__UI_Container
        (
            UI_Anchored_Wrapper anchoredWrapperToSort,
            Vector3 sortedPosition
        )
        {
            UI_Element element = anchoredWrapperToSort.UI_Wrapper__WRAPPED_ELEMENT;
            
            bool isWithin = UI_Rect.CheckIf__Rect_Is_Within_Rect
            (
                element.UI_Element__BOUNDING_RECT,
                UI_Element__BOUNDING_RECT,
                sortedPosition
            );

            return isWithin;
        }
        
        private Vector3 Private_Sort__UI_Element_On_Major_Anchor__UI_Container
        (
            UI_Anchor_Sort_Type anchorSortType,
            UI_Anchored_Wrapper anchoredWrapperToSort,
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
                    anchoredWrapperToSort,
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
        /// <param name="anchoredWrapper"></param>
        /// <param name="nullableSortedPosition"></param>
        private Vector3 Private_Verify__Major_Sort__UI_Container
        (
            UI_Anchored_Wrapper anchoredWrapper, 
            Vector3 minorAnchorPosition,
            Vector3 sortedPosition
        )
        {
            if (Handle_Check_For__Sort_Integrity__UI_Container(anchoredWrapper, sortedPosition))
            {
                return sortedPosition;
            }

            return Handle_Recover__Sort__UI_Container(anchoredWrapper, minorAnchorPosition);
        }

        protected virtual Vector3 Handle_Recover__Sort__UI_Container
        (
            UI_Anchored_Wrapper anchoredWrapper, 
            Vector3 minorAnchorPosition
        )
        {
            Vector3 minorHadamard = Vector3.One;

            UI_Anchor_Sort_Type minorSort = anchoredWrapper.Get__Minor_Sort_Type__UI_Indexed_Element;

            Vector3? offset = Private_Sort__Offset_Step__UI_Container
            (
                minorSort,
                anchoredWrapper,
                minorAnchorPosition
            );

            return minorAnchorPosition + (offset ?? Vector3.Zero);
        }

        private Vector3? Private_Sort__Offset_Step__UI_Container
        (
            UI_Anchor_Sort_Type anchorSortType,
            UI_Anchored_Wrapper anchoredWrapperToSort,
            Vector3 targetPosition
        )
        {
            UI_Anchored_Wrapper overlappingAnchoredWrapper = Protected_Find__Overlapping_Element__UI_Container
            (
                anchoredWrapperToSort,
                targetPosition
            );

            if (overlappingAnchoredWrapper == null)
                return null;

            UI_Element elementToSort = anchoredWrapperToSort.UI_Wrapper__WRAPPED_ELEMENT;
            UI_Element overlappingElement = overlappingAnchoredWrapper.UI_Wrapper__WRAPPED_ELEMENT;
            
            Vector3 offset = Handle_Get__Alignment_Offset__UI_Container
            (
                anchorSortType,
                elementToSort.UI_Element__BOUNDING_RECT,
                overlappingElement.UI_Element__BOUNDING_RECT,
                targetPosition
            );

            return offset;
        }

        protected virtual UI_Anchored_Wrapper Protected_Find__Overlapping_Element__UI_Container
        (
            UI_Anchored_Wrapper anchoredWrapperToSort,
            Vector3 sortedPosition
        )
        {
            UI_Element elementToSort = anchoredWrapperToSort.UI_Wrapper__WRAPPED_ELEMENT;
            
            foreach (UI_Anchored_Wrapper indexedChildElement in _UI_Container__CHILD_ELEMENTS)
            {
                UI_Element childElement = indexedChildElement.UI_Wrapper__WRAPPED_ELEMENT;

                if
                (
                    UI_Rect.CheckIf__Rect_Overlaps_Rect
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
            UI_Rect rect_OfOverlapping_ChildElement,
            Vector3 targetPosition
        )
        {
            UI_Anchor_Position_Type localOrigin_PointType_SortingElement = rect_OfElement_ToSort.UI_Rect__Local_Origin_Type;

            UI_Anchor_Position_Type clamped_LocalOrigin_PointType_SortingElement
                = UI_Anchor.Clamp_To_Direction__If_Is_Middle
                (
                    localOrigin_PointType_SortingElement,
                    anchorSortType
                );

            UI_Anchor_Position_Type opposingLocalOrigin = UI_Anchor.Get__Opposite
            (
                clamped_LocalOrigin_PointType_SortingElement,
                anchorSortType
            );

            Vector3 clamped_LocalOrigin_Position_SortingElement =
                rect_OfElement_ToSort.Internal_Get__Anchor_Position__UI_Rect
                (
                    clamped_LocalOrigin_PointType_SortingElement
                )
                + 
                targetPosition
                ;

            Vector3 opposing_LocalOrigin_Position =
                rect_OfOverlapping_ChildElement.Internal_Get__Anchor_Position__UI_Rect
                (
                    opposingLocalOrigin
                );

            Vector3 leftHand = new Vector3(Single.NaN, Single.NaN, Single.NaN), rightHand = leftHand;

            switch (anchorSortType)
            {
                case UI_Anchor_Sort_Type.Left:
                case UI_Anchor_Sort_Type.Top:
                    leftHand = clamped_LocalOrigin_Position_SortingElement;
                    rightHand = opposing_LocalOrigin_Position;
                    break;
                case UI_Anchor_Sort_Type.Right:
                case UI_Anchor_Sort_Type.Bottom:
                    leftHand = opposing_LocalOrigin_Position;
                    rightHand = clamped_LocalOrigin_Position_SortingElement;
                    break;
            }

            Vector3 allAxisOffset = leftHand - rightHand;
            
            Vector3 offset_Hadamard_Vector = Vector3.One;
            
            switch (anchorSortType)
            {
                case UI_Anchor_Sort_Type.Left:
                case UI_Anchor_Sort_Type.Right:
                    offset_Hadamard_Vector = new Vector3(1, 0, 0);
                    break;
                case UI_Anchor_Sort_Type.Top:
                case UI_Anchor_Sort_Type.Bottom:
                    offset_Hadamard_Vector = new Vector3(0, 1, 0);
                    break;
            }

            Vector3 offset = Math_Helper.Get__Hadamard_Product
            (
                allAxisOffset,
                offset_Hadamard_Vector
            );

            return offset;
        }
        
        #endregion
        
        #endregion
        
        #region Mutate-Container
        
        #region Mutate-Size--Of-Container

        internal override void Internal_Handle_Scale__UI_Element(float newHypotenuse)
        {
            base.Internal_Handle_Scale__UI_Element(newHypotenuse);

            foreach (UI_Anchored_Wrapper elementContainer in _UI_Container__CHILD_ELEMENTS)
            {
                Private_Scale__Child_Element_Size__UI_Container(elementContainer);
                Private_Scale__Child_Element_Position__UI_Container(elementContainer);
            }
        }

        #region Handle-Scale-Mutation--Of-Container
        
        private void Private_Scale__Child_Element_Size__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            float? newHypotenuse = Handle_Scale__Determine_Child_Hypotenuse__UI_Container(anchoredWrapper);
            
            anchoredWrapper.Internal_Scale__Element__UI_Indexed_Element(newHypotenuse);
        }
        
        /// <summary>
        /// Returns null if to resort to internal default size scaling.
        /// </summary>
        /// <param name="anchoredWrapper"></param>
        /// <returns></returns>
        protected virtual float? Handle_Scale__Determine_Child_Hypotenuse__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            return null;
        }

        private void Private_Scale__Child_Element_Position__UI_Container(UI_Anchored_Wrapper anchoredWrapper)
        {
            Vector3 position = Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(anchoredWrapper);

            UI_Element element = anchoredWrapper.UI_Wrapper__WRAPPED_ELEMENT;
            element.Internal_Set__Position__UI_Element(position);
        }

        #endregion

        #endregion

        #region  Mutate-Position--Of-Container

        internal override void Internal_Set__Position__UI_Element(Vector3 position)
        {
            base.Internal_Set__Position__UI_Element(position);

            foreach (UI_Anchored_Wrapper elementContainer in _UI_Container__CHILD_ELEMENTS)
            {
                UI_Element element = elementContainer.UI_Wrapper__WRAPPED_ELEMENT;
                Vector3 childPosition = Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(elementContainer);
                
                element.Internal_Set__Position__UI_Element(childPosition);
            }
        }
        
        protected virtual Vector3 Handle_Post_Sort_Reposition__Of_Child_Element__UI_Container(
            UI_Anchored_Wrapper anchoredWrapper)
        {
            Vector3 anchorPosition =
                Get__Anchor_Position__UI_Element(anchoredWrapper.UI_Indexed_Element__Anchor_Position_Type);
            Vector3 scaledPositionFromAnchor = anchoredWrapper.Get__Current_Position_From_Anchor__UI_Indexed_Element();
            
            Vector3 elementPosition =
                    anchorPosition
                    + scaledPositionFromAnchor
                ;

            return elementPosition;
        }

        #endregion
        
        #endregion

        protected UI_Container
            (
            UI_Rect boundingRect
            )
        : base 
            (
            boundingRect
            )
        {
        }
    }
}
