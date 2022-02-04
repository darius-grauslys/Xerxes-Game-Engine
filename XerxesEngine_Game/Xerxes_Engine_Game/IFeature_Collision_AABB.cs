
namespace Xerxes.Game_Engine
{
    public interface IFeature_Collision_AABB : 
        IFeature
    {
        float Collision__AX { get; set; }
        float Collision__AY { get; set; }

        float Collision__BX { get; set; }
        float Collision__BY { get; set; }
    }
}
