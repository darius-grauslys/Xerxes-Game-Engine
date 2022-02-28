
using System.Collections.Generic;

namespace Xerxes.Game_Engine.Physics
{
    public static class PHYSICS_2D__HITBOX
    {
        public static IEnumerable<AABB> Get__Paddings<AABB>
        (IFeature__Hitbox_2D<AABB> hitbox)
        where AABB : IFeature__AABB
        {
            yield return hitbox.Hitbox_2D__Top_Padding;
            yield return hitbox.Hitbox_2D__Right_Padding;
            yield return hitbox.Hitbox_2D__Bottom_Padding;
            yield return hitbox.Hitbox_2D__Left_Padding;
        }

        public static AABB Create__Top_Padding<AABB>
        (
            float width, float height, 
            float padding, 
            float offset_x=0, float offset_y=0,
            float margin = 0.1f
        )
        where AABB : IFeature__AABB, new()
        {
            float margin_Ma = margin;
            float margin_Mb = 1 - margin;
            float margin_padding = 1 + margin;

            AABB aabb_padding = new AABB();
            aabb_padding.AABB__Ax = width * margin_Ma + offset_x;
            aabb_padding.AABB__Bx = width * margin_Mb + offset_x;
            aabb_padding.AABB__Ay = height - (padding * margin_padding) + offset_y;
            aabb_padding.AABB__By = height + offset_y;

            return aabb_padding;
        }

        public static AABB Create__Left_Padding<AABB>
        (
            float width, float height, 
            float padding, 
            float offset_x=0, float offset_y=0,
            float margin = 0.1f
        )
        where AABB : IFeature__AABB, new()
        {
            float margin_Ma = margin;
            float margin_Mb = 1 - margin;
            float margin_padding = 1 + margin;

            AABB aabb_padding = new AABB();
            aabb_padding.AABB__Ax = offset_x;
            aabb_padding.AABB__Bx = (padding * margin_padding) + offset_x;
            aabb_padding.AABB__Ay = height * margin_Ma + offset_y;
            aabb_padding.AABB__By = height * margin_Mb + offset_y;

            return aabb_padding;
        }

        public static AABB Create__Bottom_Padding<AABB>
        (
            float width, float height, 
            float padding, 
            float offset_x=0, float offset_y=0,
            float margin = 0.1f
        )
        where AABB : IFeature__AABB, new()
        {
            float margin_Ma = margin;
            float margin_Mb = 1 - margin;
            float margin_padding = 1 + margin;

            AABB aabb_padding = new AABB();
            aabb_padding.AABB__Ax = width * margin_Ma + offset_x;
            aabb_padding.AABB__Bx = width * margin_Mb + offset_x;
            aabb_padding.AABB__Ay = offset_y;
            aabb_padding.AABB__By = (padding * margin_padding) + offset_y;

            return aabb_padding;
        }

        public static AABB Create__Right_Padding<AABB>
        (
            float width, float height, 
            float padding, 
            float offset_x=0, float offset_y=0,
            float margin = 0.1f
        )
        where AABB : IFeature__AABB, new()
        {
            float margin_Ma = margin;
            float margin_Mb = 1 - margin;
            float margin_padding = 1 + margin;

            AABB aabb_padding = new AABB();
            aabb_padding.AABB__Ax = width - (padding * margin_padding) + offset_x;
            aabb_padding.AABB__Bx = width + offset_x;
            aabb_padding.AABB__Ay = height * margin_Ma + offset_y;
            aabb_padding.AABB__By = height * margin_Mb + offset_y;

            return aabb_padding;
        }

        public static Hitbox_2D Create__Standalone_Hitbox
        (
            float width,
            float height,
            float hitbox_padding = 0.1f,
            float hitbox_margin = 0.1f,
            float x=0, float y=0, float z=0,
            bool is_kinematic = false
        )
        {
            Hitbox_2D hitbox = new Hitbox_2D();

            Create__Standalone_Hitbox
            (
                ref hitbox,
                width, height,
                hitbox_padding,
                hitbox_margin,
                x, y, z,
                is_kinematic
            );

            return hitbox;
        }

        public static void Create__Standalone_Hitbox
        (
            ref Hitbox_2D hitbox,
            float width,
            float height,
            float hitbox_padding = 0.1f,
            float hitbox_margin = 0.1f,
            float x=0, float y=0, float z=0,
            bool is_kinematic = false
        )
        {
            hitbox.Hitbox_2D__Top_Padding =
                Create__Top_Padding<Rect_AABB>(hitbox.AABB__Bx, hitbox.AABB__By, hitbox_padding, margin: hitbox_margin);
            hitbox.Hitbox_2D__Right_Padding =
                Create__Right_Padding<Rect_AABB>(hitbox.AABB__Bx, hitbox.AABB__By, hitbox_padding, margin: hitbox_margin);

            hitbox.Hitbox_2D__Bottom_Padding =
                Create__Bottom_Padding<Rect_AABB>(hitbox.AABB__Bx, hitbox.AABB__By, hitbox_padding, margin: hitbox_margin);
            hitbox.Hitbox_2D__Grounded_Padding =
                Create__Bottom_Padding<Rect_AABB>(hitbox.AABB__Bx, hitbox.AABB__By, 10 * hitbox_padding, offset_y: -(hitbox_padding * 6), margin: hitbox_margin);

            hitbox.Hitbox_2D__Left_Padding =
                Create__Left_Padding<Rect_AABB>(hitbox.AABB__Bx, hitbox.AABB__By, hitbox_padding, margin: hitbox_margin);

            hitbox.Hitbox_2D__Kinematic = is_kinematic;
            hitbox.X = x;
            hitbox.Y = y;
            hitbox.Z = z;
        }

        public static void Apply__Hitbox
        (
            Entity entity,
            float hitbox_padding,
            float hitbox_margin = 0.1f
        )
        {
            entity.Hitbox_2D__Top_Padding =
                Create__Top_Padding<Rect_AABB>(entity.AABB__Bx, entity.AABB__By, hitbox_padding, margin: hitbox_margin);
            entity.Hitbox_2D__Right_Padding =
                Create__Right_Padding<Rect_AABB>(entity.AABB__Bx, entity.AABB__By, hitbox_padding, margin: hitbox_margin);

            entity.Hitbox_2D__Bottom_Padding =
                Create__Bottom_Padding<Rect_AABB>(entity.AABB__Bx, entity.AABB__By, hitbox_padding, margin: hitbox_margin);
            entity.Hitbox_2D__Grounded_Padding =
                Create__Bottom_Padding<Rect_AABB>(entity.AABB__Bx, entity.AABB__By, 10 * hitbox_padding, offset_y: -(hitbox_padding * 6), margin: hitbox_margin);

            entity.Hitbox_2D__Left_Padding =
                Create__Left_Padding<Rect_AABB>(entity.AABB__Bx, entity.AABB__By, hitbox_padding, margin: hitbox_margin);
        }

        public static bool Check_If__Either_Crossing_Edges<AABB>
        (
            IFeature__Hitbox_2D<AABB> aggressing_hitbox,
            IFeature__Hitbox_2D<AABB> resisting_hitbox,
            float offset_aggressor_x = 0,
            float offset_aggressor_y = 0
        )
        where AABB : IFeature__AABB
            =>
            Check_If__Either_Crossing_Edges
            (
                aggressing_hitbox, resisting_hitbox, 
                out _, out _, out _, out _, 
                offset_aggressor_x, offset_aggressor_y
            );

        public static bool Check_If__Either_Crossing_Edges<AABB>
        (
            IFeature__Hitbox_2D<AABB> aggressing_hitbox,
            IFeature__Hitbox_2D<AABB> resisting_hitbox,
            out bool colliding_top,
            out bool colliding_right,
            out bool colliding_bottom,
            out bool colliding_left,
            float offset_aggressor_x = 0,
            float offset_aggressor_y = 0
        )
        where AABB : IFeature__AABB
        {
            float collide_x =
                aggressing_hitbox.X
                +
                offset_aggressor_x;

            float collide_y =
                aggressing_hitbox.Y
                +
                offset_aggressor_y;

            colliding_top =
                PHYSICS_2D__AABB
                .Check_If__Either_Crossing_Edges
                (
                    aggressing_hitbox.Hitbox_2D__Top_Padding,
                    resisting_hitbox,
                    collide_x,
                    collide_y,
                    resisting_hitbox.X,
                    resisting_hitbox.Y
                );

            colliding_bottom =
                PHYSICS_2D__AABB
                .Check_If__Either_Crossing_Edges
                (
                    aggressing_hitbox.Hitbox_2D__Bottom_Padding,
                    resisting_hitbox,
                    collide_x,
                    collide_y,
                    resisting_hitbox.X,
                    resisting_hitbox.Y
                );

            colliding_right =
                PHYSICS_2D__AABB
                .Check_If__Either_Crossing_Edges
                (
                    aggressing_hitbox.Hitbox_2D__Right_Padding,
                    resisting_hitbox,
                    collide_x,
                    collide_y,
                    resisting_hitbox.X,
                    resisting_hitbox.Y
                );

            colliding_left =
                PHYSICS_2D__AABB
                .Check_If__Either_Crossing_Edges
                (
                    aggressing_hitbox.Hitbox_2D__Left_Padding,
                    resisting_hitbox,
                    collide_x,
                    collide_y,
                    resisting_hitbox.X,
                    resisting_hitbox.Y
                );

            return
                colliding_left
                ||
                colliding_right
                ||
                colliding_top
                ||
                colliding_bottom;
        }

        public static bool Check_If__Either_Overlapping<AABB>
        (
            IFeature__Hitbox_2D<AABB> aggressing_hitbox,
            IFeature__Hitbox_2D<AABB> resisting_hitbox,
            float offset_aggressor_x = 0,
            float offset_aggressor_y = 0
        )
        where AABB : IFeature__AABB
            => 
            Check_If__Either_Overlapping
            (
                aggressing_hitbox, 
                resisting_hitbox, 
                out _, out _, out _, out _, 
                offset_aggressor_x, offset_aggressor_y
            );

        public static bool Check_If__Either_Overlapping<AABB>
        (
            IFeature__Hitbox_2D<AABB> aggressing_hitbox,
            IFeature__Hitbox_2D<AABB> resisting_hitbox,
            out bool colliding_top,
            out bool colliding_right,
            out bool colliding_bottom,
            out bool colliding_left,
            float offset_aggressor_x = 0,
            float offset_aggressor_y = 0
        )
        where AABB : IFeature__AABB
        {
            float collide_x =
                aggressing_hitbox.X
                +
                offset_aggressor_x;

            float collide_y =
                aggressing_hitbox.Y
                +
                offset_aggressor_y;

            colliding_top =
                PHYSICS_2D__AABB
                .Check_If__Either_Overlapping
                (
                    aggressing_hitbox.Hitbox_2D__Top_Padding,
                    resisting_hitbox,
                    collide_x,
                    collide_y,
                    resisting_hitbox.X,
                    resisting_hitbox.Y
                );

            colliding_bottom =
                PHYSICS_2D__AABB
                .Check_If__Either_Overlapping
                (
                    aggressing_hitbox.Hitbox_2D__Bottom_Padding,
                    resisting_hitbox,
                    collide_x,
                    collide_y,
                    resisting_hitbox.X,
                    resisting_hitbox.Y
                );

            colliding_right =
                PHYSICS_2D__AABB
                .Check_If__Either_Overlapping
                (
                    aggressing_hitbox.Hitbox_2D__Right_Padding,
                    resisting_hitbox,
                    collide_x,
                    collide_y,
                    resisting_hitbox.X,
                    resisting_hitbox.Y
                );

            colliding_left =
                PHYSICS_2D__AABB
                .Check_If__Either_Overlapping
                (
                    aggressing_hitbox.Hitbox_2D__Left_Padding,
                    resisting_hitbox,
                    collide_x,
                    collide_y,
                    resisting_hitbox.X,
                    resisting_hitbox.Y
                );

            return
                colliding_left
                ||
                colliding_right
                ||
                colliding_top
                ||
                colliding_bottom;
        }
    }
}
