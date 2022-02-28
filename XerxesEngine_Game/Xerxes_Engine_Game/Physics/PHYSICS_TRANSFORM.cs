
using Xerxes.Tools;

namespace Xerxes.Game_Engine.Physics
{
    public static class PHYSICS_TRANSFORM 
    {
        public static bool Check_If__In_Motion
        (IFeature__Transform transform)
            =>
            Math_Helper.Tolerable__Is_Zero__Float(transform.Transform__Velocity_X)
            ||
            Math_Helper.Tolerable__Is_Zero__Float(transform.Transform__Velocity_Y)
            ||
            Math_Helper.Tolerable__Is_Zero__Float(transform.Transform__Velocity_Z);

        public static bool Check_If__Accelerating
        (IFeature__Transform transform)
            =>
            Math_Helper.Tolerable__Is_Zero__Float(transform.Transform__Acceleration_X)
            ||
            Math_Helper.Tolerable__Is_Zero__Float(transform.Transform__Acceleration_Y)
            ||
            Math_Helper.Tolerable__Is_Zero__Float(transform.Transform__Acceleration_Z);

        /// <summary>
        /// Returns false if Check_If__In_Motion or Check_If__Accelerating
        /// </summary>
        public static bool Check_If__Strictly_Stationary
        (IFeature__Transform transform)
            =>
            !(
                Check_If__In_Motion(transform)
                ||
                Check_If__Accelerating(transform)
            );
    }
}
