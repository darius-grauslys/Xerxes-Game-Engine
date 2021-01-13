using isometricgame.GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Components.Rendering
{
    public class SpriteComponent : GameComponent
    {
        private Sprite sprite;
        
        public SpriteComponent(GameObject parentObject) 
            : base(parentObject)
        {
        }

        public virtual Sprite[] GetSprites()
        {
            return new Sprite[] { sprite };
        }

        /// <summary>
        /// might need to make this thread safe.
        /// </summary>
        /// <param name="s"></param>
        public void SetSprite(Sprite s)
        {
            sprite = s;
        }
    }
}
