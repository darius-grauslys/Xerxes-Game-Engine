
namespace Xerxes.Game_Engine
{
    public interface IFeature_Physics : 
        IFeature_Transform
    {
        float Physics__Velocity_X { get; set; }
        float Physics__Velocity_Y { get; set; }
        float Physics__Velocity_Z { get; set; }

        float Physics__Acceleration_X { get; set; }
        float Physics__Acceleration_Y { get; set; }
        float Physics__Acceleration_Z { get; set; }

        float Physics__Angular_Velocity_Pitch { get; set; }
        float Physics__Angular_Velocity_Yaw { get; set; }
        float Physics__Angular_Velocity_Roll { get; set; }
    }
}
