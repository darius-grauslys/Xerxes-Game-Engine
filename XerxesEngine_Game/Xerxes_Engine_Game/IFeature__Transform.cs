
namespace Xerxes.Game_Engine
{
    public interface IFeature__Transform : IFeature
    {
        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }

        float Transform__Velocity_X { get; set; }
        float Transform__Velocity_Y { get; set; }
        float Transform__Velocity_Z { get; set; }

        float Transform__Acceleration_X { get; set; }
        float Transform__Acceleration_Y { get; set; }
        float Transform__Acceleration_Z { get; set; }
    }
}
