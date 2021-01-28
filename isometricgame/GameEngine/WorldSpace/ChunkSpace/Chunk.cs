using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Systems;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace.ChunkSpace
{
    public class Chunk
    {
        public static readonly int CHUNK_TILE_WIDTH = 16;
        public static readonly int CHUNK_TILE_HEIGHT = 16;
        public static int CHUNK_PIXEL_WIDTH => CHUNK_TILE_WIDTH * 35;
        public static int CHUNK_PIXEL_HEIGHT => CHUNK_TILE_HEIGHT * 18;

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

        private Tile[,] tiles = new Tile[CHUNK_TILE_WIDTH, CHUNK_TILE_HEIGHT];
        private IntegerPosition chunkIndexPosition;
        private bool verifiedChunk = false;

        private List<SceneStructure> chunkStructures = new List<SceneStructure>();
        private List<SceneObject> chunkObjects = new List<SceneObject>();

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

        public bool ZValuesVerified { get => verifiedChunk; }
        public int MinimumZ { get => minimumZ; set => minimumZ = value; }
        public int MaximumZ { get => maximumZ; set => maximumZ = value; }
        public List<SceneStructure> ChunkStructures { get => chunkStructures; protected set => chunkStructures = value; }
        public List<SceneObject> ChunkObjects { get => chunkObjects; protected set => chunkObjects = value; }

        public event Action<Chunk> ZVerificated;

        internal void ConfirmZValueVerfication(int minimumZ, int maximumZ)
        {
            verifiedChunk = true;
            this.MinimumZ = minimumZ;
            this.MaximumZ = maximumZ;
            ZVerificated?.Invoke(this);
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
    }
}
