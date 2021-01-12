using isometricgame.GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Attributes.Rendering
{
    public class SpriteAttribute : GameAttribute
    {
        private Sprite sprite;
        
        public SpriteAttribute(GameObject parentObject) 
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
