using System;
using OpenTK;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI
{
    /// <summary>
    /// Wraps UI_Element and provides localized position logic.
    /// </summary>
    public class UI_Indexed_Element
    {
        /// <summary>
        /// The UI_Element that indexes this element.
        /// </summary>
        protected UI_Container UI_Indexed_Element__PARENT_CONTAINER { get; }

        public UI_Element UI_Indexed_Element__ELEMENT { get; }

        public UI_GameObject UI_Element__Associated_UI_GameObject__Reference
            => UI_Indexed_Element__ELEMENT.UI_Element__Associated_UI_GameObject;

        public Vector3 Get__Local_Origin_Offset__Of_ELEMENT__UI_Indexed_Element()
            => UI_Indexed_Element__ELEMENT.Get__Local_Origin_Offset__UI_Element();
        
        #region Scaling
        private float UI_Indexed_Element__RATIO_OF_HYPOTENUSE_TO_PARENT { get; }
        
        internal void Internal_Scale__Element__UI_Indexed_Element(float? newHypotenuse = null)
            => UI_Indexed_Element__ELEMENT.Internal_Scale__UI_Element
            (
                newHypotenuse 
                ??
                UI_Indexed_Element__PARENT_CONTAINER.Get__Hypotenuse_Of_Rect__UI_Element()
                *
                UI_Indexed_Element__RATIO_OF_HYPOTENUSE_TO_PARENT
            );
        
        #endregion
        
        #region Anchor
        /// <summary>
        /// Used for determining sorted positions.
        /// </summary>
        public UI_Anchor UI_Indexed_Element__Anchor { get; private set; }

        public UI_Anchor_Position_Type UI_Indexed_Element__Anchor_Position_Type
            => UI_Indexed_Element__Anchor.UI_Anchor__POSITION_TYPE;

        public UI_Anchor_Sort_Type Get__Major_Sort_Type__UI_Indexed_Element
            => UI_Indexed_Element__Anchor.Get__Major_Sort_Type__UI_Anchor();

        public UI_Anchor_Sort_Type Get__Minor_Sort_Type__UI_Indexed_Element
            => UI_Indexed_Element__Anchor.Get__Minor_Sort_Type__UI_Anchor();
        
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        public Vector3 UI_Indexed_Element__Position_From_Anchor { get; private set; }
        public float UI_Indexed_Element__Hypotenuse_Of_Position_From_Anchor { get; private set; }
        public float UI_Indexed_Element__Ratio_Of_Hypotenuse_From_Anchor_To_Parent { get; private set; }
        public Vector3 Get_Normalized__Position_From_Anchor__UI_Indexed_Element()
            => MathHelper.Get__Safe_Normalized(UI_Indexed_Element__Position_From_Anchor);
        public Vector3 Get__Current_Position_From_Anchor__UI_Indexed_Element()
            =>
                Get_Normalized__Position_From_Anchor__UI_Indexed_Element()
                * UI_Indexed_Element__Ratio_Of_Hypotenuse_From_Anchor_To_Parent
                * UI_Indexed_Element__PARENT_CONTAINER.Get__Hypotenuse_Of_Rect__UI_Element();

        internal void Set__Relative_Position_From_Anchor__UI_Indexed_Element(Vector3 offset)
        {
            UI_Indexed_Element__Position_From_Anchor = offset;
            UI_Indexed_Element__Hypotenuse_Of_Position_From_Anchor = MathHelper.Get__Hypotenuse(offset.Xy);
            UI_Indexed_Element__Ratio_Of_Hypotenuse_From_Anchor_To_Parent =
                UI_Indexed_Element__Hypotenuse_Of_Position_From_Anchor /
                UI_Indexed_Element__PARENT_CONTAINER.Get__Hypotenuse_Of_Rect__UI_Element();
        }

        public UI_Indexed_Element
        (
            UI_Element indexedElement,
            UI_Container parentContainer,
            UI_Anchor_Sort_Style sortStyle = null
        )
        {
            UI_Anchor_Position_Type localOrigin = indexedElement.Get__Local_Origin_Position_Type__UI_Element();
            
            UI_Anchor_Sort_Style clampedSortStyle
                = new UI_Anchor_Sort_Style
                (
                    Clamp__Sort_Type
                        (
                        sortStyle?.UI_Anchor_Style__MAJOR ?? UI_Anchor_Sort_Type.Right,
                        localOrigin
                        ),
                    Clamp__Sort_Type
                        (
                        sortStyle?.UI_Anchor_Style__MINOR ?? UI_Anchor_Sort_Type.Bottom,
                        localOrigin
                        )
                );
            
            UI_Indexed_Element__ELEMENT = indexedElement;

            UI_Indexed_Element__Anchor =
                new UI_Anchor
                (
                    localOrigin,
                    clampedSortStyle
                );
            
            UI_Indexed_Element__PARENT_CONTAINER = parentContainer;

            UI_Indexed_Element__RATIO_OF_HYPOTENUSE_TO_PARENT =
                UI_Indexed_Element__ELEMENT.Get__Hypotenuse_Of_Rect__UI_Element() /
                UI_Indexed_Element__PARENT_CONTAINER.Get__Hypotenuse_Of_Rect__UI_Element();
        }

        public override string ToString()
        {
            return String.Format("Indexed_Element: [{0}]", UI_Indexed_Element__ELEMENT);
        }

        private static UI_Anchor_Sort_Type Clamp__Sort_Type
        (
            UI_Anchor_Sort_Type sortType,
            UI_Anchor_Position_Type positionType
        )
        {
            switch (sortType)
            {
                case UI_Anchor_Sort_Type.Left:
                case UI_Anchor_Sort_Type.Right:
                    return Clamp__Sort_Type__Horizontal(sortType, positionType);
                default:
                    return Clamp__Sort_Type__Vertical(sortType, positionType);
            }
        }
        
        private static UI_Anchor_Sort_Type Clamp__Sort_Type__Horizontal
        (
            UI_Anchor_Sort_Type sortType,
            UI_Anchor_Position_Type positionType
        )
        {
            switch (positionType)
            {
                case UI_Anchor_Position_Type.Top_Left:
                case UI_Anchor_Position_Type.Middle_Left:
                case UI_Anchor_Position_Type.Bottom_Left:    
                    sortType = UI_Anchor_Sort_Type.Right;
                    break;
                case UI_Anchor_Position_Type.Top_Right:
                case UI_Anchor_Position_Type.Middle_Right:
                case UI_Anchor_Position_Type.Bottom_Right:
                    sortType = UI_Anchor_Sort_Type.Left;
                    break;
            }
            
            return sortType;
        }

        private static UI_Anchor_Sort_Type Clamp__Sort_Type__Vertical
        (
            UI_Anchor_Sort_Type sortType,
            UI_Anchor_Position_Type positionType
        )
        {
            switch (positionType)
            {
                case UI_Anchor_Position_Type.Top_Left:
                case UI_Anchor_Position_Type.Top_Middle:
                case UI_Anchor_Position_Type.Top_Right:
                    sortType = UI_Anchor_Sort_Type.Bottom;
                    break;
                case UI_Anchor_Position_Type.Bottom_Left:
                case UI_Anchor_Position_Type.Bottom_Middle:
                case UI_Anchor_Position_Type.Bottom_Right:
                    sortType = UI_Anchor_Sort_Type.Top;
                    break;
            }
            
            return sortType;
        }
    }
}