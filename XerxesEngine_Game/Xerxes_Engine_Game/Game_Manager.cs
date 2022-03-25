
using System;
using System.Collections.Generic;

namespace Xerxes.Game_Engine
{
    public abstract class Game_Manager<TEntity> :
        Game_Manager_Base
        where TEntity : Entity
    {
        private List<TEntity> _Game_Manager__ENTITIES { get; }

        public Game_Manager()
        {
            _Game_Manager__ENTITIES = new List<TEntity>();

            Genealogy
                .With__Streamlines
                    .With__Ancestors
                        .Recieving<SA__Update>
                            (Handle_Update__Entities__Game_Manager)
                        .Recieving<SA__Register_Entity<TEntity>>
                            (Handle_Register__Entity__Game_Manager);
        }

        protected void Associate__Manager<TManager, UEntity>()
        where TManager : Game_Manager<UEntity>, new()
        where UEntity : Entity
        {
            if (!typeof(UEntity).IsAssignableFrom(typeof(TEntity)))
            {
                //TODO: const msg.
                Log.Write__Error__Log
                (
                    $"Tried to associate manager {typeof(TManager)} but managed entity {typeof(TEntity)} is not of type {typeof(UEntity)}!", this
                );
                return;
            }

            Declare__Streams()
                .Downstream.Extending<SA__Register_Entity<UEntity>>(false)
                .Downstream.Receiving<SA__Register_Entity<UEntity>>(Invoke__Void__Descending, false);

            Declare__Hierarchy()
                .Associate<TManager>();
        }

        protected void Associate__System<TSystem, TFeature, TOperation>()
        where TSystem : Xerxes_System<TFeature, TOperation>, new()
        where TFeature : IFeature
        where TOperation : SA__Operate_Feature<TFeature>
        {
            if (typeof(TEntity).GetInterface(typeof(IFeature).FullName) == null)
            {
                //TODO: const msg.
                Log.Write__Error__Log($"Tried to associate system {typeof(TSystem)} but managed entity {typeof(TEntity)} is not of type {typeof(TFeature)}!", this);
                return;
            }

            //TODO: I have to do a work around.
            // for some reason this causes the association stack to be empty... big yikes
            Declare__Streams()
                .Downstream.Extending<TOperation>(false)
                ;//.Downstream.Receiving<TOperation>(Invoke__Void__Descending, false);

            Declare__Hierarchy()
                .Associate<TSystem>();
        }

        protected virtual void Handle_Register__Entity__Game_Manager
        (SA__Register_Entity<TEntity> e)
        {
            if (e.Register_Entity__Entity == null)
            {
                Log.Write__Warning__Log("Entity registered is null.", this);
                return;
            }

            _Game_Manager__ENTITIES
                .Add(e.Register_Entity__Entity);

            e.Register_Entity__Entity
                .Entity__Disposed +=
                Handle_Dispose__Entity__Game_Manager;
        }

        protected virtual void Handle_Dispose__Entity__Game_Manager
        (Entity entity)
        {
            if (entity is TEntity)
                _Game_Manager__ENTITIES
                    .Remove(entity as TEntity);
        }

        protected abstract void Handle_Update__Entities__Game_Manager
        (SA__Update e);

        protected void For_Each__Entity__Game_Manager(Action<TEntity> iterator)
            => _Game_Manager__ENTITIES.ForEach(iterator);

        protected void For_Each__Operate__System<TFeature, TOperation, UEntity>(TOperation e_operaiton)
        where TFeature : IFeature
        where TOperation : SA__Operate_Feature<TFeature>
        where UEntity : Entity, TFeature
        {
            foreach(TEntity entity in _Game_Manager__ENTITIES)
            {
                e_operaiton.Operate_Feature__Feature = entity as UEntity;
                Invoke__Descending(e_operaiton);
            }
        }

        protected void Mediate_Register__Entity<UEntity>
        (TEntity entity)
        where UEntity : Entity
        {
            //TODO: make SA member;
            Invoke__Descending(new SA__Register_Entity<UEntity>() { Register_Entity__Entity = entity as UEntity });
        }
    }
}
