using isometricgame.GameEngine.WorldSpace.ChunkSpace;
using isometricgame.GameEngine.WorldSpace.Generators.PerlinNoise;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace isometricgame.GameEngine.WorldSpace.Generators
{
    public abstract class Generator
    {
        private int seed;
        private Perlin perlin;

        public int Seed { get => seed; private set => seed = value; }

        public Generator(int seed)
        {
            this.seed = seed;
            perlin = new Perlin(seed);
        }

        internal abstract Chunk GetChunk(float[,] noiseMap, Vector2 pos);

        public Chunk[] GetChunks(Vector2 centralPosition, List<Vector2> neededActivePositions)
        {
            Chunk[] ret = new Chunk[neededActivePositions.Count];

            for (int i = 0; i < neededActivePositions.Count; i++)
            {
                ret[i] = GetChunk(perlin.InterprolateNoise(neededActivePositions[i]), neededActivePositions[i]);
            }

            return ret;
        }

        public Chunk GetChunk(Vector2 pos)
        {
            return GetChunk(perlin.InterprolateNoise(pos), pos);
        }
    }
}
