namespace Xerxes_Engine
{
    public class Event_Argument_Frame : Event_Argument
    {
        public double Event_Argument_Frame__DELTA_TIME  { get; }
        public double Event_Argument_Frame__SENDER_TIME { get; }
        
        internal Event_Argument_Frame
        (
            double deltaTime,
            double senderTime
        )
        {
            Event_Argument_Frame__DELTA_TIME = deltaTime;
            Event_Argument_Frame__SENDER_TIME = senderTime;
        }
    }
}
