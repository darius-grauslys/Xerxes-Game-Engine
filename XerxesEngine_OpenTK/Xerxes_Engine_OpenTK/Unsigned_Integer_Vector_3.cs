
namespace Xerxes.Xerxes_OpenTK
{
    public struct Unsigned_Integer_Vector_3
    {
        public uint uX { get; }
        public uint uY { get; }
        public uint uZ { get; }

        public Unsigned_Integer_Vector_3
        (
            uint x = 0,
            uint y = 0,
            uint z = 0
        )
        {
            uX = x;
            uY = y;
            uZ = z;
        }

        public override string ToString()
            => $"({uX}, {uY}, {uZ})";

        public static implicit operator OpenTK.Vector3(Unsigned_Integer_Vector_3 iv3)
            => new OpenTK.Vector3(iv3.uX, iv3.uY, iv3.uZ);

        public static Unsigned_Integer_Vector_3 operator +(Unsigned_Integer_Vector_3 v1, Unsigned_Integer_Vector_3 v2)
            => new Unsigned_Integer_Vector_3(v1.uX + v2.uX, v1.uY + v2.uY, v1.uZ + v2.uZ);

        public static Unsigned_Integer_Vector_3 operator -(Unsigned_Integer_Vector_3 v1, Unsigned_Integer_Vector_3 v2)
            => new Unsigned_Integer_Vector_3(v1.uX - v2.uX, v1.uY - v2.uY, v1.uZ - v2.uZ);

        public static Unsigned_Integer_Vector_3 operator *(uint scalar, Unsigned_Integer_Vector_3 v1)
            => new Unsigned_Integer_Vector_3(v1.uX*scalar, v1.uY*scalar, v1.uZ*scalar);

        public static Unsigned_Integer_Vector_3 operator *(Unsigned_Integer_Vector_3 v1, uint scalar)
            => new Unsigned_Integer_Vector_3(v1.uX*scalar, v1.uY*scalar, v1.uZ*scalar);

        public static Unsigned_Integer_Vector_3 operator /(Unsigned_Integer_Vector_3 v1, uint divsior)
            => new Unsigned_Integer_Vector_3(v1.uX/divsior, v1.uY/divsior, v1.uZ/divsior);

        public static bool operator ==(Unsigned_Integer_Vector_3 v1, Unsigned_Integer_Vector_3 v2)
            => (v1.uX == v2.uX && v1.uY == v2.uY && v1.uZ == v2.uZ);

        public static bool operator !=(Unsigned_Integer_Vector_3 v1, Unsigned_Integer_Vector_3 v2)
            => !(v1 == v2);

        public override bool Equals(object obj)
        {
            if (!(obj is Unsigned_Integer_Vector_3))
                return false;
            Unsigned_Integer_Vector_3 i = (Unsigned_Integer_Vector_3)obj;

            return i == this;
        }

        public override int GetHashCode()
        {
            int hashX = uX.GetHashCode();
            int hashY = uY.GetHashCode();
            int hashZ = uZ.GetHashCode();

            int hashXY =
                (hashX << 5) + hashX + hashY;
            int hashXYZ =
                (hashXY << 5) + hashXY + hashZ;

            // https://referencesource.microsoft.com/#System.Numerics/System/Numerics/HashCodeHelper.cs,280ac94ccc8906e8
            return hashXYZ;
        }
    }
}
