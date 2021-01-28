using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Components.Rendering
{
    public class SpriteComponent : GameComponent
    {
        protected Sprite sprite;

        public Sprite Sprite => sprite;

        public SpriteComponent(SceneObject parentObject) 
            : base(parentObject)
        {
        }

        /// <summary>
        /// might need to make this thread safe.
        /// </summary>
        /// <param name="s"></param>
        public virtual void SetSprite(Sprite s)
        {
            sprite = s;
        }
    }
}
