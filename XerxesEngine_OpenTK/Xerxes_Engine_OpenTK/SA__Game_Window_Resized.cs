namespace Xerxes.Xerxes_OpenTK
{
    public class SA__Game_Window_Resized : Streamline_Argument
    {
        public float SA__Resize_2D__WIDTH  { get; }
        public float SA__Resize_2D__HEIGHT { get; }

        internal SA__Game_Window_Resized
        (
            double elapsedTime,
            double deltaTime,
            float width,
            float height
        )
        {
            SA__Resize_2D__WIDTH  = width;
            SA__Resize_2D__HEIGHT = height;
        }
    }
}
