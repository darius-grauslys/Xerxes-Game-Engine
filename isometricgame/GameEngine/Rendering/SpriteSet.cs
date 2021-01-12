using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Rendering
{
    public class SpriteSet
    {
        protected List<Sprite> sprites;
        
        public Sprite GetSprite(int i)
        {
            return sprites[i];
        }

        public Sprite[] GetSprites()
        {
            return sprites.ToArray();
        }

        public SpriteSet(Sprite[] sprites)
        {
            if (sprites == null)
                return;
            this.sprites = new List<Sprite>(sprites);
        }
    }
}
