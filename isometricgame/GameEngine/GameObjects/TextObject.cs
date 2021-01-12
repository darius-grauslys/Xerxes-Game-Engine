using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Scenes;
using OpenTK;

namespace isometricgame.GameEngine.GameObjects
{
    public class TextObject : GameObject
    {
        public TextObject(Scene scene, Vector3 position) 
            : base(scene, position)
        {
        }
    }
}
