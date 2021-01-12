using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace
{
    public struct TilePosition
    {
        int x, y, z;
        float zFloat;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Z { get => z; set => z = value; }

        public Vector2 Vector => new Vector2(x, y);

        public float ZFloat { get => zFloat;
            set {
                zFloat = value;
                Z = (int)zFloat;
            }

        }

        public TilePosition(int x, int y, int z, float zFloat = 0.0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.zFloat = zFloat;
        }

        public TilePosition(Vector2 pos, float zFloat = 0.0f)
        {
            x = (int)pos.X;
            y = (int)pos.Y;
            z = (int)zFloat;
            this.zFloat = zFloat;
        }

        public static TilePosition operator -(TilePosition pos1, TilePosition pos2)
        {
            return new TilePosition(pos1.x - pos2.x, pos1.y - pos2.y, pos1.z - pos2.z);
        }

        public static TilePosition operator +(TilePosition pos1, TilePosition pos2)
        {
            return new TilePosition(pos1.x + pos2.x, pos1.y + pos2.y, pos1.z + pos2.z);
        }

        public static TilePosition operator *(int i, TilePosition pos2)
        {
            return new TilePosition(i * pos2.x, i * pos2.y, i * pos2.z);
        }

        public static TilePosition operator /(int i, TilePosition pos2)
        {
            return new TilePosition(pos2.x / i, pos2.y / i, pos2.z / i);
        }
    }
}
