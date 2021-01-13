using isometricgame.GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services
{
    public class SpriteLibrary : GameSystem
    {
        private List<Sprite> spriteByID = new List<Sprite>();
        private List<SpriteSet> spriteSetByID = new List<SpriteSet>();
        
        private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        private Dictionary<string, SpriteSet> spriteSets = new Dictionary<string, SpriteSet>();

        public SpriteLibrary(Game game) 
            : base(game)
        {
        }

        public Sprite GetSprite(string spriteName)
        {
            return sprites[spriteName];
        }

        public Sprite GetSprite(int id)
        {
            return spriteByID[id];
        }

        public void RecordSprite(string spriteName, Sprite s)
        {
            Console.WriteLine("Recorded sprite: \n\tname: {0}\n\tid: {1}", spriteName, s.Texture.ID);
            if (sprites.ContainsKey(spriteName))
            {
                Sprite oldS = sprites[spriteName];
                int index = spriteByID.FindIndex((s1) => s1.Texture.ID == oldS.Texture.ID);
                spriteByID[index] = s;
                sprites[spriteName] = s;
            }
            else
            {
                sprites.Add(spriteName, s);
                spriteByID.Add(s);
            }
        }

        public T GetSpriteSet<T>(string spriteSetName) where T : SpriteSet
        {
            return spriteSets[spriteSetName] as T;
        }

        public T GetSpriteSet<T>(int id) where T : SpriteSet
        {
            return spriteSetByID[id] as T;
        }

        public void RecordSpriteSet<T>(string spriteSetName, T ss, Func<int, string> namingScheme) where T : SpriteSet
        {
            spriteSets.Add(spriteSetName, ss);
            spriteSetByID.Add(ss);

            Sprite[] sprites = ss.GetSprites();

            for (int i = 0; i < sprites.Length; i++)
            {
                RecordSprite(namingScheme(i), sprites[i]);
            }
        }
    }
}
