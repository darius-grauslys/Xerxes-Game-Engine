using isometricgame.GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using isometricgame.GameEngine.Services;
using System.Drawing.Imaging;

namespace isometricgame.GameEngine.WorldSpace.Generators
{
    /// <summary>
    /// This is used for generating the chunk's sprite. It can also be used to update it as well.
    /// </summary>
    public static class ChunkSpriteFabricator
    {
        /*
        public Sprite GenerateChunkSprite(Chunk c, SpriteLibrary spriteLibrary)
        {
            if (c.SpriteHasBeenCreated)
                return c.ChunkSprite;

            //Bitmap bmp = new Bitmap(Tile.TILE_WIDTH * Chunk.CHUNK_TILE_WIDTH / 2, Tile.TILE_HEIGHT * (Chunk.CHUNK_TILE_HEIGHT + (c.MinimumZ - c.MaximumZ)) / 2);
            DirectBitmap dbmp = new DirectBitmap(Tile.TILE_WIDTH * Chunk.CHUNK_TILE_WIDTH / 2, Tile.TILE_HEIGHT * (Chunk.CHUNK_TILE_HEIGHT + (c.MinimumZ - c.MaximumZ)) / 2);

            for (int y = 0; y < Chunk.CHUNK_TILE_HEIGHT; y++)
            {
                for (int x = 0; x < Chunk.CHUNK_TILE_WIDTH; x++)
                {
                    int cx = (int)Chunk.CartesianToIsometric_X(x, y), cy = (int)Chunk.CartesianToIsometric_Y(x, y, c.Tiles[x, y].Z);

                    Tile t = c.Tiles[x, y];

                    Sprite s = spriteLibrary.GetSpriteSet<TileSpriteSet>(t.Data).GetTile(t.Orientation);

                    IntPtr pixels = new IntPtr();
                    GL.GetTextureImage(s.Texture.ID, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.Bitmap, Tile.TILE_AREA, pixels);
                    
                }
            }

            return chunkSprite;
        }

        public Sprite UpdateSprite(Chunk c, Tile t, int x, int y)
        {

        }
        */
    }
}
