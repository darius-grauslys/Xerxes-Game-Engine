namespace Xerxes_Engine.Export_OpenTK.Engine_Objects.Entities
{
    public sealed class SA__Control_Entity<EntityType> :
    SA__Chronical
    where EntityType : Entity
    {
        public EntityType SA__Control_Entity__ENTITY { get; }

        public SA__Control_Entity(SA__Chronical e, EntityType entity)
        : base(e)
        {
            SA__Control_Entity__ENTITY = entity;
        }
    }
}
