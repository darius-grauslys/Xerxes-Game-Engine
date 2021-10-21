namespace Xerxes_Engine
{
    public class Streamline_Argument
    {
        public double SA___ELAPSED_TIME { get; }
        public double SA___DELTA_TIME   { get; }
        internal Streamline_Argument
        (
            double elapsedTime,
            double deltaTime
        ) 
        {
            SA___ELAPSED_TIME = elapsedTime;
            SA___DELTA_TIME = deltaTime;
        }

        public Streamline_Argument
        (
            Streamline_Argument streamline_Argument
        )
        : this
        (
            streamline_Argument.SA___ELAPSED_TIME,
            streamline_Argument.SA___DELTA_TIME
        )
        { }
    }
}
