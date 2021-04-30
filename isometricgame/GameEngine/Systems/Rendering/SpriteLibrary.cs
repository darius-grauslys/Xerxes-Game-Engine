using isometricgame.GameEngine.Rendering;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Systems.Rendering
{
    public class SpriteLibrary : GameSystem
    {
        internal List<Sprite> sprites = new List<Sprite>();
        private Dictionary<string, int> nameToIndex = new Dictionary<string, int>();

        public SpriteLibrary(Game gameRef) 
            : base(gameRef)
        {
        }

        public override void Unload()
        {
            base.Unload();
        }

        public int RecordSprite(Sprite s)
        {
            nameToIndex.Add(s.Name, sprites.Count);
            sprites.Add(s);
            
            return sprites.Count - 1;
        }

        public VertexArray[] GetArrays(string name) => sprites[nameToIndex[name]].VertexArrays;

        public bool HasSprite(string name) => nameToIndex.ContainsKey(name);
        public int GetSpriteID(string name) => nameToIndex[name];
        public Sprite GetSprite(string name) => sprites[nameToIndex[name]];
        public Sprite GetSprite(int id) => sprites[id];
        public void SetVAO(int id, int vao) => sprites[id].VAO_Index = vao;
        public void SetVAO_Row(int id, int row) => sprites[id].VAO_Row = row;

        public RenderUnit ExtractRenderUnit(string name) => ExtractRenderUnit(nameToIndex[name]);
        public RenderUnit ExtractRenderUnit(int id) => new RenderUnit(id, 0, Vector3.Zero);
    }
}
