using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.WorldSpace.ChunkSpace;
using OpenTK;

namespace isometricgame.GameEngine.WorldSpace.Generators
{
    public class GenericGenerator : Generator
    {
        public GenericGenerator(int seed) 
            : base(seed)
        {
        }

        public override Chunk GetChunk(Vector2 pos)
        {
            Chunk c = new Chunk(pos);
            float[,] noiseMap = Perlin.InterprolateNoise(pos);

            for (int x = 0; x < Chunk.CHUNK_TILE_WIDTH; x++)
            {
                for (int y = 0; y < Chunk.CHUNK_TILE_WIDTH; y++)
                {
                    c.Tiles[x, y] = new Tile((int)noiseMap[x, y], 0, 0);
                    float z = c.Tiles[x, y].Z;
                    if (z > 3)
                        c.Tiles[x, y].Data = 1;
                }
            }

            return c;
        }
    }
}
