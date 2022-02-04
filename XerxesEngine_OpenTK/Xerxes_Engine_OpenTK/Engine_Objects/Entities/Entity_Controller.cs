

namespace Xerxes.Xerxes_OpenTK.Engine_Objects.Entities
{
    public abstract class Entity_Controller<EntityType> :
    Xerxes_Object<Entity_Controller<EntityType>>
    where EntityType : Entity
    {
        public Entity_Controller()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Control_Entity<EntityType>>
                (Handle_Control__Entity__Entity_Controller)
                .Downstream.Extending<SA__Move_Entity<EntityType>>();
        }

        protected abstract void Handle_Control__Entity__Entity_Controller
        (SA__Control_Entity<EntityType> e);
    }
}
