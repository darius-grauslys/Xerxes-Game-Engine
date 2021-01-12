using isometricgame.GameEngine.WorldSpace;
using isometricgame.GameEngine.WorldSpace.Generators;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Serializations
{
    public class ChunkSerializationService : BaseSerializationService
    {
        private static readonly int CHUNK_GROUPING_BASE = 16;
        
        private WorldMetaHeader worldMetaHeader;
        private readonly string WORLD_PATH, HEADER_PATH;
        private SerializationService<WorldMetaHeader> headerSerializer;
        private Generator worldGenerator;

        private static readonly string HEADER_FILE_NAME = "header.dat";

        public ChunkSerializationService(Game gameRef, string worldName, Generator worldGenerator)
            :base(gameRef)
        {
            Console.WriteLine("Starting Chunk Serialization Service...");
            WORLD_PATH = Path.Combine(GameRef.GAME_DIRECTORY_WORLDS, worldName);
            HEADER_PATH = Path.Combine(WORLD_PATH, HEADER_FILE_NAME);

            if (!Directory.Exists(WORLD_PATH))

            headerSerializer = new SerializationService<WorldMetaHeader>(gameRef);
            this.worldGenerator = worldGenerator;

            if (File.Exists(HEADER_PATH))
            {
                worldMetaHeader = headerSerializer.DeserializeObject(HEADER_PATH);
            }
            else
            {
                worldMetaHeader = new WorldMetaHeader();
            }
            Console.WriteLine("Done!");
        }

        private int DeliminateRegion(Vector3 baseLocation)
        {
            return worldMetaHeader.SerializedRegions.FindIndex((r) =>
                r.StartBaseLocation.X < baseLocation.X &&
                r.StartBaseLocation.Y < baseLocation.Y &&
                r.EndBaseLocation.X > baseLocation.X &&
                r.EndBaseLocation.X > baseLocation.Y
                );
        }

        private bool ExistsChunk(Vector3 baseLocation)
        {
            int ir = worldMetaHeader.IndexOfRegion(baseLocation);
            int ic = IndexInRegion(baseLocation);

            return ir > -1 && (worldMetaHeader.SerializedRegions[ir].ExistingLocations[ic]);
        }

        public void SerializeChunk(Chunk c)
        {
            Vector2 baseLocation = c.ChunkIndexPosition;

            //int i = worldMetaHeader.CreateRegion(baseLocation);

            //SerializeObject<Chunk>(c, Marshal.SizeOf<Chunk>(), IndexInRegion(baseLocation));
        }

        public Chunk GetChunk(Vector3 baseLocation)
        {
            /*if (ExistsChunk(baseLocation))
            {
                return DeserializeChunk(baseLocation);
            }
            else
            {*/
            //return worldGenerator.GetChunk(baseLocation);
            return null;
            //}
        }

        private Chunk DeserializeChunk(Vector3 baseLocation)
        {
            int chunkIndex = IndexInRegion(baseLocation);

            int regionIndex = worldMetaHeader.IndexOfRegion(baseLocation);
            if (regionIndex == -1)
            {
                throw new NotImplementedException(); //replace with RegionDoesNotExistException;
            }
            else
            {
                bool existsLocation = worldMetaHeader.ExistsChunk(baseLocation);
                if (!existsLocation)
                {
                    throw new NotImplementedException(); //replace with ChunkDoesNotExistException;
                }
            }

            string fileHandle = worldMetaHeader.SerializedRegions[regionIndex].RegionFileHandle;
            return DeserializeObject<Chunk>(fileHandle, Marshal.SizeOf<Chunk>(), chunkIndex);
        }


        private static int IndexInRegion(Vector3 baseLocation)
        {
            Vector3 loc = new Vector3((int)baseLocation.X % CHUNK_GROUPING_BASE, (int)baseLocation.Y % CHUNK_GROUPING_BASE, 0);

            return (int)(loc.X + (loc.Y * CHUNK_GROUPING_BASE));
        }


        //BEGIN CLASS
        private class WorldMetaHeader
        {
            private List<SerializedChunkRegion> serializedRegions = new List<SerializedChunkRegion>();

            public List<SerializedChunkRegion> SerializedRegions { get => serializedRegions; set => serializedRegions = value; }

            public bool ExistsChunk(Vector3 baseLocation)
            {
                int ic = IndexInRegion(baseLocation);
                int ir = IndexOfRegion(baseLocation);

                return ir > -1 && (serializedRegions[ir].ExistingLocations[ic]);
            }

            public int IndexOfRegion(Vector3 baseLocation)
            {
                int i = SerializedRegions.FindIndex((scr) => MathHelper.IsBounded_XY(baseLocation, scr.StartBaseLocation, scr.EndBaseLocation));

                return i;
            }

            public int CreateRegion(Vector3 baseLocation)
            {
                int i = IndexOfRegion(baseLocation);
                if (i != -1)
                    return i;

                Vector3 baseRegionLocation_Modulo = new Vector3
                    (
                    (int)baseLocation.X % CHUNK_GROUPING_BASE, 
                    (int)baseLocation.Y % CHUNK_GROUPING_BASE,
                    0
                    );
                Vector3 baseRegionLocation_Division = new Vector3
                    (
                    (int)baseLocation.X / CHUNK_GROUPING_BASE,
                    (int)baseLocation.Y / CHUNK_GROUPING_BASE,
                    0
                    );

                Vector3 startLocation = baseRegionLocation_Division * CHUNK_GROUPING_BASE;
                Vector3 endLocation = (baseRegionLocation_Division + new Vector3(1, 1, 0)) * CHUNK_GROUPING_BASE;

                serializedRegions.Add(new SerializedChunkRegion(
                    startLocation, endLocation
                    ));
                return serializedRegions.Count - 1;
            }


            //BEGIN STRUCT
            public struct SerializedChunkRegion
            {
                private Vector3 startBaseLocation, endBaseLocation;
                private string regionFileHandle;
                private bool[] existingLocations;

                public Vector3 StartBaseLocation { get => startBaseLocation; private set => startBaseLocation = value; }
                public Vector3 EndBaseLocation { get => endBaseLocation; private set => endBaseLocation = value; }
                public string RegionFileHandle { get => regionFileHandle; private set => regionFileHandle = value; }
                public bool[] ExistingLocations { get => existingLocations; set => existingLocations = value; }

                public SerializedChunkRegion(Vector3 startBaseLoc, Vector3 endBaseLoc)
                {
                    startBaseLocation = startBaseLoc;
                    endBaseLocation = endBaseLoc;
                    regionFileHandle = String.Format("chunkspace_{0}_{1}.dat", startBaseLoc.X, startBaseLoc.Y);
                    existingLocations = new bool[CHUNK_GROUPING_BASE * CHUNK_GROUPING_BASE];
                }

                public void RecordChunk(int i, bool existanceState)
                {
                    existingLocations[i] = existanceState;
                }
            }
            //END STRUCT
        }
        //END CLASS
    }
}
