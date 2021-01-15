using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Services;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace
{
    public class Chunk
    {
        public static readonly int CHUNK_TILE_WIDTH = 16;
        public static readonly int CHUNK_TILE_HEIGHT = 16;
        public static int CHUNK_PIXEL_WIDTH => CHUNK_TILE_WIDTH * 35;
        public static int CHUNK_PIXEL_HEIGHT => CHUNK_TILE_HEIGHT * 18;

        public static Vector2 CHUNK_TILE_OFFSET = new Vector2(CHUNK_TILE_WIDTH, CHUNK_TILE_WIDTH);
        public static Vector2 CHUNK_TILE_OFFSET_NEGATIVE = new Vector2(-CHUNK_TILE_WIDTH, -CHUNK_TILE_WIDTH);

        public static float CartesianToIsometric_X(float x, float y)
        {
            return Tile.TILE_WIDTH * 0.5f * (x + y);
        }

        public static float CartesianToIsometric_Y(float x, float y, float z)
        {
            return (Tile.TILE_HEIGHT - 7) * 0.5f * (y - x) + (z * 6);
        }


        private Tile[,] tiles = new Tile[CHUNK_TILE_WIDTH, CHUNK_TILE_WIDTH];
        private IntegerPosition chunkIndexPosition;
        private bool verifiedChunk = false;

        private Sprite chunkSprite;
        private bool spriteHasBeenCreated = false;

        private int minimumZ, maximumZ;

        public Tile[,] Tiles { get => tiles; set => tiles = value; }
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
        /// <summary>
        /// GameSpace Location is used for positioning on the pixel level.
        /// </summary>
        public Vector2 GameSpaceLocation => new Vector2(
            ChunkIndexPosition.X * Chunk.CHUNK_PIXEL_WIDTH,
            ChunkIndexPosition.Y * Chunk.CHUNK_PIXEL_HEIGHT
            );

        public bool ZValuesVerified { get => verifiedChunk; }
        public int MinimumZ { get => minimumZ; set => minimumZ = value; }
        public int MaximumZ { get => maximumZ; set => maximumZ = value; }
        public bool SpriteHasBeenCreated { get => spriteHasBeenCreated; private set => spriteHasBeenCreated = value; }
        public Sprite ChunkSprite { get => chunkSprite; set { chunkSprite = value; SpriteHasBeenCreated = true; } }

        public void ConfirmZValueVerfication(int minimumZ, int maximumZ)
        {
            verifiedChunk = true;
            this.MinimumZ = minimumZ;
            this.MaximumZ = maximumZ;
        }

        public Chunk(Vector2 baseLocation)
        {
            this.chunkIndexPosition = baseLocation;
        }

        public bool WithinPosition(Vector2 basePos)
        {
            return TileSpaceLocation.X <= basePos.X &&
                TileSpaceEdgeLocation.X > basePos.X &&
                TileSpaceLocation.Y <= basePos.Y &&
                TileSpaceEdgeLocation.Y > basePos.Y;
        }

        public static IntegerPosition WorldSpace_To_ChunkSpace(Vector2 position)
        {
            return new Vector2(position.X / Chunk.CHUNK_TILE_WIDTH, position.Y / Chunk.CHUNK_TILE_HEIGHT);
        }
    }
}
