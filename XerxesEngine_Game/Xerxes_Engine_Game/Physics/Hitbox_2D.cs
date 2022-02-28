
namespace Xerxes.Game_Engine.Physics
{
    public struct Hitbox_2D : IFeature__Hitbox_2D<Rect_AABB>
    {
        public bool Hitbox_2D__Grounded { get; set; }
        public bool Hitbox_2D__Kinematic { get; set; }
        public Rect_AABB Hitbox_2D__Top_Padding { get; set; }
        public Rect_AABB Hitbox_2D__Left_Padding { get; set; }
        public Rect_AABB Hitbox_2D__Bottom_Padding { get; set; }
        public Rect_AABB Hitbox_2D__Right_Padding { get; set; }
        public Rect_AABB Hitbox_2D__Grounded_Padding { get; set; }
        public bool Hitbox_2D__Top_Collided { get; set; }
        public bool Hitbox_2D__Right_Collided { get; set; }
        public bool Hitbox_2D__Bottom_Collided { get; set; }
        public bool Hitbox_2D__Left_Collided { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Transform__Velocity_X { get; set; }
        public float Transform__Velocity_Y { get; set; }
        public float Transform__Velocity_Z { get; set; }
        public float Transform__Acceleration_X { get; set; }
        public float Transform__Acceleration_Y { get; set; }
        public float Transform__Acceleration_Z { get; set; }
        public bool Feature__Disabled { get; set; }
        public float AABB__Ax { get; set; }
        public float AABB__Ay { get; set; }
        public float AABB__Bx { get; set; }
        public float AABB__By { get; set; }
    }
}
