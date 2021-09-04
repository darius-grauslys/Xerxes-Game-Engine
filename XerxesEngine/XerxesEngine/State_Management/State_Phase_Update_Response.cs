namespace XerxesEngine.State_Management
{
    public enum State_Phase_Update_Response
    {
        /// <summary>
        /// Continue to reside on this state.
        /// </summary>
        Idle,
        
        /// <summary>
        /// Put State Machine into panic.
        /// </summary>
        Break,
        
        /// <summary>
        /// Progress into leading State.
        /// </summary>
        Progress
    }
}