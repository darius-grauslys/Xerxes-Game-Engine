using System;
using OpenTK;

namespace XerxesEngine.UI
{
    /// <summary>
    /// Describes a contract for Containers on how to sort an indexed element.
    /// </summary>
    public sealed class UI_Anchor
    {
        public UI_Anchor_Position_Type UI_Anchor__Target_Anchor_Point { get; internal set; }

        internal UI_Anchor_Sort_Style UI_Anchor__Sort_Style { get; set; }
        
        public UI_Anchor_Offset_Type UI_Anchor__Offset_Type__UI_Anchor { get; internal set; }
        
        public Vector3 UI_Anchor__Offset_Vector__UI_Anchor { get; internal set; }
        
        public UI_Anchor_Sort_Type Get__Major_Sort_Type__UI_Anchor()
            => UI_Anchor__Sort_Style.UI_Anchor_Style__MAJOR;
        public UI_Anchor_Sort_Type Get__Minor_Sort_Type__UI_Anchor()
            => UI_Anchor__Sort_Style.UI_Anchor_Style__MINOR;

        public UI_Anchor(UI_Anchor_Position_Type targetAnchorPoint)
            : this
                (
                targetAnchorPoint,
                UI_Anchor_Offset_Type.Pixel
                )
        {
            
        }
        
        public UI_Anchor
            (
            UI_Anchor_Position_Type targetAnchorPoint,
            UI_Anchor_Offset_Type offsetType = UI_Anchor_Offset_Type.Pixel,
            Vector3? offsetVector = null
            )
            : this
                (
                targetAnchorPoint,
                new UI_Anchor_Sort_Style(),
                offsetType,
                offsetVector
                )
        {
            
        }
        
        public UI_Anchor
            (
            UI_Anchor_Position_Type targetAnchorPoint,
            UI_Horizontal_Anchor_Sort_Type majorSort,
            UI_Vertical_Anchor_Sort_Type minorSort,
            UI_Anchor_Offset_Type offsetType = UI_Anchor_Offset_Type.Pixel,
            Vector3? offsetVector = null
            )
            : this
                (
                targetAnchorPoint,
                new UI_Anchor_Sort_Style((int)majorSort, (int)minorSort),
                offsetType,
                offsetVector
                )
        {
            
        }
        
        public UI_Anchor
        (
            UI_Anchor_Position_Type targetAnchorPoint,
            UI_Vertical_Anchor_Sort_Type majorSort,
            UI_Horizontal_Anchor_Sort_Type minorSort,
            UI_Anchor_Offset_Type offsetType = UI_Anchor_Offset_Type.Pixel,
            Vector3? offsetVector = null
        )
            : this
            (
                targetAnchorPoint,
                new UI_Anchor_Sort_Style((int)minorSort, (int)majorSort),
                offsetType,
                offsetVector
            )
        {
            
        }
        
        internal UI_Anchor
        (
            UI_Anchor_Position_Type targetAnchorPoint = UI_Anchor_Position_Type.Top_Left,
            UI_Anchor_Sort_Style sortStyle = null,
            UI_Anchor_Offset_Type offsetType = UI_Anchor_Offset_Type.Pixel,
            Vector3? offsetVector = null
        )
        {
            UI_Anchor__Target_Anchor_Point = targetAnchorPoint;
            UI_Anchor__Sort_Style = sortStyle;
            UI_Anchor__Offset_Type__UI_Anchor = offsetType;
            UI_Anchor__Offset_Vector__UI_Anchor = offsetVector ?? Vector3.Zero;
        }

        public static UI_Anchor_Position_Type Clamp_To_Direction__If_Is_Middle
        (
            UI_Anchor_Position_Type positionType,
            UI_Anchor_Sort_Type sortDirection
        )
        {
            if (positionType != UI_Anchor_Position_Type.Middle)
                return positionType;

            switch (sortDirection)
            {
                case UI_Anchor_Sort_Type.Left:
                    return UI_Anchor_Position_Type.Middle_Left;
                case UI_Anchor_Sort_Type.Right:
                    return UI_Anchor_Position_Type.Middle_Right;
                case UI_Anchor_Sort_Type.Top:
                    return UI_Anchor_Position_Type.Top_Middle;
                case UI_Anchor_Sort_Type.Bottom:
                    return UI_Anchor_Position_Type.Bottom_Middle;
                default:
                    throw new InvalidOperationException();
            }
        }
        
        public static UI_Anchor_Position_Type Get__Opposite
        (
            UI_Anchor_Position_Type positionType,
            UI_Anchor_Sort_Type sortDirection
        )
        {
            switch (sortDirection)
            {
                case UI_Anchor_Sort_Type.Left:
                case UI_Anchor_Sort_Type.Right:
                    return Get__Opposite_Horizontal(positionType);
                default:
                    return Get__Opposite_Vertical(positionType);
            }
        }

        private static UI_Anchor_Position_Type Get__Opposite_Horizontal
        (
            UI_Anchor_Position_Type positionType
        )
        {
            int positionType_Int = (int) positionType;

            switch (positionType)
            {
                case UI_Anchor_Position_Type.Top_Left:
                case UI_Anchor_Position_Type.Middle_Left:
                case UI_Anchor_Position_Type.Bottom_Left:
                    return (UI_Anchor_Position_Type) (positionType_Int + 2);
                case UI_Anchor_Position_Type.Top_Right:
                case UI_Anchor_Position_Type.Middle_Right:
                case UI_Anchor_Position_Type.Bottom_Right:
                    return (UI_Anchor_Position_Type) (positionType_Int - 2);
                default:
                    return positionType;
            }
        }

        private static UI_Anchor_Position_Type Get__Opposite_Vertical
        (
            UI_Anchor_Position_Type positionType
        )
        {
            int positionType_Int = (int) positionType;

            switch (positionType)
            {
                case UI_Anchor_Position_Type.Top_Left:
                case UI_Anchor_Position_Type.Top_Middle:
                case UI_Anchor_Position_Type.Top_Right:
                    return (UI_Anchor_Position_Type) (positionType_Int + 6);
                case UI_Anchor_Position_Type.Bottom_Left:
                case UI_Anchor_Position_Type.Bottom_Middle:
                case UI_Anchor_Position_Type.Bottom_Right:
                    return (UI_Anchor_Position_Type) (positionType_Int - 6);
                default:
                    return positionType;
            }
        }
        
        public override string ToString()
        {
            return String.Format
                (
                "Anchor [Mj:{0}, Mi:{1}, T:{2}]",
                Get__Major_Sort_Type__UI_Anchor(),
                Get__Minor_Sort_Type__UI_Anchor(),
                UI_Anchor__Target_Anchor_Point
                );
        }
    }
}