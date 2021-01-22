using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Systems
{
    public class MathHelper
    {
        public static bool IsBounded_XYZ(Vector3 subjectVector, Vector3 lowerBound, Vector3 upperBound)
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

        public static bool IsBounded_XY(Vector3 subjectVector, Vector3 lowerBound, Vector3 upperBound)
        {
            return (

                subjectVector.X > lowerBound.X &&
                subjectVector.Y > lowerBound.Y &&

                subjectVector.X < upperBound.X &&
                subjectVector.Y < upperBound.Y

                );
        }

        public static int MapEven(int n)
        {
            return 2 * n;
        }

        public static int MapOdd(int n)
        {
            return MapEven(n) + 1;
        }

        public static int MapPositive(int n)
        {
            if (n == 0)
                return 0;
            return (2 * Math.Abs(n)) + ((n + Math.Abs(n)) / (2 * n));
        }

        public static int MapCoordsToUniqueInteger(int x, int y)
        {
            int index = 4 * MapPositive(x) + MapPositive(y);
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
        public static float MapCoordsToUniqueFloat(int x, int y)
        {
            return 2920 * (float)Math.Sin(x * 21942 + y * 171324 + 8912) * (float)Math.Cos(x * 23157 * y * 217832 + 9758);
        }

        public static int StrideToIndex(int n, int stride)
        {
            return ((stride - 1) * (n + 1)) + n;
        }

        /// <summary>
        /// Used a lot for finding the angle. With 0rad being on  the x axis
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y_or_z"></param>
        /// <returns></returns>
        public static float GetAngle(float x, float y_or_z, float offset=0f)
        {
            float angle = (float)(Math.Atan2(x,y_or_z) / Math.PI * 180f) + offset;
            if (angle < 0)
                angle += 360f;
            return angle;
        }

        public static float GetAngle(Vector2 position, float offset = 0f)
        {
            return GetAngle(position.X, position.Y, offset);
        }

        public static bool FormsTriangle(float side1, float side2, float side3)
        {
            return
                (
                side1 + side2 > side3 &&
                side1 + side3 > side2 &&
                side2 + side3 > side1
                );
        }

        public static float Euler_To_Radian(float thetaEuler)
        {
            return (float)(thetaEuler * Math.PI / 180f);
        }

        public static float Radian_To_Euler(float thetaRadian)
        {
            return (float)(thetaRadian / Math.PI * 180f);
        }
    }
}
