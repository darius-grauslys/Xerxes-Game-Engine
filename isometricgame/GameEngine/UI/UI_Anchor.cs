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
        public UI_Anchor_Position_Type UI_Anchor__POSITION_TYPE { get; }

        private UI_Anchor_Sort_Style UiAnchorSortStyle { get; }
        
        public UI_Anchor_Sort_Type Get__Major_Sort_Type__UI_Anchor()
            => UiAnchorSortStyle.UI_Anchor_Style__MAJOR;
        public UI_Anchor_Sort_Type Get__Minor_Sort_Type__UI_Anchor()
            => UiAnchorSortStyle.UI_Anchor_Style__MINOR;
        
        internal UI_Anchor
        (
            UI_Anchor_Position_Type positionType,
            UI_Anchor_Sort_Style sortStyle
        )
        {
            UI_Anchor__POSITION_TYPE = positionType;
            UiAnchorSortStyle = sortStyle;
        }
    }
}