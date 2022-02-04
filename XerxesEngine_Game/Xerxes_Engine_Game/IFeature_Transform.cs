
namespace Xerxes.Game_Engine
{
    public interface IFeature_Transform : 
        IFeature
    {
        //positional
        float Transform__X { get; set; }
        float Transform__Y { get; set; }
        float Transform__Z { get; set; }
                                  
        float Transform__Post_X { get; set; }
        float Transform__Post_Y { get; set; }
        float Transform__Post_Z { get; set; }

        //quaternion
        float Transform__Rotation_X { get; set; }
        float Transform__Rotation_Y { get; set; }
        float Transform__Rotation_Z { get; set; }
        float Transform__Rotation_W { get; set; }
    }
}
