
namespace Xerxes_Engine.Export_OpenTK
{
    public struct Integer_Vector_3 
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Integer_Vector_3
        (
            int x = 0,
            int y = 0,
            int z = 0
        )
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
            => $"({X}, {Y}, {Z})";

        public static implicit operator OpenTK.Vector3(Integer_Vector_3 iv3)
            => new OpenTK.Vector3(iv3.X, iv3.Y, iv3.Z);

        public static Integer_Vector_3 operator +(Integer_Vector_3 v1, Integer_Vector_3 v2)
            => new Integer_Vector_3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

        public static Integer_Vector_3 operator -(Integer_Vector_3 v1, Integer_Vector_3 v2)
            => new Integer_Vector_3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

        public static Integer_Vector_3 operator *(int scalar, Integer_Vector_3 v1)
            => new Integer_Vector_3(v1.X*scalar, v1.Y*scalar, v1.Z*scalar);

        public static Integer_Vector_3 operator *(Integer_Vector_3 v1, int scalar)
            => new Integer_Vector_3(v1.X*scalar, v1.Y*scalar, v1.Z*scalar);

        public static Integer_Vector_3 operator /(Integer_Vector_3 v1, int divsior)
            => new Integer_Vector_3(v1.X/divsior, v1.Y/divsior, v1.Z/divsior);

        public static bool operator ==(Integer_Vector_3 v1, Integer_Vector_3 v2)
            => (v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z);

        public static bool operator !=(Integer_Vector_3 v1, Integer_Vector_3 v2)
            => !(v1 == v2);

        public override bool Equals(object obj)
        {
            if (!(obj is Integer_Vector_3))
                return false;
            Integer_Vector_3 i = (Integer_Vector_3)obj;

            return i == this;
        }

        public override int GetHashCode()
        {
            int hashX = X.GetHashCode();
            int hashY = Y.GetHashCode();
            int hashZ = Z.GetHashCode();

            int hashXY =
                (hashX << 5) + hashX + hashY;
            int hashXYZ =
                (hashXY << 5) + hashXY + hashZ;

            // https://referencesource.microsoft.com/#System.Numerics/System/Numerics/HashCodeHelper.cs,280ac94ccc8906e8
            return hashXYZ;
        }
    }
}
