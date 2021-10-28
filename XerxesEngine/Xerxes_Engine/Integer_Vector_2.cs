namespace Xerxes_Engine
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

        public static Integer_Vector_2 operator +(Integer_Vector_2 v1, Integer_Vector_2 v2)
            => new Integer_Vector_2(v1.X + v2.X, v1.Y + v2.Y);
    }
}
