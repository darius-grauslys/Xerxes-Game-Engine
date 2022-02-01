
namespace Xerxes_Engine.Export_OpenTK.Tools
{
    public static class Math_Helper
    {
        public static int Distance_Squared(Integer_Vector_3 source, Integer_Vector_3 target)
        {
            int delta_x = target.X - source.X;
            int delta_y = target.Y - source.Y;
            int delta_z = target.Z - source.Z;

            return 
                (delta_x * delta_x)
                +
                (delta_y * delta_y)
                +
                (delta_z * delta_z);
        }

        public static Integer_Vector_3 Hadamard_Product(Integer_Vector_3 iv1, Integer_Vector_3 iv2)
        {
            return new Integer_Vector_3(iv1.X * iv2.X, iv1.Y * iv2.Y, iv1.Z * iv2.Z);
        }
    }
}
