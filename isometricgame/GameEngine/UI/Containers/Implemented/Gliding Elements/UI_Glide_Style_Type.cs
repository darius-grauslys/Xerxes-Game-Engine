namespace isometricgame.GameEngine.UI.Containers.Implemented.Gliding_Elements
{
    public enum UI_Glide_Style_Type
    {
        /// <summary>
        /// Any percentage, will be clamped between 0% and 100%.
        /// </summary>
        Clamped,
        /// <summary>
        /// Any percentage will be absolute valued and modulo'd.
        /// </summary>
        Wraps_Forward,
        /// <summary>
        /// Percentage fed into: [1 - abs(n)(-1)^n] as n.
        /// </summary>
        Wraps_With_Bounce
    }
}