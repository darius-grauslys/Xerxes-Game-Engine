
namespace Xerxes.Game_Engine
{
    public abstract class Game :
    Root<Root_Association_Event, Root_Dissassociation_Event>
    {
        private SA__Update _Game__UPDATE { get; }
        private SA__Render _Game__RENDER { get; }
        private SA__Render_End _Game__RENDER_END { get; }

        public Game()
        {
            Declare__Streams()
                .Downstream.Extending<SA__Sealed_Under_Game>()
                .Upstream.Extending<SA__Sealed_Under_Game>()
                .Downstream.Extending<SA__Update>()
                .Upstream.Extending<SA__Update>()
                .Downstream.Extending<SA__Render>()
                .Upstream.Extending<SA__Render>()
                .Downstream.Extending<SA__Render_Begin>()
                .Upstream.Extending<SA__Render_Begin>()
                .Downstream.Extending<SA__Render_End>()
                .Upstream.Extending<SA__Render_End>();

            _Game__UPDATE = new SA__Update();
            _Game__RENDER = new SA__Render();
            _Game__RENDER_END = new SA__Render_End();
        }

        protected override void Execute()
        {
            Handle_Load__Content__Game();

            Invoke__Descending(new SA__Sealed_Under_Game());
            Invoke__Ascending(new SA__Sealed_Under_Game());   

        }

        protected virtual void Handle_Load__Content__Game(){}

        protected void Associate__Manager<TManager, TEntity>()
        where TManager : Game_Manager<TEntity>, new()
        where TEntity : Entity
        {
            Declare__Hierarchy()
                .Associate<TManager>();

            Declare__Streams()
                .Downstream.Extending<SA__Register_Entity<TEntity>>();
        }

        protected void Ascend__Update(double delta_time, double elapsed_time)
        {
            _Game__UPDATE.Frame__Delta_Time = delta_time;
            _Game__UPDATE.Frame__Elapsed_Time = elapsed_time;
            Invoke__Ascending(_Game__UPDATE);
        }

        protected void Descend__Update(double delta_time, double elapsed_time)
        {
            _Game__UPDATE.Frame__Delta_Time = delta_time;
            _Game__UPDATE.Frame__Elapsed_Time = elapsed_time;
            Invoke__Descending(_Game__UPDATE);
        }

        protected void Ascend__Render(double delta_time, double elapsed_time)
        {
            _Game__RENDER.Frame__Delta_Time = delta_time;
            _Game__RENDER.Frame__Elapsed_Time = elapsed_time;
            Invoke__Ascending(_Game__RENDER);
        }

        protected void Descend__Render(double delta_time, double elapsed_time)
        {
            _Game__RENDER.Frame__Delta_Time = delta_time;
            _Game__RENDER.Frame__Elapsed_Time = elapsed_time;
            Invoke__Descending(_Game__RENDER);
        }

        protected void Ascend__Render_Begin<TRender_Begin>(TRender_Begin e)
        where TRender_Begin : SA__Render_Begin
        {
            Invoke__Ascending(e);
        }

        protected void Descend__Render_Begin<TRender_Begin>(TRender_Begin e)
        where TRender_Begin : SA__Render_Begin
        {
            Invoke__Descending(e);
        }

        protected void Ascend__Render_End()
        {
            Invoke__Ascending(_Game__RENDER_END);
        }

        protected void Descend__Render_End()
        {
            Invoke__Descending(_Game__RENDER_END);
        }
    }
}
