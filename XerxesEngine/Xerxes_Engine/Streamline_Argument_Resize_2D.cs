namespace Xerxes_Engine
{
    public class Streamline_Argument_Resize_2D : Streamline_Argument
    {
        public float Streamline_Argument_Resize_2D__WIDTH  { get; }
        public float Streamline_Argument_Resize_2D__HEIGHT { get; }

        internal Streamline_Argument_Resize_2D
        (
            float width,
            float height
        )
        {
            Streamline_Argument_Resize_2D__WIDTH  = width;
            Streamline_Argument_Resize_2D__HEIGHT = height;
        }
    }
}
