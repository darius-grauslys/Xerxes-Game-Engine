namespace isometricgame.GameEngine.UI
{
    public enum UI_Anchor_Padding_Type
    {
        /// <summary>
        /// Buffers of this type cannot be negative.
        /// Elements with this buffer type are forced to fit in it's panel.
        /// If it can't, it is removed from the panel.
        /// </summary>
        Constrained__Pixel = 1,
        /// <summary>
        /// Buffers of this type cannot be negative.
        /// Elements with this buffer type are forced to fit in it's panel.
        /// If it can't, it is removed from the panel.
        /// </summary>
        Constrained__Percentage = 2,
        
        /// <summary>
        /// Buffers of this type can be negative, and allow it's element to sort off of its parent panel.
        /// </summary>
        Unbound__Pixel = -1,
        /// <summary>
        /// Buffers of this type can be negative, and allow it's element to sort off of its parent panel.
        /// </summary>
        Unbound__Percentage = -2
    }
}