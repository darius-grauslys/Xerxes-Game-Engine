
namespace Xerxes.Game_Engine.Physics
{
    public interface IFeature__Hitbox_2D<AABB> :
    IFeature__Transform, IFeature__AABB
    where AABB : IFeature__AABB
    {
        bool Hitbox_2D__Grounded { get; set; }

        bool Hitbox_2D__Kinematic { get; set; }

        AABB Hitbox_2D__Top_Padding { get; set; }
        AABB Hitbox_2D__Left_Padding { get; set; }
        AABB Hitbox_2D__Bottom_Padding { get; set; }
        AABB Hitbox_2D__Right_Padding { get; set; }

        AABB Hitbox_2D__Grounded_Padding { get; set; }

        bool Hitbox_2D__Top_Collided { get; set; }
        bool Hitbox_2D__Right_Collided { get; set; }
        bool Hitbox_2D__Bottom_Collided { get; set; }
        bool Hitbox_2D__Left_Collided { get; set; }
    }
}
