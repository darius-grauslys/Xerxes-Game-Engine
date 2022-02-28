
using Xerxes.Game_Engine;
using Xerxes.Game_Engine.Physics;

namespace Xerxes.Xerxes_OpenTK.Templates
{
    public class World_2D<TEntity> :
    Game_Manager<TEntity>
    where TEntity : Entity
    {
        public World_2D()
        {
            Declare__Streams()
                .Downstream.Extending<SA__Update>()
                .Downstream.Receiving<SA__Render>(Handle_Render__World_2D);

            Declare__Streams()
                .Downstream.Extending<SA__Operate_Frame_Feature<Entity>>();

            Associate__System<Entity_Render_System, IFeature__Render_Target, SA__Operate_Frame_Feature<IFeature__Render_Target>>();

            Associate__Manager<Physics_Manager_2D, Entity>();
        }

        public void Handle_Render__World_2D(SA__Render e)
        {
            SA__Operate_Frame_Feature<IFeature__Render_Target> e_operate_frame =
                new SA__Operate_Frame_Feature<IFeature__Render_Target>(e);

            For_Each__Operate__System<IFeature__Render_Target, SA__Operate_Frame_Feature<IFeature__Render_Target>, OpenTK_Entity>(e_operate_frame);
        }

        protected override void Handle_Update__Entities__Game_Manager(SA__Update e)
        {
            SA__Operate_Frame_Feature<Entity> e_operate_frame =
                new SA__Operate_Frame_Feature<Entity>(e);

            For_Each__Operate__System<Entity, SA__Operate_Frame_Feature<Entity>, OpenTK_Entity>(e_operate_frame);
            Invoke__Descending(e);
        }

        protected override void Handle_Register__Entity__Game_Manager(SA__Register_Entity<TEntity> e)
        {
            base.Handle_Register__Entity__Game_Manager(e);
            Mediate_Register__Entity<Entity>(e.Register_Entity__Entity);
        }
    }
}
