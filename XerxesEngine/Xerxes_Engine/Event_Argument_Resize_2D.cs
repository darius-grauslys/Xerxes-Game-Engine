namespace Xerxes_Engine
{
    public class Event_Argument_Resize_2D : Event_Argument
    {
        public float Event_Argument_Resize_2D__WIDTH  { get; }
        public float Event_Argument_Resize_2D__HEIGHT { get; }

        internal Event_Argument_Resize_2D
        (
            float width,
            float height
        )
        {
            Event_Argument_Resize_2D__WIDTH  = width;
            Event_Argument_Resize_2D__HEIGHT = height;
        }
    }
}
