namespace Xerxes.Xerxes_OpenTK.Engine_Objects
{
    public class Scene : 
        Xerxes_Object<Scene>
    {
        public Game Game__REFERENCE { get; private set; }
        public float Scene__Width   { get; private set; }
        public float Scene__Height  { get; private set; }

        public Scene()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Game_Window_Resized>
                (
                    Private_Handle__2D_Resize__Scene
                );
        }

        private void Private_Handle__2D_Resize__Scene(SA__Game_Window_Resized e)
        {
            Scene__Width  = e.SA__Resize_2D__WIDTH;
            Scene__Height = e.SA__Resize_2D__HEIGHT;
        }
    }
}
