namespace Xerxes_Engine
{
    public class Streamline_Argument_Resize_2D : Streamline_Argument
    {
        public float Streamline_Argument_Resize_2D__WIDTH  { get; }
        public float Streamline_Argument_Resize_2D__HEIGHT { get; }

        internal Streamline_Argument_Resize_2D
        (
            double elapsedTime,
            double deltaTime,
            float width,
            float height
        )
        : base
        (
            elapsedTime,
            deltaTime
        )
        {
            Streamline_Argument_Resize_2D__WIDTH  = width;
            Streamline_Argument_Resize_2D__HEIGHT = height;
        }
    }
}
