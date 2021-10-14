namespace Xerxes_Engine
{
    public enum Streamline_Divert_Type
    {
        /// <summary>
        /// This streamline does not divert or
        /// stop the flow in anyway.
        /// </summary>
        Streams,
        /// <summary>
        /// This streamline is capable of diverting
        /// the flow, and possibly droughting 
        /// descendants.
        Diverts,
        /// <summary>
        /// This streamline ends here. Connections
        /// to it will fail.
        /// </summary>
        Seals
    }
}
