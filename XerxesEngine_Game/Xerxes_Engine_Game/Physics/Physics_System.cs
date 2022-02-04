
namespace Xerxes.Game_Engine.Physics
{
    public class Physics_System :
        System
        <
            SA__Operate_Feature_Chronical<IFeature_Physics>, 
            IFeature_Physics
        >
    {
        protected override void Handle_Operate__Feature__System
        (
            SA__Operate_Feature_Chronical<IFeature_Physics> e
        )
        {
            IFeature_Physics feature = e.Operate_Feature__Feature;
            float delta_time = e.Operate_Feature_Chronical__DELTA_TIME;

            feature
                .Physics__Velocity_X =
                Handle_Determine__Velocity__Physics_System
                (
                    feature.Physics__Velocity_X,
                    feature.Physics__Acceleration_X,
                    delta_time
                );
            feature
                .Physics__Velocity_Y =
                Handle_Determine__Velocity__Physics_System
                (
                    feature.Physics__Velocity_Y,
                    feature.Physics__Acceleration_Y,
                    delta_time
                );
            feature
                .Physics__Velocity_Z =
                Handle_Determine__Velocity__Physics_System
                (
                    feature.Physics__Velocity_Z,
                    feature.Physics__Acceleration_Z,
                    delta_time
                );

            feature
                .Transform__Post_X =
                Handle_Determine__Position__Physics_System
                (
                    feature.Transform__X,
                    feature.Physics__Velocity_X,
                    delta_time
                );
            feature
                .Transform__Post_Y =
                Handle_Determine__Position__Physics_System
                (
                    feature.Transform__Y,
                    feature.Physics__Velocity_Y,
                    delta_time
                );
            feature
                .Transform__Post_Z =
                Handle_Determine__Position__Physics_System
                (
                    feature.Transform__Z,
                    feature.Physics__Velocity_Z,
                    delta_time
                );
        }

        protected virtual float Handle_Determine__Position__Physics_System
        (
            float transform_value,
            float velocity,
            float delta_time
        )
        {
            return transform_value + (velocity * delta_time);
        }

        protected virtual float Handle_Determine__Velocity__Physics_System
        (
            float velocity,
            float acceleration,
            float delta_time
        )
        {
            return velocity + (acceleration * delta_time);
        }
    }
}
