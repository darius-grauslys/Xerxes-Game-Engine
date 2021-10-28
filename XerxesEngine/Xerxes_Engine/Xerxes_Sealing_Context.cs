namespace Xerxes_Engine
{
    public enum Xerxes_Sealing_Context
    {
        /// <summary>
        /// This sealing context forbids new definitions.
        /// </summary>
        Immutable,
        /// <summary>
        /// This sealing context forbids overriding
        /// inherited declarations.
        /// </summary>
        Extending,
        /// <summary>
        /// This sealing context allow for overriding
        /// inherited declarations.
        /// </summary>
        Virtual
    }
}
