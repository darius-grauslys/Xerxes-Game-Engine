namespace isometricgame.GameEngine.UI
{
    public class UI_Vertical_Anchor: UI_Anchor
    {
        public UI_Vertical_Anchor
        (
            UI_Vertical_Anchor_Sort_Type anchorSortType = UI_Vertical_Anchor_Sort_Type.Top, 
            float anchorPadding = 0, 
            UI_Anchor_Padding_Type anchorPaddingType = UI_Anchor_Padding_Type.Constrained__Pixel
        )
            : base 
            (
                UI_Anchor.Get__Internal_Sort_Type(anchorSortType),
                anchorPadding,
                anchorPaddingType
            )
        { }
    }
}