
namespace Xerxes.Game_Engine.Physics
{
    public struct Rect_AABB : IFeature__AABB
    {
        public float AABB__Ax { get; set; }
        public float AABB__Ay { get; set; }
        public float AABB__Bx { get; set; }
        public float AABB__By { get; set; }

        public Rect_AABB
        (
            float ax = 0, 
            float ay = 0, 
            float bx = 1, 
            float by = 1
        )
        {
            AABB__Ax = ax;
            AABB__Ay = ay;
            AABB__Bx = bx;
            AABB__By = by;
        }

        public override string ToString()
        {
            return $"[Rect: (Ax: {AABB__Ax}, Ay: {AABB__Ay}), (Bx: {AABB__Bx}, By: {AABB__By}) ]";
        }
    }
}
