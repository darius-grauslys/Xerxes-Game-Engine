namespace Xerxes_Engine
{
    public class Streamline_Argument
    {
        public double Streamline_Argument__ELAPSED_TIME { get; }
        public double Streamline_Argument__DELTA_TIME   { get; }
        internal Streamline_Argument
        (
            double elapsedTime,
            double deltaTime
        ) 
        {
            Streamline_Argument__ELAPSED_TIME = elapsedTime;
            Streamline_Argument__DELTA_TIME = deltaTime;
        }

        internal Streamline_Argument
        (
            Streamline_Argument streamline_Argument
        )
        : this
        (
            streamline_Argument.Streamline_Argument__ELAPSED_TIME,
            streamline_Argument.Streamline_Argument__DELTA_TIME
        )
        { }
    }
}
