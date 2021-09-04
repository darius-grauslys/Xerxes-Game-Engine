using XerxesEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XerxesEngine.Systems
{
    public class SceneManagementService : GameSystem
    {
        private Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

        public SceneManagementService(Game game) 
            : base(game)
        {
        }

        public void AddScene(string name, Scene scene) => scenes.Add(name, scene);
        public Scene GetScene(string name) => scenes[name];
        public void SetScene(string name) { scenes[name].GainFocus(); Game.Internal_Set__Scene__Game(scenes[name]); }
    }
}
