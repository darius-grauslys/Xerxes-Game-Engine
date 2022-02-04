using OpenTK;

namespace Xerxes.Xerxes_OpenTK.Engine_Objects.Entities
{
    public sealed class SA__Move_Entity<EntityType> :
    SA__Chronical
    where EntityType : Entity
    {
        public EntityType SA__Move_Entity__ENTITY { get; }
        public Vector3 SA__Move_Entity__POSITION { get; }

        public SA__Move_Entity(SA__Chronical e, EntityType entity, Vector3 position)
        : base(e)
        {
            SA__Move_Entity__ENTITY = entity;
            SA__Move_Entity__POSITION = position;
        }
    }  
}
