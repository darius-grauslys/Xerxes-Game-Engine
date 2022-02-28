
using Xerxes.Tools;

namespace Xerxes.Game_Engine.Physics
{
    public static class PHYSICS_2D__AABB
    {
        public static float Width(IFeature__AABB aabb)
            => aabb.AABB__Bx - aabb.AABB__Ax;

        public static float Height(IFeature__AABB aabb)
            => aabb.AABB__By - aabb.AABB__Ay;

        public static bool Check_If__Point_Clamped_X
        (
            float x,
            IFeature__AABB aabb,
            float offset_x = 0,
            float offset_aabb_x = 0
        )
        {
            bool clamped_x = 
                Math_Helper.Check_If__Obeys_Inclusive_Clamp
                (
                    x + offset_x, 
                    aabb.AABB__Ax + offset_aabb_x, 
                    aabb.AABB__Bx + offset_aabb_x
                );

            return clamped_x;
        }

        public static bool Check_If__Point_Clamped_Y
        (
            float y,
            IFeature__AABB aabb,
            float offset_y = 0,
            float offset_aabb_y = 0
        )
        {
            bool clamped_y =
                Math_Helper.Check_If__Obeys_Inclusive_Clamp
                (
                    y + offset_y, 
                    aabb.AABB__Ay + offset_aabb_y, 
                    aabb.AABB__By + offset_aabb_y
                );

            return clamped_y;
        }

        public static bool Check_If__Point_Contained
        (
            float x,
            float y,
            IFeature__AABB aabb,
            float offset_x = 0,
            float offset_y = 0,
            float offset_aabb_x = 0,
            float offset_aabb_y = 0
        )
        {
            bool bounded_x =
                Check_If__Point_Clamped_X(x, aabb, offset_x, offset_aabb_x);

            bool bounded_y =
                Check_If__Point_Clamped_Y(y, aabb, offset_y, offset_aabb_y);

            return bounded_x && bounded_y;
        }

        public static bool Check_If__Two_Is_Overlapping_One
        (
            IFeature__AABB aabb_one,
            IFeature__AABB aabb_two,

            out bool bounds_anyCorner,

            out bool bounds_xOne,
            out bool bounds_yOne,

            float offset_xOne = 0,
            float offset_yOne = 0,

            float offset_xTwo = 0,
            float offset_yTwo = 0,

            bool check_corners = true
        )
        {
            bool x_bounded__Ax_Two =
                Check_If__Point_Clamped_X(aabb_two.AABB__Ax, aabb_one, offset_xTwo, offset_xOne);
            bool y_bounded__Ay_Two =
                Check_If__Point_Clamped_Y(aabb_two.AABB__Ay, aabb_one, offset_yTwo, offset_yOne);

            bool x_bounded__Bx_Two =
                Check_If__Point_Clamped_X(aabb_two.AABB__Bx, aabb_one, offset_xTwo, offset_xOne);
            bool y_bounded__By_Two = 
                Check_If__Point_Clamped_Y(aabb_two.AABB__By, aabb_one, offset_yTwo, offset_yOne);

            bounds_xOne =
                x_bounded__Ax_Two
                &&
                x_bounded__Bx_Two;

            bounds_yOne =
                y_bounded__Ay_Two
                &&
                y_bounded__By_Two;

            bool bounds_xyA =
                bounds_xOne
                &&
                bounds_yOne;

            if (bounds_xyA)
                return bounds_anyCorner = bounds_xyA;

            bounds_anyCorner = !check_corners;
            if (check_corners)
            {
                //xyA
                if (Private_Check__Corner(x_bounded__Ax_Two, y_bounded__Ay_Two, out bounds_anyCorner))
                    return true;
                //xyB
                if (Private_Check__Corner(x_bounded__Bx_Two, y_bounded__By_Two, out bounds_anyCorner))
                    return true;
                //xAyB
                if (Private_Check__Corner(x_bounded__Ax_Two, y_bounded__By_Two, out bounds_anyCorner))
                    return true;
                //xByA
                if (Private_Check__Corner(x_bounded__Bx_Two, y_bounded__Ay_Two, out bounds_anyCorner))
                    return true;
            }

            return false;
        }

        private static bool Private_Check__Corner(bool x, bool y, out bool result)
            => result = x && y;

        public static bool Check_If__Either_Crossing_Edges
        (
            IFeature__AABB aabb1,
            IFeature__AABB aabb2,

            float offset_x1 = 0,
            float offset_y1 = 0,

            float offset_x2 = 0,
            float offset_y2 = 0
        )
        {
            bool bounds_anyCorner;
            bool bounds_x1, bounds_y1;
            bool bounds_xy1 =
                Check_If__Two_Is_Overlapping_One
                (
                    aabb1, aabb2, 
                    out bounds_anyCorner,
                    out bounds_x1, out bounds_y1,
                    offset_x1, offset_y1,
                    offset_x2, offset_y2,
                    false
                );

            if (bounds_xy1)
                return true;

            bool bounds_x2, bounds_y2;

            bool bounds_xy2 =
                Check_If__Two_Is_Overlapping_One
                (
                    aabb2, aabb1,
                    out bounds_anyCorner,
                    out bounds_x2, out bounds_y2,
                    offset_x2, offset_y2,
                    offset_x1, offset_y1,
                    false
                );

            if (bounds_xy2)
                return true;

            if (bounds_x1 && bounds_y2)
                return true;

            if (bounds_x2 && bounds_y1)
                return true;

            return false;
        }

        public static bool Check_If__Either_Overlapping
        (
            IFeature__AABB aabb1,
            IFeature__AABB aabb2,

            float offset_x1 = 0,
            float offset_y1 = 0,

            float offset_x2 = 0,
            float offset_y2 = 0
        )
        {
            bool bounds_anyCorner;
            bool bounds_x1, bounds_y1;
            bool bounds_xy1 =
                Check_If__Two_Is_Overlapping_One
                (
                    aabb1, aabb2, 
                    out bounds_anyCorner,
                    out bounds_x1, out bounds_y1,
                    offset_x1, offset_y1,
                    offset_x2, offset_y2
                );

            if (bounds_xy1 || bounds_anyCorner)
                return true;

            bool bounds_x2, bounds_y2;

            bool bounds_xy2 =
                Check_If__Two_Is_Overlapping_One
                (
                    aabb2, aabb1,
                    out bounds_anyCorner,
                    out bounds_x2, out bounds_y2,
                    offset_x2, offset_y2,
                    offset_x1, offset_y1
                );

            if (bounds_xy2 || bounds_anyCorner)
                return true;

            if (bounds_x1 && bounds_y2)
                return true;

            if (bounds_x2 && bounds_y1)
                return true;

            return false;
        }
    }
}
