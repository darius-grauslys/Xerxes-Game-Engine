namespace Xerxes_Engine.Export_OpenTK.Engine_Objects
{
    public class Scene : 
        Xerxes_Object<Scene>
    {
        public Game Game__REFERENCE { get; private set; }
        public float Scene__Width   { get; private set; }
        public float Scene__Height  { get; private set; }
        
        private Scene_Layer_Dictionary _Scene__LAYER_DICTIONARY { get; }

        public Scene()
        {
            Declare__Ancestor<Game>();
            Declare__Descendant<Scene_Layer>();
            Declare__Descendant<Camera>();

            Declare__Streams()
                .Downstream.Receiving<SA__Game_Window_Resized>
                (
                    Private_Handle__2D_Resize__Scene
                );

            _Scene__LAYER_DICTIONARY = new Scene_Layer_Dictionary();
        }

        private void Private_Handle__2D_Resize__Scene(SA__Game_Window_Resized e)
        {
            Scene__Width  = e.SA__Resize_2D__WIDTH;
            Scene__Height = e.SA__Resize_2D__HEIGHT;
        }
    }
}
