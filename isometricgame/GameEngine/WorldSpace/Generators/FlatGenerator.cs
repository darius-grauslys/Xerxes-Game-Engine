using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Services;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace.Generators
{
    public class FlatGenerator : Generator
    {
        public FlatGenerator(int seed) 
            : base(seed)
        {
        }

        internal override Chunk GetChunk(float[,] noiseMap, Vector2 pos)
        {
            Chunk c = new Chunk(pos);

            for (int x = 0; x < Chunk.CHUNK_TILE_WIDTH; x++)
            {
                for (int y = 0; y < Chunk.CHUNK_TILE_WIDTH; y++)
                {
                    c.Tiles[x, y] = new Tile(0, 0, 0);
                }
            }

            return c;
        }
    }
}
