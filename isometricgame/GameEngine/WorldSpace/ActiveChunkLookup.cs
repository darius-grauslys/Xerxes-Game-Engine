using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace
{
    /// <summary>
    /// This is a helpful class for looking up Tiles quickly.
    /// </summary>
    public class ActiveChunkLookup
    {
        //public static readonly Tuple<Vector2, byte>[] AdjacencyVectors = new Tuple<Vector2, byte>[]
        //{
        //    new Tuple<Vector2, byte>(new Vector2(0, 2), 1),
        //    new Tuple<Vector2, byte>(new Vector2(-1, 0), 2),
        //    new Tuple<Vector2, byte>(new Vector2(-1, 1), 3),
        //    new Tuple<Vector2, byte>(new Vector2(0, -2), 4),
        //    new Tuple<Vector2, byte>(new Vector2(-1, -1), 6),
        //    new Tuple<Vector2, byte>(new Vector2(1, 0), 8),
        //    new Tuple<Vector2, byte>(new Vector2(0, 1), 9),
        //    new Tuple<Vector2, byte>(new Vector2(0, -1), 12)
        //};

        /*
    private static readonly Tuple<Vector2, byte>[][] DualAdjacencyVectors = new Tuple<Vector2, byte>[][]
    {               //this table is used for when Y is even
        new Tuple<Vector2, byte>[] {
            new Tuple<Vector2, byte>(new Vector2(0, 2), 4),         //*
            new Tuple<Vector2, byte>(new Vector2(-1, 0), 2),        //*
            new Tuple<Vector2, byte>(new Vector2(-1, 1), 6),
            new Tuple<Vector2, byte>(new Vector2(0, -2), 1),        //*
            new Tuple<Vector2, byte>(new Vector2(-1, -1), 3),
            new Tuple<Vector2, byte>(new Vector2(1, 0), 8),         //*
            new Tuple<Vector2, byte>(new Vector2(0, 1), 12),
            new Tuple<Vector2, byte>(new Vector2(0, -1), 9)
        },           //this table is used for when Y is odd
        new Tuple<Vector2, byte>[] {
            new Tuple<Vector2, byte>(new Vector2(0, 2), 4),         //*
            new Tuple<Vector2, byte>(new Vector2(-1, 0), 2),        //*
            new Tuple<Vector2, byte>(new Vector2(1, -1), 9),
            new Tuple<Vector2, byte>(new Vector2(0, -2), 1),        //*
            new Tuple<Vector2, byte>(new Vector2(0, 1), 6),
            new Tuple<Vector2, byte>(new Vector2(1, 0), 8),         //*
            new Tuple<Vector2, byte>(new Vector2(0, -1), 3),
            new Tuple<Vector2, byte>(new Vector2(1, 1), 12)
        }
    };*/




        private static readonly Tuple<Vector2, byte>[][] DualAdjacencyVectors = new Tuple<Vector2, byte>[][]
        {               //this table is used for when Y is even
            new Tuple<Vector2, byte>[] {
                new Tuple<Vector2, byte>(new Vector2(-1, 1), 4), //4         //*
                new Tuple<Vector2, byte>(new Vector2(-1, -1), 2),        //*
                new Tuple<Vector2, byte>(new Vector2(-1, 0), 6),
                new Tuple<Vector2, byte>(new Vector2(1, -1), 1),        //*
                new Tuple<Vector2, byte>(new Vector2(0, 1), 12),
                new Tuple<Vector2, byte>(new Vector2(1, 1), 8), //8         //*
                new Tuple<Vector2, byte>(new Vector2(0, -1), 3),
                new Tuple<Vector2, byte>(new Vector2(1, 0), 9)
            },           //this table is used for when Y is odd
            new Tuple<Vector2, byte>[] {
                new Tuple<Vector2, byte>(new Vector2(-1, 1), 4), //4        //*
                new Tuple<Vector2, byte>(new Vector2(-1, -1), 2),        //*
                new Tuple<Vector2, byte>(new Vector2(-1, 0), 6),
                new Tuple<Vector2, byte>(new Vector2(1, -1), 1),        //*
                new Tuple<Vector2, byte>(new Vector2(0, 1), 12),
                new Tuple<Vector2, byte>(new Vector2(1, 1), 8), //8        //*
                new Tuple<Vector2, byte>(new Vector2(0, -1), 3),
                new Tuple<Vector2, byte>(new Vector2(1, 0), 9)
            }
        };

        public static Tuple<Vector2, byte>[] GetAdjacencyVectors(float y)
        {
            Tuple<Vector2, byte>[] ret = DualAdjacencyVectors[(int)Math.Abs(y) % 2];
            return ret;
        }

        private ActiveChunkLookupMode mode = ActiveChunkLookupMode.NearbyTiles;
        public Chunk targetChunk, overflowChunk, lookupChunk;
        private ChunkDirectory cs;
        private Vector2 relativeBasePosition, relativeNearbyPosition;

        public ActiveChunkLookupMode Mode { get => mode; set => mode = value; }

        public ActiveChunkLookup(Vector2 basePos, ChunkDirectory cs)
        {
            this.cs = cs;
        }

        /// <summary>
        /// This will locate a tile quickly by first checking the home chunk, then delimiting chunks if needed.
        /// </summary>
        /// <param name="basePos">The position of interest in Single Mode, the reference position in Nearby mode.</param>
        /// <param name="targetPos">This position is only relevant in Nearby mode. Use this to find adjacent tiles.</param>
        /// <returns></returns>
        public Tile DeliminateTile(Vector2 basePos, Vector2 targetPos=default(Vector2))
        {
            if (targetChunk == null)
            {
                targetChunk = cs.DeliminateChunk(basePos);
                overflowChunk = targetChunk;
            }
            if (!targetChunk.WithinPosition(basePos))
            {
                //relocate target chunk
                targetChunk = cs.DeliminateChunk(basePos);
            }
            relativeBasePosition = (IntegerPosition)basePos - targetChunk.TileSpaceLocation;

            if (mode == ActiveChunkLookupMode.NearbyTiles)
            {
                relativeNearbyPosition = basePos - targetPos;
                //if the relative position is not in the base position chunk, then utilize overflowchunk
                if (!targetChunk.WithinPosition(relativeNearbyPosition))
                {
                    //if not within the previously examined overflow, move overflow
                    if (!overflowChunk.WithinPosition(relativeNearbyPosition))
                    {
                        overflowChunk = cs.DeliminateChunk(relativeNearbyPosition);
                    }

                    //now that overflow is determined, find rel
                    relativeNearbyPosition = (IntegerPosition)relativeNearbyPosition - overflowChunk.TileSpaceLocation;
                    lookupChunk = overflowChunk;
                }
                else
                {
                    relativeNearbyPosition = (IntegerPosition)relativeNearbyPosition - targetChunk.TileSpaceLocation;
                    lookupChunk = targetChunk;
                }
            }
            else
            {
                relativeNearbyPosition = (IntegerPosition)basePos - targetChunk.TileSpaceLocation;
                lookupChunk = targetChunk;
            }

            return lookupChunk.Tiles[(int)relativeNearbyPosition.X, (int)relativeNearbyPosition.Y];
        }

        public void ModiftyTile(Tile t)
        {
            if (lookupChunk == null)
                return;
            targetChunk.Tiles[(int)relativeBasePosition.X, (int)relativeBasePosition.Y] = t;
        }
    }

    /// <summary>
    /// Single tile mode will make for fast look up single tile look up. Use NearbyTiles for fast look up for adjacent tiles.
    /// If you only need to look up a single tile once in a while, don't use ActiveChunkLookup, just use ChunkService's tile delimiter
    /// </summary>
    public enum ActiveChunkLookupMode
    {
        SingleTile,
        NearbyTiles
    }
}
