using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Rendering
{
    public class TileSpriteSet : SpriteSet
    {
        public TileSpriteSet(Sprite[] sprites) 
            : base(sprites)
        {
        }

        public TileSpriteSet(Texture2D[] texts)
            : base(new Sprite[0])
        {
            Sprite[] sprites = new Sprite[texts.Length];
            for (int i = 0; i < texts.Length; i++)
                sprites[i] = new Sprite(texts[i]);

            sprites[2].Flip(true);
            sprites[3].Flip(true);
            sprites[6].Flip(true);
            //sprites[4].Flip(true);
            //sprites[5].Flip(true);
            sprites[7].Flip(true);

            this.sprites = new List<Sprite>(sprites);
        }

        public Sprite GetTile(int i)
        {
            return GetSprite(i);
        }
    }
}
