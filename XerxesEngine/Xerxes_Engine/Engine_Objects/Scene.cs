namespace Xerxes_Engine.Engine_Objects
{
    public class Scene : 
        Xerxes_Object<Scene>,
        IXerxes_Descendant_Of<Game>,
        IXerxes_Ancestor_Of<Scene_Layer>,
        IXerxes_Ancestor_Of<Camera>
    {
        public Game Game__REFERENCE { get; private set; }
        public float Scene__Width   { get; private set; }
        public float Scene__Height  { get; private set; }
        
        private Scene_Layer_Dictionary _Scene__LAYER_DICTIONARY { get; }

        public Scene()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Resize_2D>
                (
                    Private_Handle__2D_Resize__Scene
                );

            _Scene__LAYER_DICTIONARY = new Scene_Layer_Dictionary();
        }

        private void Private_Handle__2D_Resize__Scene(SA__Resize_2D e)
        {
            Scene__Width  = e.SA__Resize_2D__WIDTH;
            Scene__Height = e.SA__Resize_2D__HEIGHT;
        }
    }
}
