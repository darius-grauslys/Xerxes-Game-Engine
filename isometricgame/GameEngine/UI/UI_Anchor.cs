using isometricgame.GameEngine.Systems;
using isometricgame.GameEngine.Tools;
using OpenTK;
using OpenTK.Graphics.ES10;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI
{
    /// <summary>
    /// Describes a contract for Containers on how to sort an indexed element.
    /// </summary>
    public class UI_Anchor
    {
        public readonly UI_Anchor_Position_Type UI_Anchor__POSITION_TYPE;

        public readonly UI_Anchor_Sort_Type UI_Anchor__MAJOR_SORT_TYPE;
        public readonly UI_Anchor_Sort_Type UI_Anchor__MINOR_SORT_TYPE;

        public UI_Anchor()
            : this 
            (
                UI_Anchor_Position_Type.Top_Left,
                UI_Anchor_Sort_Type.Right,
                UI_Anchor_Sort_Type.Bottom
            )
        {}

        public UI_Anchor
        (
            UI_Anchor_Position_Type positionType = UI_Anchor_Position_Type.Top_Left,
            UI_Horizontal_Anchor_Sort_Type majorSortType = UI_Horizontal_Anchor_Sort_Type.Right,
            UI_Vertical_Anchor_Sort_Type minorSortType = UI_Vertical_Anchor_Sort_Type.Bottom
        )
            : this
            (
                positionType,
                Internalize__External_Sort_Type(majorSortType, positionType),
                Internalize__External_Sort_Type(minorSortType, positionType)
            )
        {
            
        }
        
        public UI_Anchor
        (
            UI_Anchor_Position_Type positionType = UI_Anchor_Position_Type.Top_Left,
            UI_Vertical_Anchor_Sort_Type majorSortType = UI_Vertical_Anchor_Sort_Type.Bottom,
            UI_Horizontal_Anchor_Sort_Type minorSortType = UI_Horizontal_Anchor_Sort_Type.Right
        )
            : this
            (
                positionType,
                Internalize__External_Sort_Type(majorSortType, positionType),
                Internalize__External_Sort_Type(minorSortType, positionType)
            )
        {
            
        }
        
        internal UI_Anchor
        (
            UI_Anchor_Position_Type positionType = UI_Anchor_Position_Type.Top_Left,
            UI_Anchor_Sort_Type majorSortType = UI_Anchor_Sort_Type.Right,
            UI_Anchor_Sort_Type minorSortType = UI_Anchor_Sort_Type.Bottom
        )
        {
            UI_Anchor__POSITION_TYPE = positionType;
            UI_Anchor__MAJOR_SORT_TYPE = majorSortType;
            UI_Anchor__MINOR_SORT_TYPE = minorSortType;
        }

        internal static UI_Anchor_Sort_Type Internalize__External_Sort_Type
        (
            UI_Horizontal_Anchor_Sort_Type sortType,
            UI_Anchor_Position_Type positionType
        )
        {
            switch (positionType)
            {
                case UI_Anchor_Position_Type.Top_Left:
                case UI_Anchor_Position_Type.Middle_Left:
                case UI_Anchor_Position_Type.Bottom_Left:    
                    sortType = UI_Horizontal_Anchor_Sort_Type.Right;
                    break;
                case UI_Anchor_Position_Type.Top_Right:
                case UI_Anchor_Position_Type.Middle_Right:
                case UI_Anchor_Position_Type.Bottom_Right:
                    sortType = UI_Horizontal_Anchor_Sort_Type.Left;
                    break;
            }
            
            return Internalize__External_Sort_Type((int) sortType);
        }

        internal static UI_Anchor_Sort_Type Internalize__External_Sort_Type
        (
            UI_Vertical_Anchor_Sort_Type sortType,
            UI_Anchor_Position_Type positionType
        )
        {
            switch (positionType)
            {
                case UI_Anchor_Position_Type.Top_Left:
                case UI_Anchor_Position_Type.Top_Middle:
                case UI_Anchor_Position_Type.Top_Right:
                    sortType = UI_Vertical_Anchor_Sort_Type.Bottom;
                    break;
                case UI_Anchor_Position_Type.Bottom_Left:
                case UI_Anchor_Position_Type.Bottom_Middle:
                case UI_Anchor_Position_Type.Bottom_Right:
                    sortType = UI_Vertical_Anchor_Sort_Type.Top;
                    break;
            }
            
            return Internalize__External_Sort_Type((int) sortType);
        }

        private static UI_Anchor_Sort_Type Internalize__External_Sort_Type(int value)
            => (UI_Anchor_Sort_Type) value;
    }
}