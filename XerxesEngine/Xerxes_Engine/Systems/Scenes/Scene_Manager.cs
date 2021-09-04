using System.Collections.Generic;

namespace Xerxes_Engine.Systems.Scenes
{
    public class Scene_Manager : Game_System
    {
        private Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

        public Scene_Manager(Game game) 
            : base(game)
        {
        }

        public void AddScene(string name, Scene scene) => scenes.Add(name, scene);
        public Scene GetScene(string name) => scenes[name];
        public void SetScene(string name) { scenes[name].GainFocus(); Game.Internal_Set__Scene__Game(scenes[name]); }
    }
}
