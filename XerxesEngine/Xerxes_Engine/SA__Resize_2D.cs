namespace Xerxes_Engine
{
    public class SA__Resize_2D : Streamline_Argument
    {
        public float SA__Resize_2D__WIDTH  { get; }
        public float SA__Resize_2D__HEIGHT { get; }

        internal SA__Resize_2D
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
            SA__Resize_2D__WIDTH  = width;
            SA__Resize_2D__HEIGHT = height;
        }
    }
}
