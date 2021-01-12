using OpenTK;

namespace isometricgame.GameEngine.WorldSpace.Geometry
{
    public struct Region
    {
        private LineSegment[] segments;
        private float zHeight;

        public Region(LineSegment[] segments = null, float zHeight = 1)
        {
            this.zHeight = zHeight;
            if (segments == null)
            {
                this.segments = new LineSegment[]
                {
                    new LineSegment(),
                    LineSegment.FromEulerAngle(90),
                    LineSegment.FromEulerAngle(180),
                    LineSegment.FromEulerAngle(270)
                };
            }
            else
            {
                this.segments = segments;
            }
        }

        public void RotateRegion_Euler(float thetaEuler)
        {
            RotateRegion_Radian(Services.MathHelper.Euler_To_Radian(thetaEuler));
        }

        public void RotateRegion_Radian(float thetaRadian)
        {
            for (int i = 0; i < segments.Length; i++)
                segments[i].Theta += thetaRadian;
        }

        private static bool AnySegmentsFacing(Region r, Vector2 offset, Vector2 targetPosition, out LineSegment facingSegment)
        {
            LineSegment ret = r.segments[0];
            bool found = false;
            foreach (LineSegment seg in r.segments)
            {
                if (found = LineSegment.IsFacing(seg, targetPosition, offset.X, offset.Y))
                {
                    ret = seg;
                    break;
                }
            }
            facingSegment = ret;
            return found;
        }

        public static bool IsIntersecting(Region a, Region b, Vector3 aOffset, Vector3 bOffset, out Vector3 contactPoint)
        {
            LineSegment segA, segB;
            Vector2 aXy = aOffset.Xy, bXy = bOffset.Xy;
            bool ret = false;
            float retZ = 0;

            // this probably needs to get reworked.
            // not gonna bother with this shit yet. No gravity yet!!!

            // see if we collide on Z. If not we don't continue.
            /*
            float atop = aOffset.Z + a.zHeight;
            float btop = bOffset.Z + b.zHeight;
            float ab_bot_diff = aOffset.Z - bOffset.Z;
            float ab_top_diff = atop - btop;
            if (ab_bot_diff >= 0)
            {
                retZ = bOffset.Z;
            }
            else if (ab_top_diff >= 0)
            {
                retZ = btop;
            }
            else
            {
                ret = false;
            }
            */
            // done checking if we collide on z. Now check for arbitrary collisions:

            if (ret && AnySegmentsFacing(a, aXy, bXy, out segA) && AnySegmentsFacing(b, bXy, aXy, out segB))
            {
                Vector2 xyContactPoint;
                ret = LineSegment.IsIntersecting(segA, segB, out xyContactPoint, aOffset.X, aOffset.Y, bOffset.X, bOffset.Y);

                contactPoint = new Vector3(xyContactPoint.X, xyContactPoint.Y, retZ);
                return ret;
            }
            else
            {
                contactPoint = new Vector3(0,0,0);
                return ret;
            }
        }
    }
}
