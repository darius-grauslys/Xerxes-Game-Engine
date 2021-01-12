using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace.Generators.PerlinNoise
{
    public class Perlin
    {
        private int seed;
        private int frequency = 2;

        private static readonly Vector2[] OFFSETS = new Vector2[]
        {
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,1)
        };

        public Perlin(int seed)
        {
            this.seed = seed;
        }

        
        public float[,] InterprolateNoise(Vector2 chunkPosition)
        {
            float[,] ret = new float[Chunk.CHUNK_TILE_WIDTH, Chunk.CHUNK_TILE_WIDTH];
            int stride = Chunk.CHUNK_TILE_WIDTH;

            Vector2 chunkTilePos = chunkPosition * Chunk.CHUNK_TILE_WIDTH;

            Vector2 strideOffset;

            //interprolation values for stride regions.
            float z0, z1, z2, z3;
            //Currently we end up recalucating these A LOT... will work to fix that later on.


            for (int period = 0; period < frequency; period++)
            {


                for (int x_stride = 0; x_stride * stride < Chunk.CHUNK_TILE_WIDTH; x_stride++)
                {
                    for (int y_stride = 0; y_stride * stride < Chunk.CHUNK_TILE_WIDTH; y_stride++)
                    {

                        strideOffset = chunkTilePos + new Vector2(x_stride * stride, y_stride * stride);

                        // get region z corners.
                        z0 = GetZ((int)strideOffset.X, (int)strideOffset.Y);
                        z1 = GetZ((int)strideOffset.X + stride, (int)strideOffset.Y);
                        z2 = GetZ((int)strideOffset.X, (int)strideOffset.Y + stride);
                        z3 = GetZ((int)strideOffset.X + stride, (int)strideOffset.Y + stride);

                        //Console.WriteLine("[{4}]: {0}, {1}, {2}, {3} --- strideXY {5}/{6}", z0, z1, z2, z3, chunkPosition, x_stride, y_stride);

                        for (int x = stride * x_stride; x < (x_stride + 1) * stride; x++)
                        {
                            for (int y = y_stride * stride; y < (y_stride + 1) * stride; y++)
                            {
                                ret[x,y] += 5 * (GetWeight(x % stride, y % stride, stride, z0, z1, z2, z3));
                                //Console.Write("[{0}] ", ret[x,y]);
                            }
                            //Console.WriteLine();
                        }
                    }
                }

                stride /= 2;
            }

            return ret;
        }

        private float GetWeight(float x, float y, int stride, float z0, float z1, float z2, float z3)
        {
            /*
            z0 *= 0.5f;
            z1 *= 0.5f;
            z2 *= 0.5f;
            z3 *= 0.5f;
            */

            float xw = (x)  / (stride - 1);
            float yw = (y) / (stride - 1);

            return ((1f - yw) * (z0 + (xw * (z1 - z0)))) + (yw * (z2 + (xw * (z3 - z2))));
            
        }

        private float GetZ(int x, int y)
        {
            int zSeed = Services.MathHelper.MapCoordsToUniqueInteger(x,y);
            Random rand = new Random(zSeed + seed);
            rand = new Random(rand.Next() * zSeed);
            return rand.Next(100) / 100f;
        }
    }
}
