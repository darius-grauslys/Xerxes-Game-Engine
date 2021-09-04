using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XerxesEngine.WorldSpace.ChunkSpace
{
    public struct Tile
    {
        public static readonly int TILE_WIDTH = 36;
        public static readonly int TILE_HEIGHT = 25;
        public static readonly int TILE_AREA = TILE_WIDTH * TILE_HEIGHT;
        public static readonly int TILE_Y_INC = TILE_HEIGHT / 2 - 3;

        public static byte[,] ORIENTATIONS = new byte[,]
        {
            /*
            { 8, 9, 1 },
            { 12, 0, 3 },
            { 4, 6, 2 }
            */
            { 2, 6, 4 },
            { 3, 0, 12 },
            { 1, 9, 8 }
        };

        private int _data;
        private int _z;
        private byte _orientation;

        public Tile(int _z, int _data = 0, byte _orientation=0)
        {
            this._z = _z;
            this._data = _data;
            this._orientation = _orientation;
        }

        public int Z { get => _z; set => _z = value; }
        public byte Orientation { get => _orientation; set => _orientation = value; }
        public int Data { get => _data; set => _data = value; }
    }
}
