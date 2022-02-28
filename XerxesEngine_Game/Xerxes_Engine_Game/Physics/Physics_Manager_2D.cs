
namespace Xerxes.Game_Engine.Physics
{
    /// <summary>
    /// This Xerxes Object mediates Update to Managers 
    /// and also operates systems on Entity. It does so in this order.
    ///
    /// Set accelerations to 0.
    /// Invoke Systems to determine acting forces -> set acceleration
    /// (Extends SA__Operate_Acceleration)
    /// Mediate to Managers to control velocity
    /// (Extends SA__Operate_Velocity)
    /// Apply controlled velocity to transform position.
    /// </summary>
    public class Physics_Manager_2D :
    Game_Manager<Entity>
    {
        private SA__Operate_Acceleration _Physics_Manager__ACCELERATION_OPERATION { get; }
        private SA__Operate_Velocity     _Physics_Manager__VELOCITY_OPERATION { get; }

        public Physics_Manager_2D()
        {
            _Physics_Manager__ACCELERATION_OPERATION = new SA__Operate_Acceleration(new SA__Update());
            _Physics_Manager__VELOCITY_OPERATION     = new SA__Operate_Velocity(new SA__Update());

            Declare__Streams()
                .Downstream.Extending<SA__Update>()
                .Downstream.Extending<SA__Operate_Acceleration>()
                .Downstream.Extending<SA__Operate_Velocity>();

            Associate__System<Gravity_System, Entity, SA__Operate_Acceleration>();
            Associate__System<Air_Resistance_System, Entity, SA__Operate_Velocity>();
            Associate__Manager<Collision_Manager_2D, Entity>();
        }

        protected override void Handle_Register__Entity__Game_Manager(SA__Register_Entity<Entity> e)
        {
            base.Handle_Register__Entity__Game_Manager(e);
            Mediate_Register__Entity<Entity>(e.Register_Entity__Entity);
        }

        protected override void Handle_Update__Entities__Game_Manager
        (SA__Update e)
        {
            _Physics_Manager__ACCELERATION_OPERATION
                .Operate_Frame_Feature__Delta_Time = e.Frame__Delta_Time;
            _Physics_Manager__ACCELERATION_OPERATION
                .Operate_Frame_Feature__Elapsed_Time = e.Frame__Elapsed_Time;

            _Physics_Manager__VELOCITY_OPERATION
                .Operate_Frame_Feature__Delta_Time = e.Frame__Delta_Time;
            _Physics_Manager__VELOCITY_OPERATION
                .Operate_Frame_Feature__Elapsed_Time = e.Frame__Elapsed_Time;

            float delta_time = (float)e.Frame__Delta_Time;
            float delta_time_2 =  delta_time;

            For_Each__Entity__Game_Manager
            (
                (entity) =>
                {
                    //reset acceleration to reapply forces via systems
                    entity.Transform__Acceleration_X = 0;
                    entity.Transform__Acceleration_Y = 0;
                    entity.Transform__Acceleration_Z = 0;

                    _Physics_Manager__ACCELERATION_OPERATION
                        .Operate_Feature__Feature = entity;

                    //apply forces
                    Invoke__Descending(_Physics_Manager__ACCELERATION_OPERATION);

                    //determine velocity.
                    entity.Transform__Velocity_X +=
                        entity.Transform__Acceleration_X
                        *
                        delta_time_2;
                    entity.Transform__Velocity_Y +=
                        entity.Transform__Acceleration_Y
                        *
                        delta_time_2;
                    entity.Transform__Velocity_Z +=
                        entity.Transform__Acceleration_Z
                        *
                        delta_time_2;

                    _Physics_Manager__VELOCITY_OPERATION
                        .Operate_Feature__Feature = entity;

                    Invoke__Descending(_Physics_Manager__VELOCITY_OPERATION);
                }
            );

            //control velocity
            Invoke__Descending(e);

            For_Each__Entity__Game_Manager
            (
                (entity) =>
                {
                    //determine offset
                    entity.X +=
                        entity.Transform__Velocity_X;
                    entity.Y +=
                        entity.Transform__Velocity_Y;
                    entity.Z +=
                        entity.Transform__Velocity_Z;
                }
            );
        }
    }
}
