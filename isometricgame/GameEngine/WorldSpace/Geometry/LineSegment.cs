using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace.Geometry
{
    /// <summary>
    /// Describes a line segement at an arbitrary angle.
    /// </summary>
    public struct LineSegment
    {
        private float x, y;
        private float leftLength, rightLength;
        private float orientationAxisOffset;

        /// <summary>
        /// Theta is in radians.
        /// </summary>
        private Orientation theta;

        private float leftTheta, rightTheta;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Length => rightLength - leftLength;
        public float LeftLength => leftLength;
        public float RightLength => rightLength;
        public float OrientationAxisOffset => orientationAxisOffset;
        public Orientation Theta { get => theta; set => theta = value; }
        public float LeftTheta => leftTheta;
        public float RightTheta => rightTheta;

        public LineSegment(float leftLength = 0.5f, float rightLength = 0.5f, float orientationAxisOffset = 0.5f, float theta = 0, float x = 0, float y = 0)
        {
            this.leftLength = -leftLength;
            this.rightLength = rightLength;
            this.orientationAxisOffset = orientationAxisOffset;
            this.theta = new Orientation(theta);
            this.x = x;
            this.y = y;

            if (orientationAxisOffset == 0)
            {
                leftTheta = 0;
                rightTheta = (float)Math.PI;
            }
            else
            {
                leftTheta = (float)(Math.Atan(leftLength / orientationAxisOffset));
                rightTheta = (float)(Math.Atan(rightLength / orientationAxisOffset));
            }
        }

        public static bool IsBehindFace(LineSegment seg, Vector2 offset, Vector2 opposingPoint)
        {
            Vector2 deltaPos = opposingPoint - (new Vector2(seg.x, seg.y) - offset);
            float bVal = (float)((deltaPos.X * Math.Cos(seg.theta)) - (deltaPos.Y * Math.Sin(seg.theta)));

            return bVal < seg.OrientationAxisOffset;
        }

        public static LineSegment FromEulerAngle(float theta, float leftLength= 0.5f, float rightLength = 0.5f, float orientationAxisOffset = 0.5f, float x=0, float y=0)
        {
            return new LineSegment
                (
                leftLength,
                rightLength,
                orientationAxisOffset,
                (float)((theta / 180f) * Math.PI),
                x,
                y
                );
        }

        /// <summary>
        /// Gets the edge (x,y) positions of a given line segment. Tuple(lowerX, lowerY, higherX, higherY)
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public static Tuple<float,float,float,float> GetEdgePositions(LineSegment segment, float offsetX=0, float offsetY=0)
        {
            Vector2 left, right;

            if (segment.orientationAxisOffset == 0) //This segment is not offset in its orientation around origin. Using the abritary equations will result in 0 in denom.
            {
                left = new Vector2
                    (
                    getX_dist0(offsetX, segment.leftLength, segment.theta),
                    getY_dist0(offsetY, segment.leftLength, segment.theta)
                    );
                right = new Vector2
                    (
                    getX_dist0(offsetX, segment.rightLength, segment.theta),
                    getY_dist0(offsetY, segment.rightLength, segment.theta)
                    );
            }
            else
            {
                left = new Vector2
                    (
                    getX(offsetX, segment.leftLength, segment.orientationAxisOffset, segment.theta),
                    getY(offsetY, segment.leftLength, segment.orientationAxisOffset, segment.theta)
                    );
                right = new Vector2
                    (
                    getX(offsetX, segment.rightLength, segment.orientationAxisOffset, segment.theta),
                    getY(offsetY, segment.rightLength, segment.orientationAxisOffset, segment.theta)
                    );
            }

            float lowerX, lowerY, higherX, higherY;
            if (left.X < right.X)
            {
                lowerX = left.X;
                higherX = right.X;
            }
            else
            {
                lowerX = right.X;
                higherX = left.X;
            }

            if (left.Y < right.Y)
            {
                lowerY = left.Y;
                higherY = right.Y;
            }
            else
            {
                lowerY = right.Y;
                higherY = left.Y;
            }

            return new Tuple<float, float, float, float>(lowerX, lowerY, higherX, higherY);
        }


        /// <summary>
        /// Determines if two line segments are intersecting. Outs the collision point. Default (0,0) if NOT intersecting.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="collisionPoint"></param>
        /// <param name="offsetX0"></param>
        /// <param name="offsetY0"></param>
        /// <param name="offsetX1"></param>
        /// <param name="offsetY1"></param>
        /// <returns></returns>
        public static bool IsIntersecting(LineSegment a, LineSegment b, out Vector2 collisionPoint, float offsetX0=0, float offsetY0=0, float offsetX1=0, float offsetY1=0)
        {
            Tuple<float, float, float, float> edgesA, edgesB;
            edgesA = GetEdgePositions(a, offsetX0, offsetY0);
            edgesB = GetEdgePositions(b, offsetX1, offsetY1);

            collisionPoint = new Vector2();

            float denominator = (float)(Math.Tan(a.theta) - Math.Tan(b.theta));
            if (denominator == 0)
            {
                return false;
            }

            float cx = (float)(
                (offsetX0 * Math.Tan(b.theta)) -
                (offsetX1 * Math.Tan(a.theta)) -
                offsetY1 + offsetY0
                ) / denominator;


            float cy = (float)(
                (offsetY1 * Math.Tan(a.theta)) -
                (offsetY0 * Math.Tan(b.theta)) +
                ((offsetX0 - offsetX1) * (Math.Tan(b.theta) * Math.Tan(a.theta)))
                ) / -denominator;

            if (
                edgesA.Item1 <= cx && edgesA.Item3 >= cx &&
                edgesA.Item2 <= cy && edgesA.Item4 >= cy &&
                edgesB.Item1 <= cx && edgesB.Item3 >= cx &&
                edgesB.Item2 <= cy && edgesB.Item4 >= cy
                )
            {
                collisionPoint = new Vector2(cx, cy);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Returns true or false if the target position is within the segment's rotational angles.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="targetPosition"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public static bool IsFacing(LineSegment a, Vector2 targetPosition, float offsetX=0, float offsetY=0)
        {
            float angle = Services.MathHelper.GetAngle(targetPosition - new Vector2(a.x+offsetX, a.y+offsetY));
            return (a.leftTheta <= angle && a.rightTheta >= angle);
        }



        private static float getX_dist0(float x_o, float k, float theta)
        {
            return (float)(-k * Math.Sin(theta)) + x_o;
        }

        private static float getY_dist0(float y_o, float k, float theta)
        {
            return (float)(k * Math.Cos(theta)) + y_o;
        }

        private static float getX(float x_o, float k, float b, float theta)
        {
            return (float)(
                b /
                (
                Math.Sin(theta) * (Math.Tan(theta+Math.Atan(k/b))-Math.Tan(theta + Math.PI/2))
                )
                ) + x_o;
        }

        private static float getY(float y_o, float k, float b, float theta)
        {
            return (float)(
                (
                b * Math.Tan(theta + Math.Atan(k/b))
                )/(
                Math.Sin(theta) * (
                Math.Tan(theta + Math.PI/2) - Math.Tan(theta + Math.Atan(k/b))
                )
                )
                ) + y_o;
        }
    }
}
