
namespace Xerxes.Xerxes_OpenTK.Exports.Graphics
{
    public struct Render_Target : IFeature__Render_Target
    {
        public Vertex_Object Vertex_Object { get; set; }
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

        public Render_Target
        (
            float x = 0, float y = 0, float z = 0,
            Vertex_Object? nullable_vo = null
        )
        {
            Vertex_Object = nullable_vo ?? new Vertex_Object();

            X = x;
            Y = y;
            Z = z;

            Transform__Velocity_X = 0;
            Transform__Velocity_Y = 0;
            Transform__Velocity_Z = 0;

            Transform__Acceleration_X = 0;
            Transform__Acceleration_Y = 0;
            Transform__Acceleration_Z = 0;

            Feature__Disabled = false;
        }
    }
}
