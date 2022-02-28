
namespace Xerxes.Game_Engine.Physics
{
    public class Gravity_System :
    Xerxes_System<Entity, SA__Operate_Acceleration>
    {
        protected float Gravity_System__Gravity_Acceleration { get; set; }

        public Gravity_System()
        {
            Gravity_System__Gravity_Acceleration = -9.8f;
        }

        protected override void Handle_Operate__Feature__System
        (SA__Operate_Acceleration e)
        {
            if (e.Operate_Feature__Feature.Hitbox_2D__Kinematic)
                return;

            e.Operate_Feature__Feature
                .Transform__Acceleration_Y +=
                Gravity_System__Gravity_Acceleration;
        }
    }
}
