
using System;
using Xerxes.Game_Engine.Physics;

namespace Xerxes.Game_Engine
{
    public class Entity :
        IFeature__Hitbox_2D<Rect_AABB>,
        IDisposable
    {
        public event Action<Entity> Entity__Disposed;

        public bool Feature__Disabled { get; set; }



        public bool Hitbox_2D__Grounded { get; set; }
        public bool Hitbox_2D__Kinematic { get; set; }

        public Rect_AABB Hitbox_2D__Top_Padding { get; set; }
        public Rect_AABB Hitbox_2D__Bottom_Padding { get; set; }
        public Rect_AABB Hitbox_2D__Left_Padding { get; set; }
        public Rect_AABB Hitbox_2D__Right_Padding { get; set; }
        
        public float AABB__Ax { get; set; }
        public float AABB__Ay { get; set; }
        public float AABB__Bx { get; set; }
        public float AABB__By { get; set; }



        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Transform__Velocity_X { get; set; }
        public float Transform__Velocity_Y { get; set; }
        public float Transform__Velocity_Z { get; set; }
        public float Transform__Acceleration_X { get; set; }
        public float Transform__Acceleration_Y { get; set; }
        public float Transform__Acceleration_Z { get; set; }
        public bool Hitbox_2D__Top_Collided { get; set; }
        public bool Hitbox_2D__Right_Collided { get; set; }
        public bool Hitbox_2D__Bottom_Collided { get; set; }
        public bool Hitbox_2D__Left_Collided { get; set; }
        public Rect_AABB Hitbox_2D__Grounded_Padding { get; set; }

        public Entity
        (
            float x = 0,
            float y = 0,
            float z = 0,
            float aabb_width = 1,
            float aabb_height = 1,
            float hitbox_padding = 0.1f,
            float hitbox_margin = 0.1f,

            bool is_kinematic = false,
            bool is_enabled = true
        )
        {
            Feature__Disabled = true;

            X = x;
            Y = y;
            Z = z;



            AABB__Ax = 0;
            AABB__Ay = 0;
            AABB__Bx = aabb_width;
            AABB__By = aabb_height;



            Hitbox_2D__Kinematic = is_kinematic;

            PHYSICS_2D__HITBOX
                .Apply__Hitbox(this, hitbox_padding, hitbox_margin);
        }

        public virtual void Dispose()
        {
            Entity__Disposed?.Invoke(this);
        }
    }
}
