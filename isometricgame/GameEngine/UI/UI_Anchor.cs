using System;
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
        public UI_Anchor_Position_Type UI_Anchor__Target_Anchor_Point { get; internal set; }

        internal UI_Anchor_Sort_Style UI_Anchor__Sort_Style { get; set; }
        
        public UI_Anchor_Offset_Type UI_Anchor__Offset_Type__UI_Anchor { get; internal set; }
        
        public Vector3 UI_Anchor__Offset_Vector__UI_Anchor { get; internal set; }
        
        public UI_Anchor_Sort_Type Get__Major_Sort_Type__UI_Anchor()
            => UI_Anchor__Sort_Style.UI_Anchor_Style__MAJOR;
        public UI_Anchor_Sort_Type Get__Minor_Sort_Type__UI_Anchor()
            => UI_Anchor__Sort_Style.UI_Anchor_Style__MINOR;
        
        internal UI_Anchor
        (
            UI_Anchor_Position_Type targetAnchorPoint = UI_Anchor_Position_Type.Top_Left,
            UI_Anchor_Sort_Style sortStyle = null
        )
        {
            UI_Anchor__Target_Anchor_Point = targetAnchorPoint;
            UI_Anchor__Sort_Style = sortStyle;
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