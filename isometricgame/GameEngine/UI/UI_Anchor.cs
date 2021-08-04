using System;
using isometricgame.GameEngine.Systems;
using isometricgame.GameEngine.Tools;
using OpenTK;
using OpenTK.Graphics.ES10;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI
{
    public class UI_Anchor
    {
        internal UI_Anchor_Sort_Type UI_Anchor__Sort_Type__Internal { get; private set; }
        internal UI_Anchor_Padding UI_Anchor__Anchor_Padding__Internal { get; private set; }
        
        internal void Internal_Scale__Anchor_Paddings__UI_Anchor(Vector2 containerSize)
        {
            UI_Anchor__Anchor_Padding__Internal.Internal_Scale__Float_Buffer__UI_Anchor_Padding(containerSize);
        }
        
        internal UI_Anchor
        (
            UI_Anchor_Sort_Type uiAnchorSortTypeInternal,
            float paddingValue,
            UI_Anchor_Padding_Type paddingType
        )
        {
            UI_Anchor__Sort_Type__Internal = uiAnchorSortTypeInternal;
            UI_Anchor__Anchor_Padding__Internal = new UI_Anchor_Padding(paddingValue, paddingType, uiAnchorSortTypeInternal);
        }
        
        internal static UI_Anchor_Sort_Type Get__Internal_Sort_Type
        (
            UI_Horizontal_Anchor_Sort_Type horizontalAnchorSortType
        )
            => (UI_Anchor_Sort_Type) ((int) horizontalAnchorSortType);
        internal static UI_Anchor_Sort_Type Get__Internal_Sort_Type
        (
            UI_Vertical_Anchor_Sort_Type verticalAnchorSortType
        )
            => (UI_Anchor_Sort_Type) ((int) verticalAnchorSortType);

        internal static UI_Anchor_Position_Type Get__Internal_Position__From__One_Internal_Sort
        (
            UI_Anchor_Sort_Type internalSort
        )
        {
            switch (internalSort)
            {
                case UI_Anchor_Sort_Type.Left:
                case UI_Anchor_Sort_Type.Top:    
                    return UI_Anchor_Position_Type.Top_Left;
                    break;
                case UI_Anchor_Sort_Type.Right:
                    return UI_Anchor_Position_Type.Top_Right;
                    break;
                case UI_Anchor_Sort_Type.Bottom:
                    return UI_Anchor_Position_Type.Bottom_Left;
            }

            return UI_Anchor_Position_Type.Invalid;
        }
        
        internal static UI_Anchor_Position_Type Get__Internal_Position__From__Two_Internal_Sorts
        (
            UI_Anchor_Sort_Type internalSortMajor,
            UI_Anchor_Sort_Type internalSortLesser
        )
        {
            UI_Anchor_Position_Type position_1 = Get__Internal_Position__From__One_Internal_Sort(internalSortMajor);
            UI_Anchor_Position_Type position_2 = Get__Internal_Position__From__One_Internal_Sort(internalSortLesser);

            if (position_1 == UI_Anchor_Position_Type.Invalid ||
                position_2 == UI_Anchor_Position_Type.Invalid)
                return UI_Anchor_Position_Type.Invalid;

            return Merge__Internal_Position_Types(position_1, position_2);
        }

        internal static UI_Anchor_Position_Type Get__Internal_Position__From__Two_External_Sorts
        (
            UI_Horizontal_Anchor_Sort_Type sortType_1,
            UI_Vertical_Anchor_Sort_Type sortType_2
        )
        {
            return Get__Internal_Position__From__Two_Internal_Sorts
            (
                Get__Internal_Sort_Type(sortType_1),
                Get__Internal_Sort_Type(sortType_2)
            );
        }
        
        internal static UI_Anchor_Position_Type Merge__Internal_Position_Types
        (
            UI_Anchor_Position_Type internalPosition_1,
            UI_Anchor_Position_Type internalPosition_2
        )
        {
            int sum = (int) internalPosition_1 + (int) internalPosition_2;

            if
            (
                !MathHelper.Obeys_IClamp
                (
                    sum,
                    (int) UI_Anchor_Position_Type.Top_Left,
                    (int) UI_Anchor_Position_Type.Bottom_Right
                )
            )
                return UI_Anchor_Position_Type.Invalid;
            
            return (UI_Anchor_Position_Type) (sum);
        }
    }
}