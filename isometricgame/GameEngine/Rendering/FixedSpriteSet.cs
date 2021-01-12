using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Rendering
{
    /// <summary>
    /// This is a sprite set where all textures are the same dimension.
    /// </summary>
    public class FixedSpriteSet : SpriteSet
    {
        private float textureWidth, textureHeight;

        public float TextureWidth { get => textureWidth; private set => textureWidth = value; }
        public float TextureHeight { get => textureHeight; private set => textureHeight = value; }

        public FixedSpriteSet(Sprite[] sprites) 
            : base(sprites)
        {
            Vector2 dim = sprites[0].Vertices[2].TextCoord - sprites[0].Vertices[0].TextCoord;
            textureWidth = dim.X * sprites[0].Texture.Width;
            textureHeight = dim.Y * sprites[0].Texture.Height;
        }
    }
}
