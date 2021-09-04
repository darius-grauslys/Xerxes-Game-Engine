namespace XerxesEngine.UI.Containers.Implemented_UI_Containers.Gliding_Elements
{
    public enum UI_Glide_Type
    {
        /// <summary>
        /// Performs modulo on overflow percentage.
        /// </summary>
        Clamped,
        /// <summary>
        /// Same as Clamped, but considers the distance between the last and first node as a path.
        /// </summary>
        Wrapped,
        /// <summary>
        /// Takes the product of the modulo and "(-1)^N" where N is the divisor of the percentage.
        /// </summary>
        Bounced
    }
}
