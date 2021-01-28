using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Systems.Scenes
{
    public class SceneObjectCollection
    {
        private SceneObject[] collection;
        public SceneObject[] Collection => collection;

        public SceneObjectCollection(SceneObject[] collection) => this.collection = collection;
    }
}
