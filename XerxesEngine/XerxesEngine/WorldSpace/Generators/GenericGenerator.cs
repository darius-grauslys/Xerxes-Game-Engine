using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XerxesEngine.Rendering;
using XerxesEngine.Scenes;
using XerxesEngine.WorldSpace.ChunkSpace;
using OpenTK;

namespace XerxesEngine.WorldSpace.Generators
{
    public class GenericGenerator : Generator
    {
        public GenericGenerator(int seed) 
            : base(seed)
        {
        }

        public override Chunk CreateChunk(Vector2 pos)
        {
            Chunk c = new Chunk(pos);
            float[,] noiseMap = Perlin.InterprolateNoise(pos);

            RenderStructure ground = new RenderStructure(Chunk.CHUNK_TILE_WIDTH, Chunk.CHUNK_TILE_HEIGHT);

            for (int x = 0; x < Chunk.CHUNK_TILE_WIDTH; x++)
            {
                for (int y = 0; y < Chunk.CHUNK_TILE_WIDTH; y++)
                {
                    int z = (int)noiseMap[x, y];
                    int id = (z > 3) ? 1 : 0;
                    ground.structuralUnits[x][y] = new RenderUnit((uint)id, 0, new Vector3(x, y, z));
                }
            }

            c.AddStructure(ground);

            return c;
        }

        public override void FinalizeChunk(ChunkDirectory chunkDirectory, ref Chunk c)
        {
            chunkDirectory.PerformTileOrientation(c.TileSpaceLocation, ref c.ChunkStructures[0]);
        }
    }
}
