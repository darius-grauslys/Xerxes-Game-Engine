
using OpenTK;

namespace Xerxes.Xerxes_OpenTK
{
    public struct Unsigned_Integer_Vector_2
    {
        public uint uX { get; }
        public uint uY { get; }

        public Unsigned_Integer_Vector_2
        (
            uint x=0, uint y=0
        )
        {
            uX = x;
            uY = y;
        }

        public override string ToString()
        {
            return $"({uX}, {uY})";
        }

        public static explicit operator Integer_Vector_2(Unsigned_Integer_Vector_2 iv)
            => new Integer_Vector_2((int)iv.uX, (int)iv.uY);

        public static implicit operator Vector2(Unsigned_Integer_Vector_2 iv)
            => new Vector2(iv.uX, iv.uY);

        public static Unsigned_Integer_Vector_2 operator +(Unsigned_Integer_Vector_2 v1, Unsigned_Integer_Vector_2 v2)
            => new Unsigned_Integer_Vector_2(v1.uX + v2.uX, v1.uY + v2.uY);

        public static Unsigned_Integer_Vector_2 operator -(Unsigned_Integer_Vector_2 v1, Unsigned_Integer_Vector_2 v2)
            => new Unsigned_Integer_Vector_2(v1.uX - v2.uX, v1.uY - v2.uY);

        public static Unsigned_Integer_Vector_2 operator *(uint scalar, Unsigned_Integer_Vector_2 v1)
            => new Unsigned_Integer_Vector_2(v1.uX*scalar, v1.uY*scalar);

        public static Unsigned_Integer_Vector_2 operator *(Unsigned_Integer_Vector_2 v1, uint scalar)
            => new Unsigned_Integer_Vector_2(v1.uX*scalar, v1.uY*scalar);

        public static Unsigned_Integer_Vector_2 operator /(Unsigned_Integer_Vector_2 v1, uint divsior)
            => new Unsigned_Integer_Vector_2(v1.uX/divsior, v1.uY/divsior);

        public static bool operator ==(Unsigned_Integer_Vector_2 v1, Unsigned_Integer_Vector_2 v2)
            => (v1.uX == v2.uX && v1.uY == v2.uY);

        public static bool operator !=(Unsigned_Integer_Vector_2 v1, Unsigned_Integer_Vector_2 v2)
            => !(v1 == v2);

        public override bool Equals(object obj)
        {
            if (!(obj is Unsigned_Integer_Vector_2))
                return false;
            Unsigned_Integer_Vector_2 i = (Unsigned_Integer_Vector_2)obj;

            return i == this;
        }

        public override int GetHashCode()
        {
            int hashX = uX.GetHashCode();
            int hashY = uY.GetHashCode();

            // https://referencesource.microsoft.com/#System.Numerics/System/Numerics/HashCodeHelper.cs,280ac94ccc8906e8
            return (((hashX << 5) + hashX) + hashY);
        }
    }
}
