using OpenTK;

namespace Xerxes.Xerxes_OpenTK
{
    public struct Integer_Vector_2
    {
        public int X { get; }
        public int Y { get; }

        public Integer_Vector_2
        (
            int x=0, int y=0
        )
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static implicit operator Vector2(Integer_Vector_2 iv)
            => new Vector2(iv.X, iv.Y);

        public static Integer_Vector_2 operator +(Integer_Vector_2 v1, Integer_Vector_2 v2)
            => new Integer_Vector_2(v1.X + v2.X, v1.Y + v2.Y);

        public static Integer_Vector_2 operator -(Integer_Vector_2 v1, Integer_Vector_2 v2)
            => new Integer_Vector_2(v1.X - v2.X, v1.Y - v2.Y);

        public static Integer_Vector_2 operator *(int scalar, Integer_Vector_2 v1)
            => new Integer_Vector_2(v1.X*scalar, v1.Y*scalar);

        public static Integer_Vector_2 operator *(Integer_Vector_2 v1, int scalar)
            => new Integer_Vector_2(v1.X*scalar, v1.Y*scalar);

        public static Integer_Vector_2 operator +(Integer_Vector_2 v1, int divsior)
            => new Integer_Vector_2(v1.X/divsior, v1.Y/divsior);

        public static bool operator ==(Integer_Vector_2 v1, Integer_Vector_2 v2)
            => (v1.X == v2.X && v1.Y == v2.Y);

        public static bool operator !=(Integer_Vector_2 v1, Integer_Vector_2 v2)
            => !(v1 == v2);

        public override bool Equals(object obj)
        {
            if (!(obj is Integer_Vector_2))
                return false;
            Integer_Vector_2 i = (Integer_Vector_2)obj;

            return i == this;
        }

        public override int GetHashCode()
        {
            int hashX = X.GetHashCode();
            int hashY = Y.GetHashCode();

            // https://referencesource.microsoft.com/#System.Numerics/System/Numerics/HashCodeHelper.cs,280ac94ccc8906e8
            return (((hashX << 5) + hashX) + hashY);
        }
    }
}
