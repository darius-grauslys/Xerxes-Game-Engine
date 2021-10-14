namespace Xerxes_Engine
{
    public enum Streamline_Flow_Type
    {
        /// <summary>
        /// A streamline with this flow type will
        /// not accept invocations.
        /// </summary>
        Stream,
        /// <summary>
        /// A streamline with this flow type will
        /// be invokable.
        /// </summary>
        Sources,
        /// <summary>
        /// A streamline with this flow type will
        /// not associate with objects that have
        /// sources on their streamline.
        Uniquely_Sources
    }
}
