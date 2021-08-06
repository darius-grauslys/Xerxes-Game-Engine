using isometricgame.GameEngine.Tools;
using OpenTK;

namespace isometricgame.GameEngine.UI
{
    public class UI_Anchor_Sort_Style
    {
        public UI_Anchor_Sort_Type UI_Anchor_Style__MAJOR { get; internal set; }
        public UI_Anchor_Sort_Type UI_Anchor_Style__MINOR { get; internal set; }
        
        public UI_Anchor_Sort_Style()
            : this
            (
                UI_Anchor_Sort_Type.Left
            )
        {
            
        }
        
        public UI_Anchor_Sort_Style
        (
            UI_Horizontal_Anchor_Sort_Type majorOnly
        )
            : this
            (
                Internalize__External_Sort_Type((int) majorOnly)
            )
        {
            
        }
        
        public UI_Anchor_Sort_Style
        (
            UI_Vertical_Anchor_Sort_Type majorOnly
        )
            : this
                (
                Internalize__External_Sort_Type((int) majorOnly)
                )
        {
            
        }
        
        public UI_Anchor_Sort_Style
        (
            UI_Horizontal_Anchor_Sort_Type major,
            UI_Vertical_Anchor_Sort_Type minor
        )
        : this
            (
            Internalize__External_Sort_Type((int) major),
            Internalize__External_Sort_Type((int) minor)
            )
        {
            
        }
        
        public UI_Anchor_Sort_Style
        (
            UI_Vertical_Anchor_Sort_Type major,
            UI_Horizontal_Anchor_Sort_Type minor
        )
            : this
            (
                Internalize__External_Sort_Type((int) major),
                Internalize__External_Sort_Type((int) minor)
            )
        {
            
        }
        
        internal UI_Anchor_Sort_Style
        (
            UI_Anchor_Sort_Type major = UI_Anchor_Sort_Type.Right, 
            UI_Anchor_Sort_Type minor = UI_Anchor_Sort_Type.Bottom
        )
        {
            UI_Anchor_Style__MAJOR = major;
            UI_Anchor_Style__MINOR = minor;
        }

        private static UI_Anchor_Sort_Type Internalize__External_Sort_Type(int value)
            => (UI_Anchor_Sort_Type) value;
    }
}