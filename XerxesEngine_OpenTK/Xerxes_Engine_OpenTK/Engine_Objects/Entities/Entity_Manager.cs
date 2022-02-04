using System.Collections.Generic;

namespace Xerxes.Xerxes_OpenTK.Engine_Objects.Entities
{
    public class Entity_Manager<EntityType> :
        Xerxes_Object<Entity_Manager<EntityType>>
        where EntityType : Entity
    {
        private List<EntityType> _Entity_Manager__ENTITIES { get; }
        protected IEnumerable<EntityType> Entity_Manager__ENTITIES
            => _Entity_Manager__ENTITIES;

        public Entity_Manager()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Update>
                (Private_Update__Entities__Entity_Manager)
                .Downstream.Extending<SA__Control_Entity<EntityType>>();
        }

        private void Private_Update__Entities__Entity_Manager
        (SA__Update e)
        {
            foreach(EntityType entity in _Entity_Manager__ENTITIES)
                Invoke__Descending(new SA__Control_Entity<EntityType>(e, entity));
        }
    }
}
