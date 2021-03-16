using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.WorldSpace.Generators;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace.ChunkSpace
{
    /// <summary>
    /// Responsible for generating new chunks, and serializing and deserializing them.
    /// </summary>
    public class ChunkDirectory
    {
        //Chunks[0] is the minimal position. It is often times removed or inserted.
        private List<Chunk> activeChunks = new List<Chunk>();
        private List<Chunk> skirtChunks = new List<Chunk>();
        private Generator chunkGenerator;
        private int renderDistance, newRenderDistance;

        private Chunk[,] _chunks;
        private IntegerPosition center = new IntegerPosition(0, 0);
        private bool firstRender = true;

        #region locationals

        public float MinimalX_ByBaseLocation => Chunks[0,0].ChunkIndexPosition.X;
        public float MinimalY_ByBaseLocation => Chunks[0,0].ChunkIndexPosition.Y;
        public float MaximalX_ByBaseLocation => Chunks[DoubleDist-1, DoubleDist-1].ChunkIndexPosition.X;
        public float MaximalY_ByBaseLocation => Chunks[DoubleDist-1, DoubleDist-1].ChunkIndexPosition.Y;

        public float MinimalX_ByTileLocation => MinimalX_ByBaseLocation * Chunk.CHUNK_TILE_WIDTH;
        public float MinimalY_ByTileLocation => MinimalY_ByBaseLocation * Chunk.CHUNK_TILE_HEIGHT;
        public float MaximalX_ByTileLocation => (MaximalX_ByBaseLocation+1) * Chunk.CHUNK_TILE_WIDTH-1;
        public float MaximalY_ByTileLocation => (MaximalY_ByBaseLocation+1) * Chunk.CHUNK_TILE_HEIGHT-1;

        public float VisibleWidth => (MaximalX_ByTileLocation - MinimalX_ByTileLocation) * Tile.TILE_WIDTH;
        public float VisibleHeight => (MaximalY_ByTileLocation - MinimalY_ByTileLocation) * Tile.TILE_HEIGHT;

        //public float MinimalX_ByGameLocation => MinimalX_ByBaseLocation * Chunk.CHUNK_PIXEL_WIDTH;
        //public float MinimalY_ByGameLocation => MinimalY_ByBaseLocation * Chunk.CHUNK_PIXEL_HEIGHT;
        //public float MaximalX_ByGameLocation => MaximalX_ByBaseLocation * Chunk.CHUNK_PIXEL_WIDTH;
        //public float MaximalY_ByGameLocation => MaximalY_ByBaseLocation * Chunk.CHUNK_PIXEL_HEIGHT;
        #endregion

        private int DoubleDist => renderDistance * 2 + 1;

        public Generator ChunkGenerator { get => chunkGenerator; set => chunkGenerator = value; }

        public int RenderDistance { get => renderDistance; set => newRenderDistance = value; }
        public Chunk[,] Chunks { get => _chunks; private set => _chunks = value; }

        public ChunkDirectory(int renderDistance, Generator chunkGenerator)
        {
            this.chunkGenerator = chunkGenerator;
            this.renderDistance = renderDistance;
            this.newRenderDistance = renderDistance;

            Chunks = new Chunk[DoubleDist, DoubleDist];
        }

        /// <summary>
        /// Sorts the chunks from 0 -> n by the sum of X*renderDist and Y.
        /// </summary>
        /// <returns></returns>
        public List<Chunk> GetSortedChunks()
        {
            return activeChunks.OrderBy((c) => (c.ChunkIndexPosition.X * renderDistance) + c.ChunkIndexPosition.Y).ToList();
        }

        public void SetCenter(Vector2 position)
        {
            center = Chunk.WorldSpace_To_ChunkSpace(position);
        }

        public IntegerPosition DeliminateChunkIndex(IntegerPosition position)
        {
            IntegerPosition posToChunk = Chunk.WorldSpace_To_ChunkSpace(position);
            IntegerPosition chunkPosition = posToChunk - center + new IntegerPosition(renderDistance, renderDistance);

            return chunkPosition;
        }

        /// <summary>
        /// Deliminates a tile using gamespace coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public IntegerPosition Localize(IntegerPosition position, IntegerPosition cpos)
        {
            /*
            IntegerPosition posToChunk = Chunk.WorldSpace_To_ChunkSpace(position);
            IntegerPosition chunkPosition = posToChunk - center + new IntegerPosition(renderDistance, renderDistance);

            Chunk c = _chunks[chunkPosition.X, chunkPosition.Y];
            */        

            return position - Chunks[cpos.X, cpos.Y].TileSpaceLocation;
        }

        private Vector3 DeliminateRenderUnit_Position(IntegerPosition position, int structureIndex = 0)
        {
            //contemplate using unsafe {}.
            IntegerPosition chunkPosition = DeliminateChunkIndex(position);
            IntegerPosition pos = Localize(position, chunkPosition);
            return Chunks[chunkPosition.X, chunkPosition.Y].ChunkStructures[structureIndex].structuralUnits[pos.X][pos.Y].Position;
        }

        public byte GetTileOrientation(IntegerPosition localPos, IntegerPosition chunkTilePos, ref RenderStructure structure)
        {
            IntegerPosition lookupPos;
            byte orientation = 0;
            byte oriVal = 0;
            Vector3 target = structure.structuralUnits[localPos.X][localPos.Y].Position;

            for (int ly = -1; ly < 2; ly++)
            {
                for (int lx = -1; lx < 2; lx++)
                {
                    if (lx == 0 && ly == 0)
                        continue;
                    oriVal = Tile.ORIENTATIONS[lx + 1, ly + 1];
                    lookupPos = new IntegerPosition(lx, ly) + localPos;
                    Vector3 t;

                    if (lookupPos.X < 0 || lookupPos.Y < 0 || lookupPos.X >= Chunk.CHUNK_TILE_WIDTH || lookupPos.Y >= Chunk.CHUNK_TILE_HEIGHT)
                    {
                        t = DeliminateRenderUnit_Position(lookupPos + chunkTilePos);
                    }
                    else
                    {
                        t = structure.structuralUnits[lookupPos.X][lookupPos.Y].Position;
                    }

                    if (t.Z < target.Z)
                    {
                        orientation = (byte)(orientation | oriVal);
                    }
                }
            }

            return orientation;
        }

        //we will need a means to check live to add/drop chunks
        public void ChunkCleanup(Vector2 centerPosition)
        {
            IntegerPosition newCenter = Chunk.WorldSpace_To_ChunkSpace(centerPosition);

            if (renderDistance != newRenderDistance)
                renderDistance = newRenderDistance;
            else if (newCenter == center && !firstRender)
                return;
            else if (firstRender)
                firstRender = false;

            IntegerPosition offsetFromOriginal = center - newCenter;
            
            Chunk[,] newChunkSet = new Chunk[DoubleDist, DoubleDist];
            
            IntegerPosition render_offset = new IntegerPosition(renderDistance, renderDistance);

            IntegerPosition minPos = newCenter - render_offset;
            IntegerPosition maxPos = newCenter + render_offset;
            Chunk c;

            //move chunks
            for (int x = 0 ; x < DoubleDist; x++)
            {
                for (int y = 0; y < DoubleDist; y++)
                {
                    c = (x < Chunks.GetLength(0) && y < Chunks.GetLength(1)) ? Chunks[x, y] : default(Chunk);

                    if (c.IsValid)
                    {
                        if (
                            c.ChunkIndexPosition.X >= minPos.X &&
                            c.ChunkIndexPosition.Y >= minPos.Y &&
                            c.ChunkIndexPosition.X < maxPos.X &&
                            c.ChunkIndexPosition.Y < maxPos.Y
                            )
                        {
                            IntegerPosition index = c.ChunkIndexPosition + render_offset - newCenter;
                            newChunkSet[index.X, index.Y] = c;
                        }
                        else
                        {

                        }
                    }

                    if (newChunkSet[x, y].IsValid)
                        continue;

                    IntegerPosition newChunkPos = new IntegerPosition(x, y) - render_offset + newCenter;
                    Chunk new_c = ChunkGenerator.CreateChunk(newChunkPos);
                    newChunkSet[x, y] = new_c;
                }
            }

            Chunks = newChunkSet;
            center = newCenter;

            //verify z values and orientations
            for (int x = 1; x < DoubleDist-1; x++) //do not verify skirt chunks, only: [1, n-1]
            {
                for (int y = 1; y < DoubleDist-1; y++)
                {
                    if (!Chunks[x, y].IsFinalized)
                    {
                        ChunkGenerator.FinalizeChunk(this, ref Chunks[x, y]);
                    }
                }
            }
        }

        public void PerformTileOrientation(IntegerPosition chunkTileSpace, ref RenderStructure structure)
        {
            IntegerPosition localPos = new IntegerPosition(0,0);

            for(localPos.Y = 0; localPos.Y < Chunk.CHUNK_TILE_HEIGHT; localPos.Y++)
            {
                for (localPos.X = 0; localPos.X < Chunk.CHUNK_TILE_WIDTH; localPos.X++)
                {
                    byte orientation = GetTileOrientation(localPos, chunkTileSpace, ref structure);

                    if (orientation >= 15)
                    {
                        orientation = 0;
                        structure.structuralUnits[localPos.X][localPos.Y].Position -= new Vector3(0,0,1);
                    }
                    structure.structuralUnits[localPos.X][localPos.Y].VAO_Index = orientation;
                }
            }
        }
    }
}
