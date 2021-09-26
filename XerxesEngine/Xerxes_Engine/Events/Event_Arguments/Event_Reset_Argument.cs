namespace Xerxes_Engine.Events.Streamline_Arguments
{
    public class Event_Reset_Argument : Streamline_Argument
    {
        public double Event_Reset_Argument__NEW_TIME_LIMIT { get; }

        internal Event_Reset_Argument
        (
            double newTimeLimit
        )
        {
            Event_Reset_Argument__NEW_TIME_LIMIT = newTimeLimit;
        }
    }
}
