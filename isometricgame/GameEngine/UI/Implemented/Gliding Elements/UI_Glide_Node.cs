using isometricgame.GameEngine.Tools;

namespace isometricgame.GameEngine.UI.Implemented.Gliding_Elements
{
    public sealed class UI_Glide_Node : UI_Element
    {
        public UI_Glide_Node
        (
            UI_Rect boundingRect,
            
            UI_Horizontal_Anchor majorAnchor,
            UI_Vertical_Anchor lesserAnchor
        )
            : base 
            (
                boundingRect,
                
                majorAnchor,
                lesserAnchor
            )
        {}
        
        public UI_Glide_Node
        (
            UI_Rect boundingRect,
            
            UI_Vertical_Anchor majorAnchor,
            UI_Horizontal_Anchor lesserAnchor
        )
            : base 
            (
                boundingRect,
                
                majorAnchor,
                lesserAnchor
            )
        {}
    }
}