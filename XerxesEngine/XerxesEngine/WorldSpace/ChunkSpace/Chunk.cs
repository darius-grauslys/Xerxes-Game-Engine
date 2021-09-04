using XerxesEngine.Rendering;
using XerxesEngine.Scenes;
using XerxesEngine.Systems;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XerxesEngine.WorldSpace.ChunkSpace
{
    public enum ChunkStructureLayerType
    {
        Ground = 0,
        Liquid = 1,
        Flora = 2
    }

    public struct Chunk
    {
        public static readonly int CHUNK_TILE_WIDTH = 16;
        public static readonly int CHUNK_TILE_HEIGHT = 16;
        public static int CHUNK_PIXEL_WIDTH => CHUNK_TILE_WIDTH * 35;
        public static int CHUNK_PIXEL_HEIGHT => CHUNK_TILE_HEIGHT * 18;

        public static int CHUNK_MAX_STRUCTURE_COUNT = 3;

        public static IntegerPosition WorldSpace_To_ChunkSpace(Vector2 position)
        {
            return new Vector2(position.X / Chunk.CHUNK_TILE_WIDTH, position.Y / Chunk.CHUNK_TILE_HEIGHT);
        }
        public static float CartesianToIsometric_X(float x, float y)
        {
            return Tile.TILE_WIDTH * 0.5f * (x + y);
        }
        public static float IsometricToCartesian_X(float iso_x, float cart_y)
        {
            return ((2 * iso_x) / Tile.TILE_WIDTH) - cart_y;
        }

        public static float CartesianToIsometric_Y(float x, float y, float z)
        {
            return (Tile.TILE_HEIGHT-7) * 0.5f * (y - x) + (-z * 6);
        }
        public static float IsometricToCartesian_Y(float iso_y, float cart_x, float z)
        {
            return ((2 * iso_y - (12 * z)) / (Tile.TILE_HEIGHT - 7)) + cart_x;
        }

        public static int Localize(int x_or_y) => (CHUNK_PIXEL_WIDTH + (x_or_y % CHUNK_TILE_WIDTH)) % CHUNK_TILE_WIDTH;
        public static IntegerPosition Localize(IntegerPosition pos) => new IntegerPosition(Localize(pos.X), Localize(pos.Y));

        //private Tile[,] tiles = new Tile[CHUNK_TILE_WIDTH, CHUNK_TILE_HEIGHT];
        private IntegerPosition chunkIndexPosition;
        private bool isFinalized;
        private bool isValid;

        private RenderStructure[] chunkStructures;
        private int structureCount;

        private float minimumZ, maximumZ;

        //public Tile[,] Tiles { get => tiles; set => tiles = value; }
        /// <summary>
        /// Base Location is used for positioning on the chunk level. 
        /// </summary>
        public IntegerPosition ChunkIndexPosition { get => chunkIndexPosition; set => chunkIndexPosition = value; }

        /// <summary>
        /// TileSpace Location is used for positioning on the tile level.
        /// </summary>
        public IntegerPosition TileSpaceLocation => new IntegerPosition(
            ChunkIndexPosition.X * Chunk.CHUNK_TILE_WIDTH,
            ChunkIndexPosition.Y * Chunk.CHUNK_TILE_WIDTH
            );

        /// <summary>
        /// This is the edge of the chunk in terms of Tile Space.
        /// </summary>
        public IntegerPosition TileSpaceEdgeLocation => TileSpaceLocation + new IntegerPosition(16, 16);

        public bool IsFinalized { get => isFinalized; }
        public float MinimumZ { get => minimumZ; set => minimumZ = value; }
        public float MaximumZ { get => maximumZ; set => maximumZ = value; }
        public RenderStructure[] ChunkStructures { get => chunkStructures; private set => chunkStructures = value; }
        public bool IsValid { get => isValid; set => isValid = value; }

        public Chunk(Vector2 baseLocation)
        {
            this.chunkIndexPosition = baseLocation;
            isFinalized = false;
            minimumZ = 0;
            maximumZ = 0;
            isValid = true;
            chunkStructures = new RenderStructure[CHUNK_MAX_STRUCTURE_COUNT];
            structureCount = 0;
        }

        public void AddStructure(RenderStructure structure)
        {
            chunkStructures[structureCount] = structure;
            structureCount++;
        }

        public void AssertZValues()
        {
            for (int i = 0; i < chunkStructures.Length; i++)
            {
                if (chunkStructures[i].minimumZ < minimumZ)
                    minimumZ = chunkStructures[i].minimumZ;
                if (chunkStructures[i].maximumZ > maximumZ)
                    maximumZ = chunkStructures[i].maximumZ;
            }
        }

        public bool WithinPosition(Vector2 basePos)
        {
            return TileSpaceLocation.X <= basePos.X &&
                TileSpaceEdgeLocation.X > basePos.X &&
                TileSpaceLocation.Y <= basePos.Y &&
                TileSpaceEdgeLocation.Y > basePos.Y;
        }
    }
}
