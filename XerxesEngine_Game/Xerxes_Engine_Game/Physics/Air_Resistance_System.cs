
using Xerxes.Tools;

namespace Xerxes.Game_Engine.Physics
{
    public class Air_Resistance_System :
    Xerxes_System<Entity, SA__Operate_Velocity>
    {
        protected float Air_Resistance_System__Drag_Coefficent { get; set; } 
        protected float Air_Resistance_System__Terminal_Velocity { get; set; }

        public Air_Resistance_System()
        {
            Air_Resistance_System__Drag_Coefficent = 0.8f;
            Air_Resistance_System__Terminal_Velocity = 4000;
        }

        protected override void Handle_Operate__Feature__System
        (SA__Operate_Velocity e)
        {
            Entity feature = e.Operate_Feature__Feature;

            float delta_resist =
                (
                    1
                    -
                    (Air_Resistance_System__Drag_Coefficent * (float)e.Operate_Frame_Feature__Delta_Time)
                );

            feature.Transform__Velocity_X *= delta_resist;
            feature.Transform__Velocity_Y *= delta_resist;

            //clamp against terminal velocity
            e.Operate_Feature__Feature
                .Transform__Velocity_Y =
                    Math_Helper
                        .Clamp__Float
                        (
                            e.Operate_Feature__Feature.Transform__Velocity_Y, 
                            -Air_Resistance_System__Terminal_Velocity, 
                            Air_Resistance_System__Terminal_Velocity
                        );

            feature.Transform__Velocity_Z *= delta_resist;
        }
    }
}
