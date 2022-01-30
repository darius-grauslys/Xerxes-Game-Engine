using System;
using System.Drawing;
using OpenTK;

namespace Xerxes_Engine.Tools
{
    public class Math_Helper
    {
        public const float FLOAT__MINIMUM__PERCISION = 0.0001f;
        
        public static readonly Vector2 MAX_VECTOR2_SQUARED = new Vector2(1844674400000000000f, 1844674400000000000f);

        public static float Get__Hypotenuse(Vector3 vec3)
            => Vector2.Distance(vec3.Xy, Vector2.Zero);
        
        public static float Get__Hypotenuse(Vector2 vec2)
            => Vector2.Distance(vec2, Vector2.Zero);

        public static float Get__Hypotenuse(float x, float y)
            => Get__Hypotenuse(new Vector2(x, y));

        public static Vector3 Get__Safe_Normalized(Vector3 vec)
        {
            if (vec == Vector3.Zero)
                return Vector3.Zero;

            return vec.Normalized();
        }

        public static Vector2 Get__Safe_Normalized(Vector2 vec)
        {
            if (vec == Vector2.Zero)
                return Vector2.Zero;

            return vec.Normalized();
        }

        public static Vector2 Get__Stepped(Vector2 vec)
        {
            float x = vec.X == 0 ? 0 : 1;
            float y = vec.Y == 0 ? 0 : 1;
            return new Vector2(x,y);
        }
        
        public static Vector3 Get__Hadamard_Product(Vector3 vec1, Vector3 vec2)
            => new Vector3(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z);

        public static Vector2 Get__Hadamard_Product(Vector2 vec1, Vector2 vec2)
            => new Vector2(vec1.X * vec2.X, vec1.Y * vec2.Y);

        public static Vector3 Get__Hadamard_Inverse(Vector3 toInvert)
            => new Vector3(1 / toInvert.X, 1 / toInvert.Y, 1 / toInvert.Z);

        public static Vector3 Convert__Scalar_To_X_Vector3(float scalar)
            => new Vector3(scalar, 0, 0);
        public static Vector3 Convert__Scalar_To_Y_Vector3(float scalar)
            => new Vector3(0, scalar, 0);
        public static Vector3 Convert__Scalar_To_Z_Vector3(float scalar)
            => new Vector3(0, 0, scalar);

        public static Vector3 Extract__X_From_Vector3(Vector3 vec)
            => Convert__Scalar_To_X_Vector3(vec.X);
        public static Vector3 Extract__Y_From_Vector3(Vector3 vec)
            => Convert__Scalar_To_Y_Vector3(vec.Y);
        public static Vector3 Extract__Z_From_Vector3(Vector3 vec)
            => Convert__Scalar_To_Z_Vector3(vec.Z);
        
        public static Vector3 Get__Safe_Hadamard_Inverse(Vector3 toInvertSafely, float safeReturn = 0)
            => new Vector3
            (
                Get__Safe_Inverse(toInvertSafely.X, safeReturn),
                Get__Safe_Inverse(toInvertSafely.Y, safeReturn),
                Get__Safe_Inverse(toInvertSafely.Z, safeReturn)
            );

        public static Vector2 Get__Safe_Hadamard_Inverse(Vector2 toInvertSafely, float safeReturn = 0)
            => new Vector2
            (
                Get__Safe_Inverse(toInvertSafely.X, safeReturn),
                Get__Safe_Inverse(toInvertSafely.Y, safeReturn)
            );

        public static Vector3 Get__VecAB__As__VecAxByZ0(Vector3 a, Vector3 b)
            => new Vector3(a.X, b.Y, 0);
        public static Vector3 Get__VecAB__As__VecBxAyZ0(Vector3 a, Vector3 b)
            => new Vector3(b.X, a.Y, 0);
        
        public static bool CheckIf__Vec_Is_Bounded__Forth_Quadrant(Vector3 vec, Vector3 v1, Vector3 v2)
        {
            bool xBounded = Tolerable__LessThanEqual__Float(v1.X, vec.X) 
                            && 
                            Tolerable__GreaterThanEqual__Float(v2.X, vec.X);
            bool yBounded = Tolerable__LessThanEqual__Float(v2.Y, vec.Y) 
                            && 
                            Tolerable__GreaterThanEqual__Float(v1.Y, vec.Y);

            return xBounded && yBounded;
        }
        
        public static float Get__Safe_Inverse(float value, float safeReturn = 0)
            => (value == 0) ? safeReturn : 1 / value;

        public static void Compare__Magnitude(int a, int b, out int smaller, out int larger)
        {
            if (a <= b)
            {
                smaller = a;
                larger = b;
                return;
            }

            smaller = b;
            larger = a;
            return;
        }

        public static int Map__Even(int n)
        {
            return 2 * n;
        }

        public static int Map__Odd(int n)
        {
            return Map__Even(n) + 1;
        }

        public static int Map__Positive(int n)
        {
            if (n == 0)
                return 0;
            return (2 * Math.Abs(n)) + ((n + Math.Abs(n)) / (2 * n));
        }

        public static int Map__Coordinates_To_Unique_Integer(int x, int y)
        {
            int index = 4 * Map__Positive(x) + Map__Positive(y);
            int quadSel = index % 4;

            if (quadSel == 0)
                return (4 * index) + 1;
            else if (quadSel == 1)
                return 2 * ((2 * index) + 1);
            else if (quadSel == 2)
                return (4 * index) + 3;
            else
                return 4 * index;
        }

        //This is simply genius. Props to wiki
        public static float Map__Coordinates_To_Unique_Float(int x, int y)
        {
            return 2920 * (float)Math.Sin(x * 21942 + y * 171324 + 8912) * (float)Math.Cos(x * 23157 * y * 217832 + 9758);
        }

        public static int Stride__To_Index(int n, int stride)
        {
            return ((stride - 1) * (n + 1)) + n;
        }

        /// <summary>
        /// Used a lot for finding the angle. With 0rad being on  the x axis
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y_or_z"></param>
        /// <returns></returns>
        public static float Get__Angle(float x, float y_or_z, float offset=0f)
        {
            float angle = (float)(Math.Atan2(x,y_or_z) / Math.PI * 180f) + offset;
            if (angle < 0)
                angle += 360f;
            return angle;
        }

        public static float Get__Angle(Vector2 position, float offset = 0f)
        {
            return Get__Angle(position.X, position.Y, offset);
        }

        public static bool CheckIf__Forms_Triangle(float side1, float side2, float side3)
        {
            return
                (
                side1 + side2 > side3 &&
                side1 + side3 > side2 &&
                side2 + side3 > side1
                );
        }

        /// <summary>
        /// Returns 0 if the param is null.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="nullableVec"></param>
        /// <returns></returns>
        public static float Get__Safe_Distance
        (
            Vector2 vec,
            Vector2? nullableVec
        )
        {
            if (nullableVec == null)
                return 0;
            return Vector2.Distance
            (
                vec,
                (Vector2) nullableVec
            );
        }

        /// <summary>
        /// Returns 0 if the param is null.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="nullableVec"></param>
        /// <returns></returns>
        public static float Get__Safe_Distance
        (
            Vector3 vec,
            Vector3? nullableVec
        )
        {
            if (nullableVec == null)
                return 0;
            return Vector3.Distance
            (
                vec,
                (Vector3) nullableVec
            );
        }
        
        public static float Calculate__Area(Vector2 vec)
            => Calculate__Product(vec.X, vec.Y);
        public static float Calculate__Product(float x1, float y1)
            => x1 * y1;

        /// <summary>
        /// Returns null if either X or Y is zero.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static float? Calculate__Safe_Area(Vector2 vec)
            => Calculate__Safe_Product(vec.X, vec.Y);
        public static float? Calculate__Safe_Product
        (
            float x1, 
            float y1
        )
        {
            if (x1 == 0 || y1 == 0)
                return null;
            return Calculate__Product(x1, y1);
        }
        
        public static float Calculate__Area_Ratio(Vector2 vec1, Vector2 vec2)
            => Calculate__Area_Ratio(vec1.X, vec1.Y, vec2.X, vec2.Y);
        public static float Calculate__Area_Ratio(float x1, float y1, float x2, float y2)
            => Calculate__Product(x1, y1) / Calculate__Product(x2, y2);

        public static float Calculate__Safe_Area_Ratio(Vector2 vec1, Vector2 vec2)
            => Calculate__Safe_Area_Ratio(vec1.X, vec1.Y, vec2.X, vec2.Y);
        public static float Calculate__Safe_Area_Ratio(float x1, float y1, float x2, float y2)
            => (Calculate__Product(x1, y1) / Calculate__Safe_Product(x2, y2)) ?? 0;
        
        public static bool CheckIf__Greater_Area(Vector2 isBigger, Vector2 thanThis)
            => Calculate__Area(isBigger) > Calculate__Area(thanThis);
        
        public static float Convert__Euler_To_Radian(float thetaEuler)
        {
            return (float)(thetaEuler * Math.PI / 180f);
        }

        public static float Convert__Radian_To_Euler(float thetaRadian)
        {
            return (float)(thetaRadian / Math.PI * 180f);
        }

        public static Vector4 Convert__Color_To_Vec4(Color color)
            => new Vector4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);

        public static bool Tolerable__Equality__Float(float f1, float f2, float tolerance=FLOAT__MINIMUM__PERCISION)
        {
            float diff = f1 - f2;

            return diff <= tolerance && diff >= -tolerance;
        }

        public static bool Tolerable__LessThan__Float(float f1, float f2, float tolerance=FLOAT__MINIMUM__PERCISION)
        {
            float diff = f1 - f2;

            return diff <= -tolerance;
        }

        public static bool Tolerable__GreaterThan__Float(float f1, float f2, float tolerance=FLOAT__MINIMUM__PERCISION)
        {
            float diff = f1 - f2;

            return diff >= tolerance;
        }

        public static bool Tolerable__LessThanEqual__Float(float f1, float f2, float tolerance=FLOAT__MINIMUM__PERCISION)
        {
            float diff = f1 - f2;

            return diff <= tolerance;
        }

        public static bool Tolerable__GreaterThanEqual__Float(float f1, float f2, float tolerance=FLOAT__MINIMUM__PERCISION)
        {
            float diff = f1 - f2;

            return diff >= -tolerance;
        }
        
        /// <summary>
        /// Same as Divides, but returns true if the remainder is
        /// below the minimum percision.
        /// </summary>
        public static bool Tolerable__Divides(float a, float b)
            => (Math.Abs(b) % Math.Abs(a)) <= FLOAT__MINIMUM__PERCISION;
        /// <summary>
        /// Checks to see if a divides b - no remainder.
        /// </summary>
        public static bool Divides(float a, float b)
            => (Math.Abs(b) % Math.Abs(a)) == 0;

        /// <summary>
        /// Returns true if the value is between min and max inclusively on both arguments.
        /// </summary>
        public static bool Check_If__Obeys_Inclusive_Clamp__Integer(int val, int min, int max)
            => (val >= min) && (val <= max);
        /// <summary>
        /// Returns true if the value is between min and max exclusively on both arguments.
        /// </summary>
        public static bool Check_If__Obeys_Exclusive_Clamp__Integer(int val, int min, int max)
            => (val > min) && (val < max);
        /// <summary>
        /// Returns true if the value is between min-inclusively and max-exclusively.
        /// </summary>
        public static bool Check_If__Obeys_Range_Clamp__Integer(int val, int min, int max)
            => (val >= min) && (val < max);
        /// <summary>
        /// Equvialent to Inclusive_Clamp, but assumes min is zero.
        /// </summary>
        public static bool Check_If__Obeys_Inclusive_Clamp__Positive_Integer(int val, int max = int.MaxValue)
            => (val >= 0) && (val <= max);
        /// <summary>
        /// Equvialent to Range_Clamp, but assumes min is zero.
        /// </summary>
        public static bool Check_If__Obeys_Range_Clamp__Positive_Integer(int val, int max)
            => (val >= 0) && (val < max);

        public static bool Check_If__Obeys_Clamp__Positive_Float(float val, float max = float.MaxValue)
            => Check_If__Obeys_Clamp(val, 0, max);
        public static bool Check_If__Obeys_Clamp(float val, float min, float max)
            //Not checking equality for precision errors.
            => !(val < min) && !(val > max);

        public static bool Check_If__Obeys_Safe_Clamp__Positive_Integer
        (
            int? val,
            int max,
            out bool valWasNotNull_ButDidNotClamp
        )
            => Check_If__Obeys_Safe_Clamp__Integer(val, 0, max, out valWasNotNull_ButDidNotClamp);
        public static bool Check_If__Obeys_Safe_Clamp__Integer
        (
            int? val,
            int min,
            int max,
            out bool valWasNotNull_ButDidNotClamp
        )
        {
            bool ret = (val >= min) && (val <= max);
            valWasNotNull_ButDidNotClamp =
                (val != null)
                &&
                !ret;
            return ret;
        }

        public static float Divide__Safely(float numerator, float denominator, float undefinedReturn = 0)
            => (denominator == 0) ? undefinedReturn : numerator / denominator;
        
        public static float Clamp__Float(float val, float min, float max)
            => (val < min) ? min : ((val > max) ? max : val);

        public static int Clamp__Integer
        (
            int val, 
            out bool isValValid, 
            int min=int.MinValue, 
            int max=int.MaxValue
        )
        {
            if (isValValid = val < min)
                return min;
            if (isValValid = val > max)
                return max;
            return val;
        }
        public static int Clamp__Integer(int val, int min=int.MinValue, int max=int.MaxValue)
            => (val < min) ? min : ((val > max) ? max : val);

        public static int Clamp__Positive_Integer(int val, out bool isValValid, int max = int.MaxValue)
            => Clamp__Integer(val, out isValValid, 0, max);
        public static int Clamp__Positive_Integer(int val, int max = int.MaxValue)
            => Clamp__Integer(val, 0, max);
        
        public static uint Clamp__Positive_Integer_As_Unsigned_Integer(int val, int max = int.MaxValue)
            => (uint)Clamp__Positive_Integer(val, max);
        
        public static float Clamp__Positive_Float(float val, float max = float.MaxValue)
            => Clamp__Float(val, 0, max);
        public static Vector2 Clamp__Vector2_UFloat(Vector2 vec, float max = float.MaxValue)
            => new Vector2(Clamp__Float(vec.X, 0, max), Clamp__Float(vec.Y, 0, max));
        
        public static float Clamp__Against_Minimum(float val, float min)
            => val < min ? min : val;

        public static float Clamp__Against_Maximum(float val, float max)
            => val > max ? max : val;


        public static double Clamp__Double(double val, double min, double max)
            => (val < min) ? min : ((val > max) ? max : val);

        public static double Clamp__Double__Against_Minimum(double val, double min)
            => val < min ? min : val;

        public static double Clamp__Double__Against_Maximum(double val, double max)
            => val > max ? max : val;
    }
}
