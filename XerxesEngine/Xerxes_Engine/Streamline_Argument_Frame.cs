namespace Xerxes_Engine
{
    public class Streamline_Argument_Frame : Streamline_Argument
    {
        public double Streamline_Argument_Frame__DELTA_TIME  { get; }
        public double Streamline_Argument_Frame__SENDER_TIME { get; }
        
        internal Streamline_Argument_Frame
        (
            double deltaTime,
            double senderTime
        )
        {
            Streamline_Argument_Frame__DELTA_TIME = deltaTime;
            Streamline_Argument_Frame__SENDER_TIME = senderTime;
        }
    }
}
