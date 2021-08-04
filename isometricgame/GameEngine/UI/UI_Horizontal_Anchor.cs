namespace isometricgame.GameEngine.UI
{
    public class UI_Horizontal_Anchor : UI_Anchor
    {
        public UI_Horizontal_Anchor
        (
            UI_Horizontal_Anchor_Sort_Type anchorSortType = UI_Horizontal_Anchor_Sort_Type.Left, 
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