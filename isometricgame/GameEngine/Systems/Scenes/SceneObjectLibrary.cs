using isometricgame.GameEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Systems.Scenes
{
    public class SceneObjectLibrary : GameSystem
    {
        private List<SceneObjectCollection> sceneObjectCollections = new List<SceneObjectCollection>();

        public SceneObjectLibrary(Game gameRef) 
            : base(gameRef)
        {
        }

        public void AddSceneObjects(SceneObject[] sceneObjects) => sceneObjectCollections.Add(new SceneObjectCollection(sceneObjects));
        public void AddSceneObject(SceneObject sceneObject) => sceneObjectCollections.Add(new SceneObjectCollection(new SceneObject[] { sceneObject }));
        public SceneObjectCollection GetObjects(int id) => sceneObjectCollections[id];
    }
}
