
using Xerxes.Game_Engine.Input;

namespace Xerxes.Game_Engine 
{
    public class Scene :
    Xerxes_Object<Scene>
    {
        public Scene()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Sealed_Under_Game>
                (Handle__Seal_Under_Game__Scene)
                .Downstream.Receiving<SA__Update>
                (Handle__Update__Scene)
                .Downstream.Receiving<SA__Render_Begin>
                (Handle__Render_Begin__Scene)
                .Downstream.Receiving<SA__Render>
                (Handle__Render__Scene)
                .Downstream.Receiving<SA__Input_Key_Down>
                (Handle__Key_Down__Scene)
                .Downstream.Receiving<SA__Input_Key_Up>
                (Handle__Key_Up__Scene)
                .Downstream.Receiving<SA__Input_Mouse_Move>
                (Handle__Mouse_Move__Scene)
                .Downstream.Receiving<SA__Input_Mouse_Button>
                (Handle__Mouse_Button__Scene);
        }

        protected void Establish__Entity_Type<TEntity>()
        where TEntity : Entity
            => Declare__Streams().Downstream.Extending<SA__Register_Entity<TEntity>>();

        protected void Register__Entity<TEntity>(TEntity entity)
        where TEntity : Entity
        {
            SA__Register_Entity<TEntity> e_register =
                new SA__Register_Entity<TEntity>();

            e_register.Register_Entity__Entity = entity;

            Invoke__Descending(e_register);
        }

        protected virtual void Handle__Seal_Under_Game__Scene(SA__Sealed_Under_Game e)
        {
        }

        protected virtual void Handle__Update__Scene(SA__Update e)
        {
        }

        protected virtual void Handle__Render_Begin__Scene(SA__Render_Begin e)
        {
        }

        protected virtual void Handle__Render__Scene(SA__Render e)
        {
        }

        protected virtual void Handle__Key_Down__Scene(SA__Input_Key_Down e)
        {
        }

        protected virtual void Handle__Key_Up__Scene(SA__Input_Key_Up e)
        {
        }

        protected virtual void Handle__Mouse_Move__Scene(SA__Input_Mouse_Move e)
        {
        }

        protected virtual void Handle__Mouse_Button__Scene(SA__Input_Mouse_Button e)
        {
        }
    }
}
