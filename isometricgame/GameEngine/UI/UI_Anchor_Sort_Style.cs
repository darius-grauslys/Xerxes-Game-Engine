using isometricgame.GameEngine.Tools;
using OpenTK;

namespace isometricgame.GameEngine.UI
{
    public class UI_Anchor_Sort_Style
    {
        public UI_Anchor_Sort_Type UI_Anchor_Style__MAJOR { get; internal set; }
        public UI_Anchor_Sort_Type UI_Anchor_Style__MINOR { get; internal set; }

        internal UI_Anchor_Sort_Style
        (
            UI_Anchor_Sort_Type major,
            UI_Anchor_Sort_Type minor
        )
        {
            UI_Anchor_Style__MAJOR = major;
            UI_Anchor_Style__MINOR = minor;
        }
        
        internal UI_Anchor_Sort_Style
        (
            int major = 2,
            int minor = 4
        )
        : this
            (
            Internalize__External_Sort_Type(major),
            Internalize__External_Sort_Type(minor)
            )
        {
        }
        

        private static UI_Anchor_Sort_Type Internalize__External_Sort_Type(int value)
            => (UI_Anchor_Sort_Type) value;
    }
}