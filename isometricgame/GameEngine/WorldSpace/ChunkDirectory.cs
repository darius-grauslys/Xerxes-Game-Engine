using isometricgame.GameEngine.Exceptions.WorldSpace;
using isometricgame.GameEngine.WorldSpace.Generators;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace
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
        private int renderDistance;
        private ActiveChunkLookup acl;
        
        #region locationals
        public float MinimalX_ByBaseLocation => activeChunks[0].ChunkIndexPosition.X;
        public float MinimalY_ByBaseLocation => activeChunks[0].ChunkIndexPosition.Y;
        public float MaximalX_ByBaseLocation => activeChunks[activeChunks.Count - 1].ChunkIndexPosition.X;
        public float MaximalY_ByBaseLocation => activeChunks[activeChunks.Count - 1].ChunkIndexPosition.Y;

        public float MinimalX_ByTileLocation => MinimalX_ByBaseLocation * Chunk.CHUNK_TILE_WIDTH;
        public float MinimalY_ByTileLocation => MinimalY_ByBaseLocation * Chunk.CHUNK_TILE_WIDTH;
        public float MaximalX_ByTileLocation => MaximalX_ByBaseLocation * Chunk.CHUNK_TILE_WIDTH;
        public float MaximalY_ByTileLocation => MaximalY_ByBaseLocation * Chunk.CHUNK_TILE_WIDTH;

        public float MinimalX_ByGameLocation => MinimalX_ByBaseLocation * Chunk.CHUNK_PIXEL_WIDTH;
        public float MinimalY_ByGameLocation => MinimalY_ByBaseLocation * Chunk.CHUNK_PIXEL_HEIGHT;
        public float MaximalX_ByGameLocation => MaximalX_ByBaseLocation * Chunk.CHUNK_PIXEL_WIDTH;
        public float MaximalY_ByGameLocation => MaximalY_ByBaseLocation * Chunk.CHUNK_PIXEL_HEIGHT;
        #endregion

        public Generator ChunkGenerator { get => chunkGenerator; set => chunkGenerator = value; }

        public ChunkDirectory(int renderDistance, Generator chunkGenerator)
        {
            this.chunkGenerator = chunkGenerator;
            this.renderDistance = renderDistance;
            acl = new ActiveChunkLookup(new Vector2(0,0), this);
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

        /// <summary>
        /// Deliminates a tile using game space coordinates.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Tile DeliminateTile(Vector2 pos)
        {
            float x, y;
            x = (float)Math.Floor(pos.X);
            y = (float)Math.Floor(pos.Y);
            return DeliminateTile_ChunkSpace(new Vector2(x,y));
        }

        public Chunk DeliminateChunk(Vector2 tilePos)
        {
            Chunk deliminatedChunk = activeChunks.Find((c) => c.TileSpaceLocation.X <= tilePos.X && c.TileSpaceLocation.Y <= tilePos.Y && (c.TileSpaceLocation.X + Chunk.CHUNK_TILE_WIDTH > tilePos.X) && (c.TileSpaceLocation.Y + Chunk.CHUNK_TILE_WIDTH > tilePos.Y));
            if (deliminatedChunk == null)
                deliminatedChunk = skirtChunks.Find((c) => c.TileSpaceLocation.X <= tilePos.X && c.TileSpaceLocation.Y <= tilePos.Y && (c.TileSpaceLocation.X + Chunk.CHUNK_TILE_WIDTH > tilePos.X) && (c.TileSpaceLocation.Y + Chunk.CHUNK_TILE_WIDTH > tilePos.Y));
            if (deliminatedChunk == null)
                throw new CouldNotDelimitException();
            return deliminatedChunk;
        }

        /// <summary>
        /// Deliminates a tile using ChunkSpace coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile DeliminateTile_ChunkSpace(Vector2 pos)
        {
            Chunk c = DeliminateChunk(pos);
            Tile tile = c.Tiles[(int)(pos.X - c.TileSpaceLocation.X), (int)(pos.Y - c.TileSpaceLocation.Y)];

            return tile;
        }

        public byte GetTileOrientation(Vector2 pos)
        {
            Tile[] tiles = new Tile[4];
            try
            {
                tiles[0] = DeliminateTile(pos);
                tiles[1] = DeliminateTile(new Vector2(pos.X - 1, pos.Y));
                tiles[2] = DeliminateTile(new Vector2(pos.X, pos.Y + 1));
                tiles[3] = DeliminateTile(new Vector2(pos.X + 1, pos.Y));
            }
            catch (CouldNotDelimitException)
            {
                return 0;
            }

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
        public void ChunkCleanup(Vector2 cameraPosition)
        {
            //Vector3 camPos = worldRef.Camera.HookedObject.Position;
            //Vector2 camBasePos = new Vector2(worldRef.Camera.Position.X / (Chunk.CHUNK_PIXEL_WIDTH), (worldRef.Camera.Position.Y+50) / (Chunk.CHUNK_PIXEL_HEIGHT));
            //camBasePos = new Vector2((float)Math.Round(camBasePos.X), (float)Math.Round(camBasePos.Y));

            //Vector2 camBasePos = new Vector2((float)Math.Round(worldRef.Camera.Position.X / Chunk.CHUNK_PIXEL_WIDTH), (float)Math.Round(worldRef.Camera.Position.Y / Chunk.CHUNK_PIXEL_WIDTH));
            Vector2 camBasePos = new Vector2((float)Math.Round(cameraPosition.X / Chunk.CHUNK_TILE_WIDTH), (float)Math.Round(cameraPosition.Y / Chunk.CHUNK_TILE_WIDTH));
            //Vector2 camBasePos = new Vector2(worldRef.Camera.Iso_X, worldRef.Camera.Iso_Y);

            //List of positions that MUST be filled based on player position.
            List<Vector2> neededBasePositions = new List<Vector2>();
            List<Vector2> neededSkirtPositions = new List<Vector2>();
            List<Chunk> unverifiedChunks = new List<Chunk>();
            for (int x = -renderDistance; x < renderDistance+1; x++)
            {
                for (int y = -renderDistance; y < renderDistance+1; y++)
                {
                    if (Math.Abs(x) >= renderDistance || Math.Abs(y) >= renderDistance)
                    {
                        neededSkirtPositions.Add(camBasePos + new Vector2(x,y));
                    }
                    else
                    {
                        neededBasePositions.Add(camBasePos + new Vector2(x, y));
                    }
                }
            }

            /*
             * Phase 1, check if active chunks has chucks filling the needed positions.
             * Check as well if skirt has existing chunks filling the needed positions.
             */

            //***********************************************************
            //***********************************************************
            //*  Check needed positions for validity.
            foreach (Chunk c in activeChunks.ToList())
            {
                if (neededBasePositions.Contains(c.ChunkIndexPosition))
                {
                    //Chunk is found with the needed position, remove the needed position from the query list.
                    neededBasePositions.Remove(c.ChunkIndexPosition);
                }
                else
                {
                    activeChunks.Remove(c);
                    if (neededSkirtPositions.Contains(c.ChunkIndexPosition))
                    {
                        skirtChunks.Add(c);
                        neededSkirtPositions.Remove(c.ChunkIndexPosition);
                    }
                }
            }

            foreach (Chunk c in skirtChunks.ToList())
            {
                if (neededSkirtPositions.Contains(c.ChunkIndexPosition))
                {
                    neededSkirtPositions.Remove(c.ChunkIndexPosition);
                }
                else
                {
                    skirtChunks.Remove(c);
                    if (neededBasePositions.Contains(c.ChunkIndexPosition))
                    {
                        activeChunks.Add(c);
                        neededBasePositions.Remove(c.ChunkIndexPosition);
                        if (!c.ZValuesVerified)
                            unverifiedChunks.Add(c);
                    }
                }
            }

            unverifiedChunks.AddRange(ChunkGenerator.GetChunks(camBasePos, neededBasePositions));
            activeChunks.AddRange(unverifiedChunks);
            activeChunks = GetSortedChunks();
            skirtChunks.AddRange(ChunkGenerator.GetChunks(camBasePos, neededSkirtPositions));

            foreach (Chunk c in unverifiedChunks)
            {
                ChunkPass_VerifyZValues(c);
            }
        }

        private void ChunkPass_VerifyZValues(Chunk c)
        {
            int minimumZ = 0, maximumZ = 0;
            Vector2 relPos = c.TileSpaceLocation;
            for (int i = 0; i < Chunk.CHUNK_TILE_WIDTH; i++)
            {
                for (int j = 0; j < Chunk.CHUNK_TILE_HEIGHT * 2; j++)
                {
                    acl.Mode = ActiveChunkLookupMode.SingleTile;
                    relPos = c.TileSpaceLocation + new Vector2(i, j);
                    Tile targetT = acl.DeliminateTile(relPos);
                    Tile t;
                    int baseZ = targetT.Z;
                    byte orientation = 0;

                    if (minimumZ > targetT.Z)
                        minimumZ = targetT.Z;
                    if (maximumZ < targetT.Z)
                        maximumZ = targetT.Z;

                    acl.Mode = ActiveChunkLookupMode.NearbyTiles;

                    foreach (Tuple<Vector2, byte> lookupPair in ActiveChunkLookup.GetAdjacencyVectors(relPos.Y+1))
                    {
                        t = acl.DeliminateTile(relPos, lookupPair.Item1);
                        if (baseZ > t.Z)
                            orientation = (byte)(orientation | lookupPair.Item2);
                    }

                    if (orientation == 15)
                    {
                        targetT.Orientation = 0;
                        //this is Z-correction.
                        targetT.Z -= 1;
                    }
                    else
                    {
                        targetT.Orientation = orientation;
                    }
                    acl.ModiftyTile(targetT);
                }
            }
            c.ConfirmZValueVerfication(minimumZ, maximumZ);
        }

        private void ChunkPass_VerifyOrientations(Vector2 chunkTilePos)
        {
            Vector2 relPos = chunkTilePos;
            for (int i = 0; i < Chunk.CHUNK_TILE_WIDTH; i++)
            {
                for (int j = 0; j < Chunk.CHUNK_TILE_HEIGHT; j++)
                {
                    relPos = chunkTilePos + new Vector2(i, j);

                }
            }
        }
    }
}
