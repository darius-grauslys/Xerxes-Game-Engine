namespace Xerxes_Engine.Engine_Objects
{
    public class Scene : Xerxes_Engine_Container 
    {
        public Game Game__REFERENCE { get; private set; }
        
        private Scene_Layer_Dictionary _Scene__LAYER_DICTIONARY { get; }

        protected Scene_Layer_Handle Protected_Associate_Descendant__Layer__Scene
        (
            Scene_Layer layer, 
            string layerAlias = null
        ) 
        { 
            bool associated = Xerxes_Engine_Container.Internal_Associate__Containers(layer, this);

            if (!associated)
                return null; //Need to return default.

            Scene_Layer_Handle layerHandle = 
                _Scene__LAYER_DICTIONARY.Internal_Declare__Layer__Scene_Layer_Dictionary
            (
                layerAlias ?? layer.ToString(),
                layer
            );

            Event_Argument_Resize_2D resize_2D_Argument =
                new Event_Argument_Resize_2D
                (
                    Game__REFERENCE.Game__Window_Width,
                    Game__REFERENCE.Game__Window_Height
                );

            layer.Internal_Resize__2D__Xerxes_Engine_Container
            (
                resize_2D_Argument
            );

            return layerHandle;
        }

        protected void Protected_Add__Layers__Scene(params Scene_Layer[] layers) 
        { 
            foreach (Scene_Layer layer in layers) 
                Protected_Associate_Descendant__Layer__Scene(layer); 
        }

        public Scene(Game game)
            : base (Xerxes_Engine_Object_Association_Type.GAME__SCENE)
        {
            Game__REFERENCE = game;
            _Scene__LAYER_DICTIONARY = new Scene_Layer_Dictionary();
        }
    }
}
