using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XerxesEngine.WorldSpace.ChunkSpace
{
    public struct IntegerPosition
    {
        int x, y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public IntegerPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(IntegerPosition position) => position.x == x && position.y == y;

        public override string ToString()
        {
            return String.Format("{0}, {1}", x, y);
        }

        public static bool operator ==(IntegerPosition pos1, IntegerPosition pos2)
        {
            return pos1.Equals(pos2);
        }

        public static bool operator !=(IntegerPosition pos1, IntegerPosition pos2)
        {
            return !pos1.Equals(pos2);
        }

        public static IntegerPosition operator -(IntegerPosition pos1, IntegerPosition pos2)
        {
            return new IntegerPosition(pos1.x - pos2.x, pos1.y - pos2.y);
        }

        public static IntegerPosition operator +(IntegerPosition pos1, IntegerPosition pos2)
        {
            return new IntegerPosition(pos1.x + pos2.x, pos1.y + pos2.y);
        }

        public static IntegerPosition operator *(IntegerPosition pos1, int scalar)
        {
            return new IntegerPosition(pos1.x * scalar, pos1.y * scalar);
        }

        public static implicit operator IntegerPosition(Vector2 pos) => new IntegerPosition((int)Math.Floor(pos.X), (int)Math.Floor(pos.Y));
        public static implicit operator Vector2(IntegerPosition pos) => new Vector2(pos.X, pos.Y);
    }
}
