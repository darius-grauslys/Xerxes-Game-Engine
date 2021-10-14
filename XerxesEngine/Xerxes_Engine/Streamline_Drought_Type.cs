namespace Xerxes_Engine
{
    public enum Streamline_Drought_Type
    {
        /// <summary>
        /// Logs, verbosely, or warning about
        /// being linked to an inconsistent streamline.
        /// </summary>
        Log,
        /// <summary>
        /// If linked to an inconsistent streamline
        /// the object with this streamline will
        /// disable on linkage.
        /// </summary>
        Disable,
        /// <summary>
        /// Disallow linkages to inconsistent streamlines.
        /// Thereby failing in linkage, disabling, and
        /// failing on associations.
        /// </summary>
        Dissassociate
    }
}
