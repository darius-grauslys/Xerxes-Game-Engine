using System;
using System.Diagnostics;
using System.Drawing;
using OpenTK;

namespace isometricgame.GameEngine.Tools
{
    public class MathHelper
    {
        public static readonly Vector2 MAX_VECTOR2_SQUARED = new Vector2(1844674400000000000f, 1844674400000000000f);

        public static float Get__Hypotenuse(Vector2 vec2)
            => Vector2.Distance(vec2, Vector2.Zero);

        public static float Get__Hypotenuse(float x, float y)
            => Get__Hypotenuse(new Vector2(x, y));
        
        public static Vector3 Get__Hadamard_Product(Vector3 vec1, Vector3 vec2)
            => new Vector3(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z);

        public static Vector2 Get__Hadamard_Product(Vector2 vec1, Vector2 vec2)
            => new Vector2(vec1.X * vec2.X, vec1.Y * vec2.Y);

        public static Vector3 Get__Hadamard_Inverse(Vector3 toInvert)
            => new Vector3(1 / toInvert.X, 1 / toInvert.Y, 1 / toInvert.Z);

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
        
        public static float Get__Safe_Inverse(float value, float safeReturn = 0)
            => (value == 0) ? safeReturn : 1 / value;
        
        public static bool CheckIf__Bounded_XYZ_Exclusive(Vector3 subjectVector, Vector3 lowerBound, Vector3 upperBound)
        {
            return (
                
                subjectVector.X > lowerBound.X &&
                subjectVector.Y > lowerBound.Y &&
                subjectVector.Z > lowerBound.Z &&

                subjectVector.X < upperBound.X &&
                subjectVector.Y < upperBound.Y &&
                subjectVector.Z < upperBound.Z
                
                );
        }

        public static bool CheckIf__Bounded_XYZ_Inclusive(Vector3 subjectVector, Vector3 lowerBound, Vector3 upperBound)
        {
            return (
                
                subjectVector.X >= lowerBound.X &&
                subjectVector.Y >= lowerBound.Y &&
                subjectVector.Z >= lowerBound.Z &&

                subjectVector.X <= upperBound.X &&
                subjectVector.Y <= upperBound.Y &&
                subjectVector.Z <= upperBound.Z
                
            );
        }
        
        public static bool CheckIf__Bounded_XY0_Exclusive(Vector3 subjectVector, Vector3 lowerBound, Vector3 upperBound)
        {
            return (

                subjectVector.X > lowerBound.X &&
                subjectVector.Y > lowerBound.Y &&

                subjectVector.X < upperBound.X &&
                subjectVector.Y < upperBound.Y

                );
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

        public static float Calculate__Area(Vector2 vec)
            => vec.X * vec.Y;

        /// <summary>
        /// Returns null if either X or Y is zero.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static float? Calculate__Safe_Area(Vector2 vec)
        {
            if (vec.X == 0 || vec.Y == 0)
                return null;
            return Calculate__Area(vec);
        }
        
        public static float Calculate__Area_Ratio(Vector2 vec1, Vector2 vec2)
            => Calculate__Area(vec1) / Calculate__Area(vec2);

        public static float Calculate__Safe_Area_Ratio(Vector2 vec1, Vector2 vec2)
            => (Calculate__Area(vec1) / Calculate__Safe_Area(vec2)) ?? 0;
        
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

        public static bool CheckIf__Obeys_IClamp(int val, int min, int max)
            => (val >= min) && (val <= max);

        public static bool CheckIf__Obeys_Clamp(float val, float min, float max)
            //Not checking equality for precision errors.
            => !(val < min) && !(val > max);
        
        public static float Clamp__Float(float val, float min, float max)
            => (val < min) ? min : ((val > max) ? max : val);

        public static int Clamp__Integer(int val, int min, int max)
            => (val < min) ? min : ((val > max) ? max : val);

        public static int Clamp__UInteger(int val, int max = int.MaxValue)
            => Clamp__Integer(val, 0, max);
        
        public static uint Clamp__UInteger_As_Uint(int val, int max = int.MaxValue)
            => (uint)Clamp__UInteger(val, max);
        
        public static float Clamp__UFloat(float val, float max = float.MaxValue)
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
