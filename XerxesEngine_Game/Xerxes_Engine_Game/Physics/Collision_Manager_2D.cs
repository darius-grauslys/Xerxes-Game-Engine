
namespace Xerxes.Game_Engine.Physics
{
    public class Collision_Manager_2D :
    Game_Manager<Entity>
    {
        protected override void Handle_Update__Entities__Game_Manager(SA__Update e)
        {
            For_Each__Entity__Game_Manager
            (
                (entity1) => 
                    For_Each__Entity__Game_Manager((entity2) =>
                    {
                        if(entity1 == entity2)
                            return;
                        Assert__Collision__Collision_Manager_2D(entity1, entity2, (float)e.Frame__Delta_Time);
                    }
                    )
            );
        }
    
        /// <summary>
        /// Checks for collision, then will resolve the collision if colliding.
        /// </summary>
        protected void Assert__Collision__Collision_Manager_2D
        (
            IFeature__Hitbox_2D<Rect_AABB> aggressing_hitbox, 
            IFeature__Hitbox_2D<Rect_AABB> resisting_hitbox,
            float delta_time
        )
        {
            if (aggressing_hitbox.Hitbox_2D__Kinematic)
                return;

            float delta_x =
                aggressing_hitbox.Transform__Velocity_X
                *
                delta_time;

            float delta_y =
                aggressing_hitbox.Transform__Velocity_Y
                *
                delta_time;

            bool colliding_top, colliding_right, colliding_bottom, colliding_left;

            PHYSICS_2D__HITBOX
                .Check_If__Either_Crossing_Edges
                (
                    aggressing_hitbox,
                    resisting_hitbox,
                    out colliding_top,
                    out colliding_right,
                    out colliding_bottom,
                    out colliding_left,
                    delta_x, delta_y
                );


            float collide_x =
                aggressing_hitbox.X
                +
                delta_x;

            float collide_y =
                aggressing_hitbox.Y
                +
                delta_y;

            float diff_x =
                aggressing_hitbox.X - resisting_hitbox.X;

            float diff_y =
                aggressing_hitbox.Y - resisting_hitbox.Y;




            bool colliding_grounded_sensor =
                PHYSICS_2D__AABB
                .Check_If__Either_Overlapping
                (
                    aggressing_hitbox.Hitbox_2D__Grounded_Padding,
                    resisting_hitbox,
                    collide_x,
                    collide_y,
                    resisting_hitbox.X,
                    resisting_hitbox.Y
                );





            if (colliding_top)
            {
                aggressing_hitbox
                    .Hitbox_2D__Top_Collided = true;

                aggressing_hitbox.Transform__Velocity_Y +=
                    -(aggressing_hitbox.AABB__By + diff_y) * 0.2f;
            }

            if (colliding_bottom)
            {
                aggressing_hitbox
                    .Hitbox_2D__Bottom_Collided = true;
                aggressing_hitbox.Transform__Velocity_Y +=
                    (resisting_hitbox.AABB__By - diff_y) * 0.2f;
            }
            
            if (colliding_right)
            {
                aggressing_hitbox
                    .Hitbox_2D__Right_Collided = true;
                aggressing_hitbox.Transform__Velocity_X +=
                    -(aggressing_hitbox.AABB__Bx + diff_x) * 0.2f;
            }
            
            if (colliding_left)
            {
                aggressing_hitbox
                    .Hitbox_2D__Left_Collided = true;
                aggressing_hitbox.Transform__Velocity_X +=
                    (resisting_hitbox.AABB__Bx - diff_x) * 0.2f;
            }

            if (colliding_grounded_sensor)
                aggressing_hitbox.Hitbox_2D__Grounded = colliding_grounded_sensor;
        }
    }
}
