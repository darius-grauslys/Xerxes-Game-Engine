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
        protected Perlin Perlin => perlin;

        public Generator(int seed)
        {
            this.seed = seed;
            perlin = new Perlin(seed);
        }

        public abstract Chunk GetChunk(Vector2 pos);
    }
}
