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

        public event Action<Chunk> ChunkLoaded;
        public event Action<Chunk> ChunkUnloaded;

        #region locationals

        public float MinimalX_ByBaseLocation => _chunks[0,0].ChunkIndexPosition.X;
        public float MinimalY_ByBaseLocation => _chunks[0,0].ChunkIndexPosition.Y;
        public float MaximalX_ByBaseLocation => _chunks[DoubleDist-1, DoubleDist-1].ChunkIndexPosition.X;
        public float MaximalY_ByBaseLocation => _chunks[DoubleDist-1, DoubleDist-1].ChunkIndexPosition.Y;

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

        public ChunkDirectory(int renderDistance, Generator chunkGenerator)
        {
            this.chunkGenerator = chunkGenerator;
            this.renderDistance = renderDistance;
            this.newRenderDistance = renderDistance;

            _chunks = new Chunk[DoubleDist, DoubleDist];
        }

        /*
         *     -a  x  x  x  x
         *  
         *      x  x  x  x  x
         *
         *      x  x -o  x  x
         * 
         *      x  x  x  x  x
         * 
         *      x  x  x  x -b
         * 
         * 
         *    Chunk 'o' is the player chunk. The one the player stands on.
         *    Chunk 'a' is the minimal chunk. Chunks[0]
         *    Chunk 'b' is the maximal chunk, C
         * 
         *    a and b are out of render view, only o and the adjacent x.
         *    infact there is probably more chunks loaded in memory.
         */

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

        public Chunk DeliminateChunk(Vector2 position)
        {
            IntegerPosition posToChunk = Chunk.WorldSpace_To_ChunkSpace(position);
            IntegerPosition chunkPosition = posToChunk - center + new IntegerPosition(renderDistance, renderDistance);
            
            return _chunks[chunkPosition.X, chunkPosition.Y];

            /*
            Chunk deliminatedChunk = activeChunks.Find((c) => c.TileSpaceLocation.X <= tilePos.X && c.TileSpaceLocation.Y <= tilePos.Y && (c.TileSpaceLocation.X + Chunk.CHUNK_TILE_WIDTH > tilePos.X) && (c.TileSpaceLocation.Y + Chunk.CHUNK_TILE_WIDTH > tilePos.Y));
            if (deliminatedChunk == null)
                deliminatedChunk = skirtChunks.Find((c) => c.TileSpaceLocation.X <= tilePos.X && c.TileSpaceLocation.Y <= tilePos.Y && (c.TileSpaceLocation.X + Chunk.CHUNK_TILE_WIDTH > tilePos.X) && (c.TileSpaceLocation.Y + Chunk.CHUNK_TILE_WIDTH > tilePos.Y));
            if (deliminatedChunk == null)
                throw new CouldNotDelimitException();
            return deliminatedChunk;
            */
        }

        /// <summary>
        /// Deliminates a tile using gamespace coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile DeliminateTile(IntegerPosition position)
        {
            //Chunk c = DeliminateChunk(position);

            IntegerPosition posToChunk = Chunk.WorldSpace_To_ChunkSpace(position);
            IntegerPosition chunkPosition = posToChunk - center + new IntegerPosition(renderDistance, renderDistance);

            Chunk c = _chunks[chunkPosition.X, chunkPosition.Y];
            IntegerPosition tilePos = position;
            Tile tile = c.Tiles[tilePos.X - c.TileSpaceLocation.X, tilePos.Y - c.TileSpaceLocation.Y];

            return tile;
        }

        public byte GetTileOrientation(Vector2 pos)
        {
            Tile[] tiles = new Tile[4];

            tiles[0] = DeliminateTile(pos);
            tiles[1] = DeliminateTile(new Vector2(pos.X - 1, pos.Y));
            tiles[2] = DeliminateTile(new Vector2(pos.X, pos.Y + 1));
            tiles[3] = DeliminateTile(new Vector2(pos.X + 1, pos.Y));

            float tZ = tiles[0].Z;
            for (int i = 1; i < 4; i++)
            {
                if (tZ < tiles[i].Z)
                    tZ = tiles[i].Z;
            }
            byte ret = (byte)( (tiles[0].Z == tZ ? 1 : 0) + (tiles[1].Z == tZ ? 2 : 0) + (tiles[2].Z == tZ ? 4 : 0) + (tiles[3].Z == tZ ? 8 : 0) );
            return ret;
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
                    c = (x < _chunks.GetLength(0) && y < _chunks.GetLength(1)) ? _chunks[x, y] : null;

                    if (c != null)
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
                            ChunkUnloaded?.Invoke(c);
                        }
                    }

                    if (newChunkSet[x, y] != null)
                        continue;

                    IntegerPosition newChunkPos = new IntegerPosition(x, y) - render_offset + newCenter;
                    Chunk new_c = ChunkGenerator.GetChunk(newChunkPos);
                    newChunkSet[x, y] = new_c;
                }
            }

            _chunks = newChunkSet;
            center = newCenter;

            //verify z values and orientations
            for (int x = 1; x < DoubleDist-1; x++) //do not verify skirt chunks, only: [1, n-1]
            {
                for (int y = 1; y < DoubleDist-1; y++)
                {
                    if (!_chunks[x, y].ZValuesVerified)
                    {
                        //obselete
                        //ChunkPass_VerifyOrientations();

                        ChunkPass_VerifyZValues(_chunks[x, y]);
                        ChunkLoaded?.Invoke(_chunks[x, y]);
                    }
                }
            }
        }

        private void ChunkPass_VerifyZValues(Chunk c)
        {
            int minimumZ = c.Tiles[0,0].Z, maximumZ = c.Tiles[0,0].Z;
            IntegerPosition lookupPos, chunkPos = c.TileSpaceLocation;

            IntegerPosition localPos = new IntegerPosition(0,0);

            for(localPos.Y = 0; localPos.Y < Chunk.CHUNK_TILE_HEIGHT; localPos.Y++)
            {
                for (localPos.X = 0; localPos.X < Chunk.CHUNK_TILE_WIDTH; localPos.X++)
                {
                    byte orientation = 0;
                    byte oriVal = 0;

                    Tile target = c.Tiles[localPos.X, localPos.Y];

                    if (target.Z < minimumZ)
                        minimumZ = target.Z;
                    if (target.Z > maximumZ)
                        maximumZ = target.Z;

                    for (int ly = -1; ly < 2; ly++)
                    {
                        for (int lx = -1; lx < 2; lx++)
                        {
                            if (lx == 0 && ly == 0)
                                continue;
                            oriVal = Tile.ORIENTATIONS[lx + 1, ly + 1];
                            lookupPos = new IntegerPosition(lx, ly) + localPos;
                            Tile t;

                            if (lookupPos.X < 0 || lookupPos.Y < 0 || lookupPos.X >= Chunk.CHUNK_TILE_WIDTH || lookupPos.Y >= Chunk.CHUNK_TILE_HEIGHT)
                            {
                                t = DeliminateTile(lookupPos + chunkPos);
                            }
                            else
                            {
                                t = c.Tiles[lookupPos.X, lookupPos.Y];
                            }

                            if (t.Z < target.Z)
                            {
                                orientation = (byte)(orientation | oriVal);
                            }
                        }
                    }
                    
                    if (orientation >= 15)
                    {
                        orientation = 0;
                        c.Tiles[localPos.X, localPos.Y].Z--;
                    }
                    c.Tiles[localPos.X, localPos.Y].Orientation = orientation;
                }
            }

            c.ConfirmZValueVerfication(minimumZ, maximumZ);
        }

        private void ChunkPass_VerifyOrientations(Vector2 chunkTilePos)
        {

        }
    }
}
