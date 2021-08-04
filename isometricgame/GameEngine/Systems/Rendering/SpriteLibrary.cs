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
        internal readonly List<Sprite> sprites = new List<Sprite>();
        private readonly Dictionary<string, int> nameToIndex = new Dictionary<string, int>();

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

        public VertexArray[] GetArrays(string name) => sprites[(int)nameToIndex[name]].VertexArrays;

        public bool HasSprite(string name) => nameToIndex.ContainsKey(name);
        public uint GetSpriteID(string name) => (uint)nameToIndex[name];
        public Sprite GetSprite(string name) => sprites[name != null ? nameToIndex[name] : 0];
        public Sprite GetSprite(int id) => sprites[id];
        public void SetVAO(int id, uint vao) => sprites[id].VAO_Index = vao;
        public void SetVAO_Row(int id, uint row) => sprites[id].VAO_Row = row;

        public RenderUnit ExtractRenderUnit(string name) => ExtractRenderUnit((uint)nameToIndex[name]);
        public RenderUnit ExtractRenderUnit(uint id) => new RenderUnit(id, 0, Vector3.Zero);
    }
}
