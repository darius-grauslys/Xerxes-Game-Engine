namespace Xerxes_Engine.Systems.Scenes
{
    public sealed class Scene_Manager : Xerxes_Export 
    {
        /*
        private const string _Scene_Manager__ERROR_SCENE_NAME = "error";


        private Scene_Dictionary _Scene_Manager__SCENE_DICTIONARY { get; }
        private Scene _Scene_Manager__ERROR_SCENE { get; }

        public Scene_Handle Scene_Manager__ERROR_SCENE_HANDLE { get; }

        */

        internal Scene_Manager(Game game) 
        {
            /*
            _Scene_Manager__SCENE_DICTIONARY = new Scene_Dictionary();
            _Scene_Manager__ERROR_SCENE = new Scene(game);

            Scene_Handle errorHandle =
                _Scene_Manager__SCENE_DICTIONARY
                    .Internal_Add__Scene__Scene_Dictionary
                    (
                        _Scene_Manager__ERROR_SCENE_NAME,
                        _Scene_Manager__ERROR_SCENE
                    );

            Scene_Manager__ERROR_SCENE_HANDLE = errorHandle;

            Game.Internal_Set__Scene__Game(_Scene_Manager__ERROR_SCENE);
            */
        }

        /*

        public Scene_Handle Add__Scene__Scene_Manager(Scene scene, string name=null) 
        {
            name = name ?? scene?.ToString();
            if(scene == null)
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__System,

                    Log.ERROR__SCENE_MANAGER__CANNOT_ADD_NULL_SCENE_1,
                    this,
                    name
                );
                return Scene_Manager__ERROR_SCENE_HANDLE;
            }

            return _Scene_Manager__SCENE_DICTIONARY
                .Internal_Add__Scene__Scene_Dictionary
                (
                    name, scene
                );
        }

        public Scene Get__Scene__Scene_Manager(Scene_Handle sceneHandle)
            => 
            _Scene_Manager__SCENE_DICTIONARY
            .Internal_Get__Scene__Scene_Dictionary
            (
                sceneHandle
            );

        public Scene Get__Scene__Scene_Manager(string name)
        {
            Log.Write__Verbose__Log
            (
                Log.VERBOSE__SCENE_MANAGER__LOUSY_LOOKUP_1,
                this,
                name
            );

            Scene returningScene = 
                _Scene_Manager__SCENE_DICTIONARY
                .Internal_Get__Scene__Scene_Dictionary
                (
                    name
                );

            if (returningScene == null)
            {
                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__SCENE_MANAGER__LOUSY_LOOKUP_FAILED_1,
                    this,
                    name
                );
                return _Scene_Manager__ERROR_SCENE;
            }

            return returningScene;
        }

        public void Set__Scene__Scene_Manager(string name) 
        { 
            Scene scene = Get__Scene__Scene_Manager(name);

            if(scene == null)
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__System,

                    Log.ERROR__SCENE_MANAGER__SWITCHED_TO_NULL_SCENE_1,
                    this,
                    name
                );
                return;
            }

            Game.Internal_Set__Scene__Game(scene);
        }

        public void Set__Scene__Scene_Manager(Scene_Handle sceneHandle)
        {
            Scene scene = Get__Scene__Scene_Manager(sceneHandle);

            Game.Internal_Set__Scene__Game(scene);
        }
    
        */
    }
}
