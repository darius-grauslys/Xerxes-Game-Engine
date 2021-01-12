using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace isometricgame.GameEngine.WorldSpace.Generators
{
    public class TestGenerator : Generator
    {

        private FlatGenerator flat;

        private Tile[,] testChunk = new Tile[,]
        {
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {                                             //3,3//        //3,4//     //3,5//
                new Tile(0),new Tile(0),new Tile(0),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {                                 //4,2//      //4,3//      //4,4//     //4,5//     //4,6//
                new Tile(0),new Tile(0),new Tile(-1),new Tile(-1),new Tile(-2),new Tile(-1),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {                                                        //5,4//
                new Tile(0),new Tile(0),new Tile(-1),new Tile(0),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(-1),new Tile(0),new Tile(0),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-2),new Tile(-2),new Tile(-2),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(-1),new Tile(-1),new Tile(-2),new Tile(-2),new Tile(-3),new Tile(-2),new Tile(-2),new Tile(-1),new Tile(-1),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-2),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(-1),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
            {
                new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),new Tile(0),
            },
        };


        public TestGenerator(int seed) 
            : base(seed)
        {
            flat = new FlatGenerator(seed);
        }

        internal override Chunk GetChunk(float[,] noiseMap, Vector2 pos)
        {
            Chunk c = new Chunk(pos);

            for (int x = 0; x < Chunk.CHUNK_TILE_WIDTH; x++)
            {
                for (int y = 0; y < Chunk.CHUNK_TILE_WIDTH; y++)
                {
                    c.Tiles[x, y] = new Tile((int)noiseMap[x, y], 0, 0);
                }
            }

            return c;
        }
    }
}
