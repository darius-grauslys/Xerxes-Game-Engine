using isometricgame.GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Systems.Rendering
{
    public class SpriteLibrary : GameSystem
    {
        private Dictionary<string, Sprite> spritesByKey = new Dictionary<string, Sprite>();

        public SpriteLibrary(Game gameRef) 
            : base(gameRef)
        {
        }

        public override void Unload()
        {
            base.Unload();
        }

        public void RecordSprite(Sprite s) => spritesByKey.Add(s.Name, s);

        public Sprite GetSprite(string name) => spritesByKey[name];

        public Sprite GetSprite(int id) => spritesByKey.Values.ElementAt(id);
    }
}
